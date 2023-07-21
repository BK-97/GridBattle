using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CustomData
{
    public GameObject Enemy;
    public int SpawnCount;

    public CustomData(GameObject enemy, int count)
    {
        Enemy = enemy;
        SpawnCount = count;
    }
}
[Serializable]
public class WaveData
{
    public List<CustomData> WaveOrder;
    public float WaveTimer;
    public float NextWaveWaitTime;
}
public class EnemySpawner : MonoBehaviour
{
    #region Params
    [SerializeField]
    private List<WaveData> waves;
    private int currentWave = 0;
    #endregion
    private void OnEnable()
    {
        GameManager.OnBattleSessionStart.AddListener(StartSpawning);
    }
    private void OnDisable()
    {
        GameManager.OnBattleSessionStart.RemoveListener(StartSpawning);
    }
    #region SpawnMethods
    public void StartSpawning()
    {
        currentWave = 0;
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

        float spawnDelay = currentWaveData.WaveTimer / totalSpawnable;

        while (waveLoop)
        {
            foreach (CustomData data in currentWaveData.WaveOrder)
            {
                if (data.SpawnCount > 0)
                {
                    InstantiateObject(data.Enemy);
                    data.SpawnCount--;
                    yield return new WaitForSeconds(spawnDelay);
                }
                else
                {
                    waveLoop = false;
                }
            }
        }

        // Bekleme süresi için
        yield return new WaitUntil(() => IsAllEnemiesDead());
        GameManager.OnSpawnSessionStart.Invoke();
        yield return new WaitForSeconds(5f);

        currentWave++;
        if (currentWave >= waves.Count)
        {
            Debug.Log("Waves are completed!");
        }
        else
        {
            StartCoroutine(SpawnWave());
        }
    }

    private bool IsAllEnemiesDead()
    {
        return transform.childCount == 0;
    }
    
    private IEnumerator WaitForNextWave()
    {
        yield return new WaitForSeconds(waves[currentWave-1].NextWaveWaitTime);
        StartCoroutine(SpawnWave());

    }
    private void InstantiateObject(GameObject spawnObject)
    {
        var go=Instantiate(spawnObject, transform.position, transform.rotation, transform);
        float posX = GetRandomNumber();
        go.transform.position = new Vector3(posX,go.transform.position.y,go.transform.position.z);
    }
    #endregion
    #region Helpers
    private float GetRandomNumber()
    {
        float[] possibleValues = new float[] { -1.5f, -0.5f, 0.5f, 1.5f };
        int randomIndex = UnityEngine.Random.Range(0, possibleValues.Length);

        return possibleValues[randomIndex];
    }
    #endregion
}
