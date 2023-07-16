using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour, IDamagable
{
    public Slider healthBar;
    public int totalHealth;
    private int currentHealth;
    private void Start()
    {
        healthBar.maxValue = totalHealth;
        SetHealth(totalHealth);
    }
    public void Die()
    {
        SetHealth(0);
        GameManager.OnStageLoose.Invoke();
    }

    public void SetHealth(int healthCount)
    {
        currentHealth = healthCount;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth - damage > 0)
        {
            SetHealth(currentHealth - damage);
        }
        else
            Die();
    }
}
