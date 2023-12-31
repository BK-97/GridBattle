using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    #region Params
    private float attackRate;
    private float attackRange;
    private int damage;

    bool isAttacking;
    float lastAttackTime=-Mathf.Infinity;
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
    public void DataSet(CharacterData data)
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
            GameManager.Instance.OnStageLoose.Invoke();
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
                if (!grid.hasObject)
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
                if (!grid.hasObject)
                    return false;

                IDamageable damagable =grid.GetGridObject().GetComponent<IDamageable>();
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
        switch (StateController.enemyData.CharacterType)
        {
            case CharacterTypes.Knight:
                closestTarget.TakeDamage(damage);
                break;
            case CharacterTypes.Archer:
                rangerAttack.CreateBullet(LayerMask.GetMask("Warrior"),damage,-Vector3.forward,transform.position.x);
                break;
            case CharacterTypes.TwoHanded:
                closestTarget.TakeDamage(damage);
                break;
            case CharacterTypes.Mage:
                rangerAttack.CreateBullet(LayerMask.GetMask("Warrior"), damage, -Vector3.forward, transform.position.x);
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
