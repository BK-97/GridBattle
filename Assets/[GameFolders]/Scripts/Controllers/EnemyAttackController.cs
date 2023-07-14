using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private int attackRate;
    private int attackRange;
    private int damage;
    bool canAttack;
    IDamagable closestTarget;
    public LayerMask attackableLayers;
    public void DataSet(EnemyData data)
    {
        attackRate = data.AttackRate;
        attackRange = data.AttackRange;
        damage = data.Damage;
    }

    public bool CheckEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, attackableLayers);

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
                    return true;
                }
            }
        }
        return false;

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
    public void AttackTimer()
    {
        if (!isAttacking)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackRate)
            {
                Attack();
            }
        }
    }
}
