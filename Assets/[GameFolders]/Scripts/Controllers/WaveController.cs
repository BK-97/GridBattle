using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CustomData
{
    public EnemyTypes EnemyType;
    public int SpawnCount;

    public CustomData(EnemyTypes enemyType, int count)
    {
        EnemyType = enemyType;
        SpawnCount = count;
    }
}
[Serializable]
public class WaveData
{
    public List<CustomData> WaveOrder;
    public float TotalWaveSpawnTime;
}
public class WaveController : MonoBehaviour
{
    #region Params
    [SerializeField]
    private List<WaveData> waves;
    [SerializeField]
    private List<Transform> spawnPosses;
    private int currentWave = -1;
    bool allEnemiesDied;
    #endregion
    private void OnEnable()
    {
        GameManager.Instance.OnBattleSessionStart.AddListener(StartSpawning);
        CharacterManager.OnAllEnemiesDied.AddListener(() => allEnemiesDied = true);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnBattleSessionStart.RemoveListener(StartSpawning);
        CharacterManager.OnAllEnemiesDied.RemoveListener(() => allEnemiesDied = true);

    }
    #region SpawnMethods
    public void StartSpawning()
    {
        currentWave++;
        StartCoroutine(SpawnWave());
    }
    private IEnumerator SpawnWave()
    {
        bool waveLoop = true;
        WaveData currentWaveData = waves[currentWave];
        int totalSpawnable = 0;

        for (int i = 0; i < currentWaveData.WaveOrder.Count; i++)
        {
            totalSpawnable += currentWaveData.WaveOrder[i].SpawnCount;
        }

        float spawnDelay = currentWaveData.TotalWaveSpawnTime / totalSpawnable;

        while (waveLoop)
        {
            foreach (CustomData data in currentWaveData.WaveOrder)
             {
                if (data.SpawnCount > 0)
                {
                    spawnPosses.Shuffle();
                    //float posX = spawnPosses[0].position.x;
                    float posX = -1.5f;
                    Vector3 spawnPos = new Vector3(posX,0,transform.position.z);
                    Quaternion spawnRotate = Quaternion.Euler(0, 180, 0);
                    var go=PoolingSystem.Instance.SpawnObject(PoolingSystem.Instance.GetObjectFromName("Enemy" + data.EnemyType.ToString()), spawnPos, spawnRotate, null);
                    CharacterManager.Instance.AddSpawnedEnemy(go);
                    data.SpawnCount--;
                    yield return new WaitForSeconds(spawnDelay);
                }
                else
                {
                    waveLoop = false;
                }
            }
        }
        yield return new WaitUntil(() => allEnemiesDied);
        allEnemiesDied = false;
        if (currentWave + 1 == waves.Count)
        {
            GameManager.Instance.OnStageWin.Invoke();
        }
        else
        {
            GameManager.Instance.OnSpawnSessionStart.Invoke();
        }
    }

    #endregion
}
