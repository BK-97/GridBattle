using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private void Start()
    {
        if (Managers.Instance == null)
            return;

        LevelManager.Instance.OnLevelFinish.AddListener(() => transform.SetParent(PoolingSystem.Instance.transform));
        SceneController.Instance.OnSceneStartedLoading.AddListener(() => PoolingSystem.Instance.DestroyAPS(gameObject));
    }

    private void OnDestroy()
    {
        if (Managers.Instance == null)
            return;

        LevelManager.Instance.OnLevelFinish.RemoveListener(() => transform.SetParent(PoolingSystem.Instance.transform));
        SceneController.Instance.OnSceneStartedLoading.RemoveListener(() => PoolingSystem.Instance.DestroyAPS(gameObject));
    }
}