using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void SetHealth(int healthCount);
    void TakeDamage(int damage);
    void Die();
}
