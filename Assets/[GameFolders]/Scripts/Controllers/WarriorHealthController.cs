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
    private void OnEnable()
    {
        GameManager.Instance.OnStageWin.AddListener(CoinCreate);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnStageWin.RemoveListener(CoinCreate);
    }
    private void CoinCreate()
    {
        var go = Instantiate(ExchangeManager.Instance.coinPrefab, transform.position, Quaternion.identity);
        go.transform.position = transform.position;
        go.GetComponent<CoinUp>().SetInfo(Mathf.RoundToInt(warriorController.warriorData.cost), CurrencyType.Coin);
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
