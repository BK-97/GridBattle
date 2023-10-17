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

    [Header("Ranged Character Params")]
    public RangerAttack rangerAttack;

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
    public bool CheckBase()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.4f, LayerMask.GetMask("Base")))
        {
            GameManager.Instance.CompeleteStage(false);
            return true;
        }
        return false;
    }
    public bool CheckGridInvadable()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.75f, gridLayer))
        {
            GridSystem.Grid grid = hitInfo.collider.GetComponent<GridSystem.Grid>();

            if (grid != null)
            {
                if (grid.gridObject == null)
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
        }
        return false;
    }

    public bool CheckEnemy()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, attackRange-0.25f, gridLayer))
        {
            GridSystem.Grid grid = hitInfo.collider.GetComponentInParent<GridSystem.Grid>();
            if (grid != null)
            {
                if (grid.gridObject == null)
                    return false;

                IDamageable damagable =grid.gridObject.GetComponent<IDamageable>();
                if (damagable != null)
                {
                    closestTarget = damagable;
                    return true;
                }
            }
        }
        return false;
    }
    #endregion
    #region AttackMethods
    public void Attack()
    {
        switch (StateController.enemyData.EnemyType)
        {
            case EnemyTypes.Knight:
                closestTarget.TakeDamage(damage);
                break;
            case EnemyTypes.Archer:
                rangerAttack.CreateBullet(LayerMask.GetMask("Warrior"),damage);
                break;
            case EnemyTypes.TwoHanded:
                closestTarget.TakeDamage(damage);
                break;
            case EnemyTypes.Mage:
                rangerAttack.CreateBullet(LayerMask.GetMask("Warrior"), damage);
                break;
            default:
                break;
        }
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
