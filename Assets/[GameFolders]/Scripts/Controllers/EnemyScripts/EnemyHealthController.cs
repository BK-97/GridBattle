using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour, IDamageable
{
    #region Params
    public HealthBar healthBar;

    private int currentHealth;

    private StateController stateController;
    public StateController StateController { get { return (stateController == null) ? stateController = GetComponent<StateController>() : stateController; } }
    #endregion
    #region IDamagableMethods
    public void Die()
    {
        var go = PoolingSystem.SpawnObject(PoolingSystem.Instance.GetObjectFromName("CoinStack"),transform.position,Quaternion.identity);
        go.GetComponent<CoinStack>().SetInfo(Mathf.RoundToInt(StateController.enemyData.cost * ExchangeManager.Instance.currentIncomeMultiplier), CurrencyType.Coin);

        currentHealth = 0;
        healthBar.ChangeBar(currentHealth);
        GetComponentInChildren<EnemyAnimationController>().DeathAnim();
        GetComponent<Collider>().enabled = false;
        StartCoroutine(WaitForDieCO());
    }
    IEnumerator WaitForDieCO()
    {
        yield return new WaitForSeconds(1);
        CharacterManager.Instance.RemoveSpawnedEnemy(gameObject);
        PoolingSystem.ReturnObjectToPool(gameObject);
    }
    public void SetHealth(int healthCount)
    {
        currentHealth = healthCount;
        healthBar.SetBar(currentHealth);
        GetComponent<Collider>().enabled = true;
    }

    public void TakeDamage(int damage)
    {
        if(currentHealth-damage>0)
        {
            currentHealth -= damage;
            healthBar.ChangeBar(currentHealth);
        }
        else
        {
            Die();
        }
    }
    #endregion
}
