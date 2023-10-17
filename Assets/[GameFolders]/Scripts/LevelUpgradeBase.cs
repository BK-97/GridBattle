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
    protected CharacterData upgradeData;
    private WarriorController warriorController;
    #endregion
    #region MonoBehaviourMethods
    private void Start()
    {
        warriorController = GetComponent<WarriorController>();
    }
    #endregion
    #region MyMethods
    public void Upgrade(int currentLevel, CharacterData upData)
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
        //upgradedHealth = upgradedHealth*currentLevel;
        //upgradedDamage = upgradedDamage+(currentLevel * 2);
        //Debug.Log(upgradedDamage);
        //upgradedRate *= (currentLevel * 0.2f);
        //if (upgradedRange > 1)
        //    upgradedRange += currentLevel;
    }

    protected virtual void SetDataValues(int currentLevel)
    {
        CharacterData newData = new CharacterData();
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
