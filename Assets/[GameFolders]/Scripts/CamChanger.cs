using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamChanger : MonoBehaviour
{
    public CinemachineVirtualCamera SpawnCam;
    public CinemachineVirtualCamera BattleCam;
    private int maxPriority=10;
    private void OnEnable()
    {
        GameManager.Instance.OnBattleSessionStart.AddListener(()=> ChangeCamera(true));
        GameManager.Instance.OnSpawnSessionStart.AddListener(() => ChangeCamera(false));
    }
    private void OnDisable()
    {
        GameManager.Instance.OnBattleSessionStart.RemoveListener(() => ChangeCamera(true));
        GameManager.Instance.OnSpawnSessionStart.RemoveListener(() => ChangeCamera(false));
    }
    public void ChangeCamera(bool onBattle)
    {
        if(onBattle)
        {
            SpawnCam.Priority = maxPriority-1;
            BattleCam.Priority = maxPriority;
        }
        else
        {
            BattleCam.Priority = maxPriority - 1;
            SpawnCam.Priority = maxPriority;
        }
    }
}
