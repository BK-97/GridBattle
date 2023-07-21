using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarriorHealthController : MonoBehaviour,IDamagable
{
    #region Params
    public Slider healthBar;
    private int health;
    WarriorController warriorController;
    #endregion
    private void Start()
    {
        warriorController = GetComponent<WarriorController>();
    }
    #region IDamagableMethods
    public void Die()
    {
        health = 0;
        healthBar.value = health;
        warriorController.animatorController.DeathAnim();
        warriorController.ControllerOff();
        Destroy(gameObject,1.5f);
    }
    
    public void SetHealth(int healthCount)
    {
        health = healthCount;
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void TakeDamage(int damage)
    {
        if (health - damage <= 0)
            Die();
        else
        {
            health -= damage;
            healthBar.value = health;
            warriorController.animatorController.HitAnim();
            warriorController.ControllerOff();
        }
    }
    #endregion
}
