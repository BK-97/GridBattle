using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    #region Params
    private int attackRate;
    private int attackRange;
    private int damage;
    bool canAttack;
    IDamagable closestTarget;
    public LayerMask attackableLayers;
    private StateController stateController;
    public StateController StateController { get { return (stateController == null) ? stateController = GetComponent<StateController>() : stateController; } }
    #endregion
    #region SetMethods
    public void DataSet(EnemyData data)
    {
        attackRate = data.AttackRate;
        attackRange = data.AttackRange;
        damage = data.Damage;
    }
    #endregion
    #region CheckMethods
    public bool CheckEnemy()
    {

        RaycastHit hitInfo;

        if (Physics.Raycast(StateController.raycastPoint.position, StateController.raycastPoint.forward, out hitInfo, attackRange, attackableLayers))
        {
            IDamagable damagable = hitInfo.collider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                closestTarget = damagable;
                AttackTimer();
                return true;

            }
            else
                lastAttackTime = Time.time;
        }
        return false;

    }
    #endregion
    #region AttackMethods
    private void Attack()
    {
        isAttacking = true;
        lastAttackTime = Time.time;
        Debug.Log("attack");
        closestTarget.TakeDamage(damage);
        isAttacking = false;

    }
    bool isAttacking;
    float lastAttackTime;
    public void AttackTimer()
    {
        if (!isAttacking)
        {

            if (Time.time >= attackRate+ lastAttackTime)
            {
                Attack();
            }
        }
    }
    #endregion
}
