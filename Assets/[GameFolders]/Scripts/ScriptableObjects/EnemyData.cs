using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/EnemyData")]
public class EnemyData : ScriptableObject
{
    public enum EnemyTypes { Minion, Ranger, Tank, Boss}
    public EnemyTypes EnemyType;
    public int Health;
    public int AttackRange;
    public int AttackRate;
    public int Damage;
    public int MoveSpeed;
}
