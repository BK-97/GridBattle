using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    #region Params
    private int attackRate;
    private int attackRange;
    private int damage;
    [HideInInspector]
    public bool canAttack;
    IDamageable closestTarget;
    public LayerMask gridLayer;
    private StateController stateController;
    public StateController StateController { get { return (stateController == null) ? stateController = GetComponent<StateController>() : stateController; } }
    private Invader invader;
    public Invader Invader { get { return (invader == null) ? invader = GetComponent<Invader>() : invader; } }
    private EnemyAnimationController animationController;
    public EnemyAnimationController AnimationController { get { return (animationController == null) ? animationController = GetComponentInChildren<EnemyAnimationController>() : animationController; } }
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
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.4f, LayerMask.GetMask("Base")))
        {
            GameManager.Instance.CompeleteStage(false);
            return false;
        }
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, attackRange-0.25f, gridLayer))
        {

            GridSystem.Grid grid = hitInfo.collider.GetComponentInParent<GridSystem.Grid>();
            if (grid == null)
                grid = hitInfo.collider.GetComponent<GridSystem.Grid>();

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

            IDamageable damagable = Invader.targetGrid.gridObject.GetComponent<IDamageable>();
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
    public void Attack()
    {
        closestTarget.TakeDamage(damage);
    }
    public void AttackEnd()
    {
        lastAttackTime = Time.time;
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
                isAttacking = true;
                AnimationController.AttackAnim();
            }
        }
    }
    #endregion
}
