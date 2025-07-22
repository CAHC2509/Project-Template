using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapManager : MonoBehaviour
{
    public static SceneSwapManager Instance;

    private List<SceneField> activeScenes = new List<SceneField>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        Scene currentScene = SceneManager.GetActiveScene();
        activeScenes.Add(new SceneField(currentScene.name));
    }

    public void LoadAdditiveScene(SceneField scene)
    {
        if (!IsSceneActive(scene))
        {
            SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive);
            activeScenes.Add(scene);
        }
    }

    public void LoadAdditiveScenesSimple(List<SceneField> scenes)
    {
        foreach (SceneField scene in scenes)
        {
            if (!IsSceneActive(scene))
            {
                SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive);
                activeScenes.Add(scene);
            }
        }
    }

    public void SetActiveScene(SceneField scene)
    {
        if (IsSceneActive(scene))
        {
            Scene sceneToActivate = SceneManager.GetSceneByName(scene.SceneName);

            if (sceneToActivate != null)
                SceneManager.SetActiveScene(sceneToActivate);
        }
    }

    private IEnumerator LoadScenesWithFadeOut_Coroutine(List<SceneField> scenes)
    {
        yield return FadeInAndShowLoading();

        yield return LoadAndActivateFirstScene(scenes[0]);

        yield return UnloadOtherScenesExcept(scenes[0]);

        if (scenes.Count > 1)
            yield return LoadRemainingScenes(scenes.GetRange(1, scenes.Count - 1));

        yield return FinalizeLoading();
    }

    private IEnumerator FadeInAndShowLoading()
    {
        FadeManager.Instance.FadeIn();
        while (FadeManager.Instance.IsFadingIn)
            yield return null;

        LoadingScreenManager.Instance.SetProgress(0f);
        LoadingScreenManager.Instance.Show();

        FadeManager.Instance.BlackOut();
        yield return new WaitForSeconds(0.2f);
    }

    private IEnumerator LoadAndActivateFirstScene(SceneField firstScene)
    {
        var loadOp = SceneManager.LoadSceneAsync(firstScene.SceneName, LoadSceneMode.Additive);
        loadOp.allowSceneActivation = false;

        while (loadOp.progress < 0.9f)
        {
            LoadingScreenManager.Instance.SetProgress(loadOp.progress * 0.5f);
            yield return null;
        }

        loadOp.allowSceneActivation = true;
        while (!loadOp.isDone)
            yield return null;

        var newActiveScene = SceneManager.GetSceneByName(firstScene.SceneName);
        if (newActiveScene.IsValid())
            SceneManager.SetActiveScene(newActiveScene);

        activeScenes.Clear();
        activeScenes.Add(firstScene);
    }

    private IEnumerator UnloadOtherScenesExcept(SceneField sceneToKeep)
    {
        for (int i = SceneManager.sceneCount - 1; i >= 0; i--)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.IsValid() && scene.isLoaded && scene.name != sceneToKeep.SceneName)
            {
                var unloadOp = SceneManager.UnloadSceneAsync(scene);
                while (!unloadOp.isDone)
                    yield return null;
            }
        }
    }

    private IEnumerator LoadRemainingScenes(List<SceneField> remainingScenes)
    {
        float totalProgress = 0.5f;
        float increment = 0.5f / remainingScenes.Count;

        foreach (var scene in remainingScenes)
        {
            var loadOp = SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive);
            loadOp.allowSceneActivation = false;

            while (loadOp.progress < 0.9f)
            {
                LoadingScreenManager.Instance.SetProgress(totalProgress + loadOp.progress * increment);
                yield return null;
            }

            loadOp.allowSceneActivation = true;
            while (!loadOp.isDone)
                yield return null;

            activeScenes.Add(scene);
            totalProgress += increment;

            LoadingScreenManager.Instance.SetProgress(totalProgress);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator FinalizeLoading()
    {
        LoadingScreenManager.Instance.SetProgress(1f);
        yield return new WaitForSeconds(0.3f);

        LoadingScreenManager.Instance.Hide();

        FadeManager.Instance.FadeOut();
        while (FadeManager.Instance.IsFadingOut)
            yield return null;
    }


    public void UnloadScene(SceneField scene)
    {
        if (IsSceneActive(scene))
        {
            SceneManager.UnloadSceneAsync(scene.SceneName);
            activeScenes.Remove(scene);
        }
    }

    public void LoadAdditiveScenes(List<SceneField> scenes) => StartCoroutine(LoadScenesWithFadeOut_Coroutine(scenes));
    private bool IsSceneActive(SceneField scene) => activeScenes.Contains(scene);
}
