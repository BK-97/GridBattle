using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    public LevelData LevelData;

    public Level CurrentLevel { get { return LevelData.Levels[LevelIndex]; } }

    [HideInInspector]
    public UnityEvent OnLevelStart = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnLevelFinish = new UnityEvent();

    private bool isLevelStarted;
    public bool IsLevelStarted { get { return isLevelStarted; } set { isLevelStarted = value; } }

    public int LevelIndex
    {
        get
        {
            int level = PlayerPrefs.GetInt(PlayerPrefKeys.LastLevel, 0);
            if (level > LevelData.Levels.Count - 1)
            {
                level = 0;
                GameManager.Instance.GameConfig.IsLooping = true;
            }

            if (GameManager.Instance.GameConfig.IsLooping)
            {
                while (LevelData.Levels[level].LevelTypes.Contains(LevelType.Tutorial))
                    level++;
            }

            return level;
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.LastLevel, value);
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnStageLoose.AddListener(ReloadLevel);
        OnLevelStart.AddListener(StartLevel);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnStageLoose.RemoveListener(ReloadLevel);
        OnLevelStart.RemoveListener(StartLevel);
    }
    public void ReloadLevel()
    {
        FinishLevel();
        SceneController.Instance.LoadScene(CurrentLevel.LoadLevelID);
    }
    public void LoadLastLevel()
    {
        FinishLevel();
        SceneController.Instance.LoadScene(CurrentLevel.LoadLevelID);
    }
    public void LoadNextLevel()
    {
        FinishLevel();

        LevelIndex++;
        if (LevelIndex > LevelData.Levels.Count - 1)
        {
            LevelIndex = 0;
        }

        SceneController.Instance.LoadScene(CurrentLevel.LoadLevelID);
    }
    public void LoadPreviousLevel()
    {
        FinishLevel();

        LevelIndex--;
        if (LevelIndex <= -1)
        {
            LevelIndex = LevelData.Levels.Count - 1;

        }

        SceneController.Instance.LoadScene(CurrentLevel.LoadLevelID);
    }
    private void StartLevel()
    {
        if (IsLevelStarted)
            return;
        IsLevelStarted = true;
    }

    public void FinishLevel()
    {
        if (!IsLevelStarted)
            return;
        IsLevelStarted = false;
        OnLevelFinish.Invoke();
    }

}