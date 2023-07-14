using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour, IDamagable
{
    #region Params
    private int currentHealth;
    public Slider healthBar;
    #endregion
    #region IDamagableMethods
    public void Die()
    {
        currentHealth = 0;
        healthBar.value = currentHealth;
        Destroy(gameObject);
    }

    public void SetHealth(int healthCount)
    {
        currentHealth = healthCount;
        healthBar.maxValue = currentHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        if(currentHealth-damage>0)
        {
            currentHealth -= damage;
            healthBar.value = currentHealth;
        }
        else
        {
            Die();
        }
    }
    #endregion
}
