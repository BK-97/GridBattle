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
        var go = Instantiate(ExchangeManager.Instance.coinPrefab, transform.position, Quaternion.identity);
        go.transform.position = transform.position;
        go.GetComponent<CoinUp>().SetInfo(StateController.enemyData.cost, StateController.enemyData.currencyType);

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
            Debug.Log("takeDamage");
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
