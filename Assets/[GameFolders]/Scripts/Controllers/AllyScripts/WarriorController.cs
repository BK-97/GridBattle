using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(WarriorHealthController))]
public class WarriorController : MonoBehaviour
{
    #region Params
    [Header("Datas")]
    public CharacterData warriorData;
    private int damage;
    private float attackRate;
    private float attackRange;
    [HideInInspector]
    public int currentLevel;

    bool isAttacking;
    float lastAttackTime = -Mathf.Infinity;
    private bool canAttack;
    private IDamageable closestTarget;
    private WarriorHealthController healthController;
    public WarriorHealthController HealthController { get { return (healthController == null) ? healthController = GetComponent<WarriorHealthController>() : healthController; } }

    private WarriorAnimatorController animatorController;
    public WarriorAnimatorController AnimatorController { get { return (animatorController == null) ? animatorController = GetComponentInChildren<WarriorAnimatorController>() : animatorController; } }
    private LevelUpgradeBase levelUpBase;
    private LevelUpgradeBase LevelUpBase { get { return (levelUpBase == null) ? levelUpBase = GetComponent<LevelUpgradeBase>() : levelUpBase; } }
    public Transform raycastMuzzle;
    [SerializeField]
    private List<SkinnedMeshRenderer> meshes;
    public LayerMask enemyLayer;

    [Header("Ranged Character Params")]
    public RangerAttack rangerAttack;
    #endregion
    #region MonoBehaviourMethods
    private void OnEnable()
    {
        SetDatas(warriorData);
        ColorChange();
    }
    private void Update()
    {
        if (GameManager.Instance.IsStageCompleted)
            return;
        if (canAttack)
            CheckEnemyWithRaycast();
    }
    #endregion
    #region AttackMethods
    private void CheckEnemyWithRaycast()
    {
        RaycastHit hitInfo;
        Debug.DrawRay(raycastMuzzle.position, raycastMuzzle.forward * (attackRange + 0.1f));
        if (Physics.Raycast(raycastMuzzle.position, raycastMuzzle.forward, out hitInfo, attackRange + 0.1f, enemyLayer))
        {
            IDamageable damagable = hitInfo.collider.GetComponent<IDamageable>();
            if (damagable != null)
            {
                closestTarget = damagable;
                AttackTimer();
            }
            else
                lastAttackTime = -Mathf.Infinity;
        }
    }
    public void Attack()
    {
        switch (warriorData.CharacterType)
        {
            case CharacterTypes.Knight:
                closestTarget.TakeDamage(damage);
                break;
            case CharacterTypes.Archer:
                rangerAttack.CreateBullet(enemyLayer, damage, Vector3.forward,transform.position.x);
                break;
            case CharacterTypes.TwoHanded:
                closestTarget.TakeDamage(damage);
                break;
            case CharacterTypes.Mage:
                rangerAttack.CreateBullet(enemyLayer, damage, Vector3.forward, transform.position.x);
                break;
            default:
                break;
        }

    }
    public void AttackEnd()
    {
        isAttacking = false;
        lastAttackTime = Time.time;
    }

    private void AttackTimer()
    {
        if (!isAttacking)
        {
            if (Time.time >= attackRate + lastAttackTime)
            {
                isAttacking = true;
                AnimatorController.AttackAnim();
            }
        }
    }

    #endregion
    #region MyMethods
    public void Initalize(int warriorLevel)
    {
        CharacterManager.Instance.AddSpawnedAlly(gameObject);
        currentLevel = warriorLevel;
        ColorChange();
        LevelUpBase.Upgrade(currentLevel, warriorData);

    }
    public void UpgradeWarrior()
    {
        currentLevel++;
        ColorChange();
        ChangeScale();
        LevelUpBase.Upgrade(currentLevel, warriorData);
        PoolingSystem.Instance.SpawnObject(PoolingSystem.Instance.GetObjectFromName("LevelUpgraded"), transform.position, PoolingSystem.Instance.GetObjectFromName("LevelUpgraded").transform.rotation, transform);
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
        transform.DOScale(transform.localScale * 1.1f, 0.2f);
    }
    public void SetDatas(CharacterData currentData)
    {
        HealthController.SetHealth(currentData.Health);
        damage = currentData.Damage;
        attackRate = currentData.AttackRate;
        attackRange = currentData.AttackRange;
        currentLevel = currentData.warriorLevel;
        canAttack = true;
    }
    #endregion
}
