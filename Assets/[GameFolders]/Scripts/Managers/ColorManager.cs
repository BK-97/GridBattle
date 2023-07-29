using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : Singleton<ColorManager>
{
    public List<Color> WarriorColors;
    public List<Color> EnemyColors;

    public Color GetWarriorColor(int level)
    {
        return WarriorColors[level - 1];
    }
    public Color GetEnemyColor(int level)
    {
        return EnemyColors[level - 1];
    }
}
