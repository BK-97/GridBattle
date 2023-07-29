using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(WarriorHealthController))]
public class WarriorController : MonoBehaviour
{
    #region Params
    public WarriorData warriorData;
    [SerializeField]
    private int damage;
    private float attackRate;
    private float attackRange;

    public int currentLevel;
    private bool canAttack;
    private IDamagable closestTarget;
    private WarriorHealthController healthController;
    [HideInInspector]
    public WarriorAnimatorController animatorController;
    private LevelUpgradeBase levelUpBase;
    public Transform raycastMuzzle;
    [SerializeField]
    private List<SkinnedMeshRenderer> meshes;
    #endregion
    #region MonoBehaviourMethods
    private void Start()
    {
        healthController = GetComponent<WarriorHealthController>();
        levelUpBase = GetComponent<LevelUpgradeBase>();
        animatorController = GetComponentInChildren<WarriorAnimatorController>();
        SetDatas(warriorData);
        ColorChange();

    }
    private void Update()
    {
        if (canAttack)
            CheckEnemyWithRaycast();
    }
    #endregion
    #region AttackMethods
    private void CheckEnemyWithRaycast()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(raycastMuzzle.position, raycastMuzzle.forward, out hitInfo, attackRange+0.1f, LayerMask.GetMask("Enemy")))
        {
            IDamagable damagable = hitInfo.collider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                closestTarget = damagable;
                AttackTimer();
            }
            else
                attackTimer = 999;
        }
    }
    public void Attack()
    {
        closestTarget.TakeDamage(damage);
    }
    public void AttackEnd()
    {
        attackTimer = 0f;
        isAttacking = false;
    }
    bool isAttacking;
    float attackTimer=999;
    private void AttackTimer()
    {
        if (!isAttacking)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackRate)
            {
                animatorController.AttackAnim();
                isAttacking = true;
            }
        }
    }
    #endregion
    #region MyMethods
    public void UpgradeWarrior()
    {
        currentLevel++;
        ColorChange();
        ChangeScale();
        levelUpBase.Upgrade(currentLevel, warriorData);
    }
    public void ControllerOff()
    {
        canAttack = false;
        isAttacking = false;
    }
    public void ControllerOn()
    {
        canAttack = true;
        isAttacking = false;
    }
    public void ColorChange()
    {
        for (int i = 0; i < meshes.Count; i++)
        {
            meshes[i].materials[0].color = ColorManager.Instance.GetWarriorColor(currentLevel);
        }
    }
    public void ChangeScale()
    {
        transform.DOScale(transform.localScale*1.1f,0.2f);
    }
    public void SetDatas(WarriorData currentData)
    {
        healthController.SetHealth(currentData.Health);
        damage = currentData.Damage;
        attackRate = currentData.AttackRate;
        attackRange = currentData.AttackRange;
        currentLevel = currentData.warriorLevel;
        canAttack = true;
    }
    #endregion
}
