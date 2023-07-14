using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightUpgrade : LevelUpgradeBase
{
    #region OverrideMethods
    protected override void CalculateData(int currentLevel)
    {
        if (currentLevel == 2 || currentLevel == 5 || currentLevel == 8)
        {
            upgradedHealth += 5;
        }
        else if(currentLevel==3|| currentLevel == 6|| currentLevel == 9)
        {
            upgradedDamage += 2;
        }
        else if(currentLevel == 4 || currentLevel == 7 || currentLevel == 10)
        {
            upgradedRate += 0.5f;
        }
    }
    #endregion
}
