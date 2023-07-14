using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WarriorHealthController))]
public class WarriorController : MonoBehaviour
{
    public WarriorData warriorData;
    private int damage;
    private float attackRate;
    private float attackRange;

    private bool canAttack;
    private IDamagable closestTarget;
    private WarriorHealthController healthController;
    private WarriorAnimatorController animatorController;
    public Transform raycastMuzzle;
    private void Start()
    {
        healthController = GetComponent<WarriorHealthController>();
        animatorController = GetComponentInChildren<WarriorAnimatorController>();
        SetDatas(warriorData);
    }
    private void Update()
    {
        if (canAttack)
            CheckEnemyWithRaycast();
    }
    #region AttackMethods
    private void CheckEnemyWithRaycast()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(raycastMuzzle.position, raycastMuzzle.forward, out hitInfo, attackRange, LayerMask.GetMask("Enemy")))
        {
            IDamagable damagable = hitInfo.collider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                closestTarget = damagable;
                AttackTimer();
            }
            else
                attackTimer = 999;
        }
    }
    public void Attack()
    {
        closestTarget.TakeDamage(damage);
    }
    public void AttackEnd()
    {
        attackTimer = 0f;
        isAttacking = false;
    }
    bool isAttacking;
    float attackTimer=999;
    private void AttackTimer()
    {
        if (!isAttacking)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackRate)
            {
                animatorController.AttackAnim();
                isAttacking = true;
            }
        }
    }
    #endregion
    public void UpgradeWarrior()
    {

    }
    public void ControllerOff()
    {
        canAttack = false;
        isAttacking = false;
    }
    public void ControllerOn()
    {
        canAttack = true;
        isAttacking = false;
    }
    public void SetDatas(WarriorData currentData)
    {
        healthController.SetHealth(warriorData.Health);
        damage = warriorData.Damage;
        attackRate = warriorData.AttackRate;
        attackRange = warriorData.AttackRange;
        canAttack = true;
    }
}
