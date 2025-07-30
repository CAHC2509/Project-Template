using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoaderController : UIControllerBase
{
    [SerializeField] private SceneLoaderView view;
    [SerializeField] private FadeInteractionController fadeController;

    private Dictionary<string, AsyncOperationHandle<SceneInstance>> loadedSceneHandles = new Dictionary<string, AsyncOperationHandle<SceneInstance>>();

    public event Action<string> OnSceneFullyLoaded;

    public override void Initialize()
    {
        AddListeners();
        view.Initialize();
    }

    public override void Conclude()
    {
        RemoveListeners();
        view.Conclude();
    }

    private void AddListeners()
    {
        OnSceneFullyLoaded += ActivateScene;
    }

    private void RemoveListeners()
    {
        OnSceneFullyLoaded -= ActivateScene;
    }

    public void LoadScene(string sceneName)
    {
        if (loadedSceneHandles.ContainsKey(sceneName)) return;
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        fadeController.BeginInteraction();
        yield return new WaitForSeconds(fadeController.FadeDuration);

        view.SetProgress(0f);
        view.EnableView();

        fadeController.FinishInteraction();

        yield return new WaitForSeconds(fadeController.FadeDuration);

        var handle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive, activateOnLoad: false);
        loadedSceneHandles[sceneName] = handle;

        while (!handle.IsDone)
        {
            view.SetProgress(Mathf.Clamp01(handle.PercentComplete));
            yield return null;
        }

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Error while loading the scene: {sceneName}. State: {handle.Status}");
            Addressables.Release(handle);
            loadedSceneHandles.Remove(sceneName);
            view.DisableView();
            fadeController.FinishInteraction();
            yield break;
        }

        view.SetProgress(1f);

        OnSceneFullyLoaded?.Invoke(sceneName);
    }

    public void ActivateScene(string sceneName)
    {
        if (loadedSceneHandles.TryGetValue(sceneName, out var handle))
        {
            fadeController.BeginInteraction();

            handle.Result.ActivateAsync().completed += (asyncOp) =>
            {
                handle.Result.ActivateAsync().completed += _ => SceneManager.SetActiveScene(handle.Result.Scene);

                fadeController.FinishInteraction();
                view.DisableView();
            };
        }
    }

    public void UnloadScene(string sceneName)
    {
        if (loadedSceneHandles.TryGetValue(sceneName, out var handle))
        {
            Addressables.UnloadSceneAsync(handle, true).Completed += (asyncOp) =>
            {
                loadedSceneHandles.Remove(sceneName);
            };
        }
    }

    public void FocusScene(string sceneName)
    {
        if (loadedSceneHandles.TryGetValue(sceneName, out var handle))
        {
            if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result.Scene.IsValid())
                SceneManager.SetActiveScene(handle.Result.Scene);
        }
    }
}