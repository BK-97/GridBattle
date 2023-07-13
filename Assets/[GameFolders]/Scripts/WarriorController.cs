using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : MonoBehaviour
{
    public WarriorData warriorData;
    private int damage;
    private int attackRate;
    private int attackRange;

    private bool canAttack;
    private IDamagable closestTarget;
    private WarriorHealthController healthController;
    private void Start()
    {
        healthController = GetComponent<WarriorHealthController>();
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
            CheckEnemy();
    }
    private void CheckEnemy()
    {
        float attackRange = warriorData.AttackRange;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, LayerMask.GetMask("Enemy"));

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
    private void Attack()
    {
        isAttacking = true;
        attackTimer = 0f;
        closestTarget.TakeDamage(damage);
        isAttacking = false;

    }
    bool isAttacking;
    float attackTimer;
    private void AttackTimer()
    {
        if (!isAttacking)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= warriorData.AttackRate)
            {
                Attack();
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
