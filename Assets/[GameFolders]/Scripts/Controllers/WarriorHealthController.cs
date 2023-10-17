using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarriorHealthController : MonoBehaviour, IDamageable
{
    #region Params
    public HealthBar healthBar;
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
        var go = PoolingSystem.SpawnObject(PoolingSystem.Instance.GetObjectFromName("CoinStack"), transform.position, Quaternion.identity);
        go.GetComponent<CoinStack>().SetInfo(Mathf.RoundToInt(warriorController.warriorData.cost), CurrencyType.Coin);
    }
    #region IDamagableMethods
    public void Die()
    {
        health = 0;
        healthBar.ChangeBar(health);
        warriorController.animatorController.DeathAnim();
        warriorController.ControllerOff();
        GetComponentInParent<GridSystem.Grid>().RemoveObject();
        StartCoroutine(WaitForDieCO());
    }
    IEnumerator WaitForDieCO()
    {
        yield return new WaitForSeconds(1.5f);
        CharacterManager.Instance.RemoveAlly(gameObject);
        PoolingSystem.ReturnObjectToPool(gameObject);
    }
    public void SetHealth(int healthCount)
    {
        health = healthCount;
        healthBar.SetBar(health);

    }

    public void TakeDamage(int damage)
    {
        if (health - damage <= 0)
            Die();
        else
        {
            health -= damage;
            healthBar.ChangeBar(health);
            warriorController.animatorController.HitAnim();
            warriorController.ControllerOff();
            PoolingSystem.SpawnObject(PoolingSystem.Instance.GetObjectFromName("Blood"), transform.position, Quaternion.identity);
        }
    }
    #endregion
}
