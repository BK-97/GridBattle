using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpgradeBase : MonoBehaviour
{
    protected int upgradedHealth;
    protected int upgradedDamage;
    protected float upgradedRange;
    protected float upgradedRate;
    protected WarriorData upgradeData;
    private WarriorController warriorController;
    private void Start()
    {
        warriorController = GetComponent<WarriorController>();
    }
    public void Upgrade(int currentLevel, WarriorData upData)
    {
        upgradeData = upData;
        GetDataValues();
        CalculateData(currentLevel);
        SetDataValues();
    }

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

    protected virtual void SetDataValues()
    {
        WarriorData newData = new WarriorData();
        newData.Health = upgradedHealth;
        newData.Damage = upgradedDamage;
        newData.AttackRange = upgradedRange;
        newData.AttackRate = upgradedRate;
        warriorController.SetDatas(newData);
    }
}
