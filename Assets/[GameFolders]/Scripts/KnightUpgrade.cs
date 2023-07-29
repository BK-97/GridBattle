using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightUpgrade : LevelUpgradeBase
{
    #region OverrideMethods
    protected override void CalculateData(int currentLevel)
    {
        upgradedHealth *=2;
        upgradedDamage = Mathf.RoundToInt(upgradedDamage*1.5f);
        upgradedRate *= (currentLevel * 0.2f);
        if (upgradedRange > 1)
            upgradedRange += currentLevel-1;
    }
    #endregion
}
