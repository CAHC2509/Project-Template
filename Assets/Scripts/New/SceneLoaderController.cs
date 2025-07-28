using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderController : UIControllerBase
{
    [SerializeField] private SceneLoaderView view;
    [SerializeField] private FadeInteractionController fadeController;

    private Dictionary<string, AsyncOperation> sceneOperations = new();

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
        if (sceneOperations.ContainsKey(sceneName)) return;

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

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        operation.allowSceneActivation = false;
        sceneOperations[sceneName] = operation;

        while (operation.progress < 0.9f)
        {
            view.SetProgress(Mathf.Clamp01(operation.progress / 0.9f));
            yield return null;
        }

        view.SetProgress(1f);

        fadeController.BeginInteraction();

        yield return new WaitForSeconds(fadeController.FadeDuration);

        OnSceneFullyLoaded?.Invoke(sceneName);
    }

    public void ActivateScene(string sceneName)
    {
        if (sceneOperations.TryGetValue(sceneName, out var operation))
        {
            fadeController.FinishInteraction();
            view.DisableView();
            operation.allowSceneActivation = true;
            sceneOperations.Remove(sceneName);
        }
    }

    public void UnloadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.UnloadSceneAsync(sceneName);

        if (sceneOperations.ContainsKey(sceneName))
            sceneOperations.Remove(sceneName);
    }

    public void FocusScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }
}
