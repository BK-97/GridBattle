using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpgradeBase : MonoBehaviour
{
    #region Params
    protected int upgradedHealth;
    protected int upgradedDamage;
    protected float upgradedRange;
    protected float upgradedRate;
    protected WarriorData upgradeData;
    private WarriorController warriorController;
    #endregion
    #region MonoBehaviourMethods
    private void Start()
    {
        warriorController = GetComponent<WarriorController>();
    }
    #endregion
    #region MyMethods
    public void Upgrade(int currentLevel, WarriorData upData)
    {
        upgradeData = upData;
        GetDataValues();
        CalculateData(currentLevel);
        SetDataValues(currentLevel);
    }

    #region VirtualMethods
    protected virtual void GetDataValues()
    {
        upgradedHealth = upgradeData.Health;
        upgradedDamage = upgradeData.Damage;
        upgradedRange = upgradeData.AttackRange;
        upgradedRate = upgradeData.AttackRate;
    }

    protected virtual void CalculateData(int currentLevel)
    {

    }

    protected virtual void SetDataValues(int currentLevel)
    {
        WarriorData newData = new WarriorData();
        newData.Health = upgradedHealth;
        newData.Damage = upgradedDamage;
        newData.AttackRange = upgradedRange;
        newData.AttackRate = upgradedRate;
        newData.warriorLevel = currentLevel;
        warriorController.SetDatas(newData);
    }
    #endregion
    #endregion
}
