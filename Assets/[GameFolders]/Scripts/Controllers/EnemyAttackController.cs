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
    public LayerMask gridLayer;
    private StateController stateController;
    public StateController StateController { get { return (stateController == null) ? stateController = GetComponent<StateController>() : stateController; } }
    private Invader invader;
    public Invader Invader { get { return (invader == null) ? invader = GetComponent<Invader>() : invader; } }
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
    public bool CheckGrid()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, attackRange-0.25f, gridLayer))
        {

            GridSystem.Grid grid = hitInfo.collider.GetComponentInParent<GridSystem.Grid>();
            if (grid == null)
                grid = hitInfo.collider.GetComponent<GridSystem.Grid>();

            Debug.DrawLine(transform.position, hitInfo.point);

            if (grid != null)
            {
                Invader.targetGrid = grid;
                return true;
            }
            else
            {

                Invader.targetGrid = null;
                return false;
            }

        }
        return false;
    }
    public bool CheckEnemy()
    {
        if (Invader.targetGrid.gridObject == null)
        {
            return false;
        }
        else
        {

            IDamagable damagable = Invader.targetGrid.gridObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                closestTarget = damagable;
                return true;
            }
            else
            {
                lastAttackTime = Time.time;
                return false;
            }
        }
    }
    #endregion
    #region AttackMethods
    private void Attack()
    {
        isAttacking = true;
        lastAttackTime = Time.time;
        closestTarget.TakeDamage(damage);
        isAttacking = false;

    }
    bool isAttacking;
    float lastAttackTime;
    public void AttackTimer()
    {
        if (!isAttacking)
        {

            if (Time.time >= attackRate + lastAttackTime)
            {
                Attack();
            }
        }
    }
    #endregion
}
