using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;


public class CharacterManager : Singleton<CharacterManager>
{
    #region Params
    private List<GameObject> spawnedEnemies=new List<GameObject>();
    private List<GameObject> spawnedAllies=new List<GameObject>();
    private List<Grid> SpawnedGrids=new List<Grid>();
    #endregion
    #region Events
    public static GameObjectEvent OnNewAllySpawned = new GameObjectEvent();
    public static UnityEvent OnAllEnemiesDied = new UnityEvent();
    #endregion
    #region Methods
    public void AddSpawnedEnemy(GameObject spawnedEnemy)
    {
        spawnedEnemies.Add(spawnedEnemy);
    }
    public void RemoveSpawnedEnemy(GameObject spawnedEnemy)
    {
        spawnedEnemies.Remove(spawnedEnemy);

        if (CheckAllEnemiesDead())
            OnAllEnemiesDied.Invoke();
    }
    public void AddSpawnedAlly(GameObject newAlly)
    {
        spawnedAllies.Add(newAlly);
        OnNewAllySpawned.Invoke(newAlly);
    }
    public void RemoveAlly(GameObject newAlly)
    {
        spawnedAllies.Remove(newAlly);
    }

    #endregion
    #region Helpers

    private bool CheckAllEnemiesDead()
    {
        return spawnedEnemies.Count == 0 ? true : false;
    }
    private bool CheckAllAlliesDead()
    {
        return spawnedAllies.Count == 0 ? true : false;
    }
    public int GetEnemyCount()
    {
        return spawnedEnemies.Count;
    }
    public int GetAllyCount()
    {
        return spawnedAllies.Count;
    }
    #endregion
}
