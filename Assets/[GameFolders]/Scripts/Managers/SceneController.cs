using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class SceneController : Singleton<SceneController>
{
    [HideInInspector]
    public UnityEvent OnSceneStartedLoading = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnSceneUnloading = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnSceneLoaded = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnSceneUnLoaded = new UnityEvent();
    [HideInInspector]
    public SceneEvent OnSceneInfo = new SceneEvent();


    public bool loadingInProgress { get; private set; }

    public void LoadScene(string scene)
    {
        if (loadingInProgress)
            return;

        StartCoroutine(LoadSceneCo(scene));
    }

    IEnumerator LoadSceneCo(string sceneName)
    {
        loadingInProgress = true;
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name.Contains("Level"))
                yield return UnloadSceneCo(scene);

            else if (scene.name.Contains("Test"))
            {
                sceneName = "Test";
            }
        }

        OnSceneStartedLoading.Invoke();

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        var loadedScene = SceneManager.GetSceneByName(sceneName);
        if (loadedScene.name.Contains("Level") || loadedScene.name.Contains("Test"))
        {
            SceneManager.SetActiveScene(loadedScene);
            yield return new WaitForSeconds(0.2f);
            OnSceneLoaded.Invoke();
        }

        OnSceneInfo.Invoke(loadedScene, true);
        loadingInProgress = false;
    }

    public void UnloadScene(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
        StartCoroutine(UnloadSceneCo(scene));
    }

    IEnumerator UnloadSceneCo(Scene scene)
    {
        OnSceneInfo.Invoke(scene, false);
        OnSceneUnloading.Invoke();
        yield return SceneManager.UnloadSceneAsync(scene.buildIndex);
        OnSceneUnLoaded.Invoke();
    }

    public int GetScene(string name)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scene = SceneUtility.GetScenePathByBuildIndex(i);
            if (scene.Contains(name))
                return i;
        }
        return 2;
    }

    public Scene GetCurrentScene(string name)
    {
        return SceneManager.GetSceneByName(name);
    }

    public Scene GetCurrentScene(int index)
    {
        return SceneManager.GetSceneByBuildIndex(index);
    }
}

public class SceneEvent : UnityEvent<Scene, bool> { }