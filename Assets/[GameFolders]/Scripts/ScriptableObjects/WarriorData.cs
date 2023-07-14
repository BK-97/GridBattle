using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Datas/WarriorData")]
public class WarriorData : ScriptableObject
{
    public enum WarriorTypes { Knight,Archer,TwoHanded,Mage}
    public WarriorTypes WarriorType;
    [Range(1,11)]
    public int warriorLevel=1;
    public int Health;
    public float AttackRange;
    public float AttackRate;
    public int Damage;


}
