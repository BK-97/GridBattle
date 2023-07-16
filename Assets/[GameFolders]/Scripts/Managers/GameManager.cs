using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    #region Params
    public enum GameStates { Menu,SpawnSession,BattleSession}
    public GameStates GameState;
    #endregion
    #region Events
    public static UnityEvent OnGameStart = new UnityEvent();
    public static UnityEvent OnSpawnSessionStart = new UnityEvent();
    public static UnityEvent OnBattleSessionStart = new UnityEvent();
    public static UnityEvent OnStageWin = new UnityEvent();
    public static UnityEvent OnStageLoose = new UnityEvent();
    #endregion
    #region MonoBehaviourMethods
    private void Start()
    {
        OnGameStart.Invoke();
    }
    private void OnEnable()
    {
        OnGameStart.AddListener(GameStarted);
        OnSpawnSessionStart.AddListener(() => ChangeState(GameStates.SpawnSession));
        OnBattleSessionStart.AddListener(() => ChangeState(GameStates.BattleSession));
        OnStageWin.AddListener(WinGame);
        OnStageLoose.AddListener(LooseGame);
    }
    private void OnDisable()
    {
        OnGameStart.RemoveListener(GameStarted);
        OnSpawnSessionStart.RemoveListener(() => ChangeState(GameStates.SpawnSession));
        OnBattleSessionStart.RemoveListener(() => ChangeState(GameStates.BattleSession));
        OnStageWin.RemoveListener(WinGame);
        OnStageLoose.RemoveListener(LooseGame);
    }
    #endregion
    #region MyMethods
    private void GameStarted()
    {

    }
    private void WinGame()
    {

    }
    private void LooseGame()
    {

    }
    public void ChangeState(GameStates newState)
    {
        GameState = newState;
    }
    #endregion
}
