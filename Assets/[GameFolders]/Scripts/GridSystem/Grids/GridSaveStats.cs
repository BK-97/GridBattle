using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSaveStats
{
    public string gridID;
    public int levelOfWarrior;
    public CharacterTypes warriorType;
    public GridSaveStats(string gridID,int levelOfWarrior,CharacterTypes warriorType)
    {
        this.gridID = gridID;
        this.levelOfWarrior = levelOfWarrior;
        this.warriorType = warriorType;
    }
    public GridSaveStats()
    {

    }
}
