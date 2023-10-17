using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Datas/CharacterData")]
public class CharacterData : ScriptableObject
{
    public CharacterTypes CharacterType;
    [Range(1,11)]
    public int warriorLevel=1;
    public int Health;
    public float AttackRange;
    public float AttackRate;
    public int Damage;
    public int cost;
    public float MoveSpeed;

}
