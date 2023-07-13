using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Datas/WarriorData")]
public class WarriorData : ScriptableObject
{
    public enum WarriorTypes { Knight,Archer,TwoHanded,Mage}
    public WarriorTypes WarriorType;
    public int Health;
    public int AttackRange;
    public int AttackRate;
    public int Damage;
}
