using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour, IDamagable
{
    #region Params
    private int currentHealth;
    public Slider healthBar;
    private StateController stateController;
    public StateController StateController { get { return (stateController == null) ? stateController = GetComponent<StateController>() : stateController; } }
    #endregion

    #region IDamagableMethods
    public void Die()
    {
        var go = PoolingSystem.SpawnObject(PoolingSystem.Instance.GetObjectFromName("CoinStack"),transform.position,Quaternion.identity);
        go.GetComponent<CoinStack>().SetInfo(Mathf.RoundToInt(StateController.enemyData.cost * ExchangeManager.Instance.currentIncomeMultiplier), CurrencyType.Coin);

        currentHealth = 0;
        healthBar.value = currentHealth;
        GetComponentInChildren<EnemyAnimationController>().DeathAnim();
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject,1);
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
