using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WarriorHealthController))]
public class WarriorController : MonoBehaviour
{
    public WarriorData warriorData;
    private int damage;
    private int attackRate;
    private int attackRange;

    private bool canAttack;
    private IDamagable closestTarget;
    private WarriorHealthController healthController;
    private WarriorAnimatorController animatorController;
    public Transform raycastMuzzle;
    private void Start()
    {
        healthController = GetComponent<WarriorHealthController>();
        animatorController = GetComponentInChildren<WarriorAnimatorController>();
        SetDatas();
    }
    private void SetDatas()
    {
        healthController.SetHealth(warriorData.Health);
        damage = warriorData.Damage;
        attackRate = warriorData.AttackRate;
        attackRange = warriorData.AttackRange;
        canAttack = true;
    }
    private void Update()
    {
        if(canAttack)
            CheckEnemyWithRaycast();
    }
    private void CheckEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(raycastMuzzle.position, attackRange, LayerMask.GetMask("Enemy"));

        float closestDistance = Mathf.Infinity;
        closestTarget = null;

        foreach (Collider hitCollider in hitColliders)
        {
            IDamagable damagable = hitCollider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                float distance = Vector3.Distance(transform.position, hitCollider.gameObject.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = damagable;
                    AttackTimer();
                }
            }
        }
    }
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
}
