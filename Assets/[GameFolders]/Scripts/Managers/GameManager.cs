using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameConfig
{
    public bool IsLooping
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefKeys.IsLooping, 0) != 0;
        }

        set
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.IsLooping, 1);
        }
    }
}
public class GameManager : Singleton<GameManager>
{
    #region Params
    public enum GameStates { Menu, SpawnSession, BattleSession }
    public GameStates GameState;
    public GameConfig GameConfig;
    private bool isGameStarted;
    public bool IsGameStarted { get { return isGameStarted; } set { isGameStarted = value; } }

    private bool isStageCompleted;
    public bool IsStageCompleted { get { return isStageCompleted; } set { isStageCompleted = value; } }
    private int currentWaveLevel=1; // PlayerPrefs ile burdan kayýtlý leveli alacaðýz daha sonra spawnerde burayý kullanacak bu artýk bizim levelimiz gibi biþey oldu yani
    #endregion
    #region Events
    [HideInInspector]
    public UnityEvent OnGameStart = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnGameEnd = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnSpawnSessionStart = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnBattleSessionStart = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnStageWin = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnStageLoose = new UnityEvent();
    #endregion
    #region MonoBehaviourMethods
    private void OnEnable()
    {
        SceneController.Instance.OnSceneLoaded.AddListener(() => IsStageCompleted = false);
        LevelManager.Instance.OnLevelStart.AddListener(() => { EventManager.OnTimeSet.Invoke(5); OnSpawnSessionStart.Invoke(); });
        OnSpawnSessionStart.AddListener(() => ChangeState(GameStates.SpawnSession));
        OnBattleSessionStart.AddListener(() => ChangeState(GameStates.BattleSession));
        OnStageWin.AddListener(() => CompeleteStage(true));
        OnStageLoose.AddListener(() => CompeleteStage(false));
    }
    private void OnDisable()
    {
        SceneController.Instance.OnSceneLoaded.RemoveListener(() => IsStageCompleted = false);
        LevelManager.Instance.OnLevelStart.RemoveListener(() => { EventManager.OnTimeSet.Invoke(5); OnSpawnSessionStart.Invoke(); });
        OnSpawnSessionStart.RemoveListener(() => ChangeState(GameStates.SpawnSession));
        OnBattleSessionStart.RemoveListener(() => ChangeState(GameStates.BattleSession));
        OnStageWin.RemoveListener(() => CompeleteStage(true));
        OnStageLoose.RemoveListener(() => CompeleteStage(false));
    }
    #endregion
    #region MyMethods
    private void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        if (isGameStarted)
            return;

        isGameStarted = true;
        OnGameStart.Invoke();
    }
    public void EndGame()
    {
        if (!isGameStarted)
            return;
        isGameStarted = false;
        OnGameEnd.Invoke();
    }
    public void CompeleteStage(bool value)
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;

        if (IsStageCompleted == true)
            return;
        StartCoroutine(WaitLevelChange(value));
        IsStageCompleted = true;
    }


    IEnumerator WaitLevelChange(bool status)
    {
        yield return new WaitForSeconds(2);
        if (status)
            LevelManager.Instance.LoadNextLevel();
        else
            LevelManager.Instance.ReloadLevel();
    }
    public void ChangeState(GameStates newState)
    {
        GameState = newState;
        Debug.Log(newState.ToString());
        if (GameState == GameStates.SpawnSession&&currentWaveLevel!=1)
            currentWaveLevel++;
    }

    public int GetCurrentWaveLevel()
    {
        return currentWaveLevel;
    }
    #endregion
}
