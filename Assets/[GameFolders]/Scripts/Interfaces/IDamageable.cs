using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void SetHealth(int healthCount);
    void TakeDamage(int damage);
    void Die();
}
