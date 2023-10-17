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
    float lastAttackTime=-Mathf.Infinity;
    private bool canAttack;
    private IDamageable closestTarget;
    private WarriorHealthController healthController;
    public WarriorHealthController HealthController { get { return (healthController == null) ? healthController = GetComponent<WarriorHealthController>() : healthController; } }

    [HideInInspector]
    public WarriorAnimatorController animatorController;
    private LevelUpgradeBase levelUpBase;
    public Transform raycastMuzzle;
    [SerializeField]
    private List<SkinnedMeshRenderer> meshes;
    public LayerMask enemyLayer;

    [Header("Ranged Character Params")]
    public RangerAttack rangerAttack;
    [Header("Close Combat Character Params")]
    public ParticleSystem trailParticle;
    #endregion
    #region MonoBehaviourMethods
    private void Start()
    {
        levelUpBase = GetComponent<LevelUpgradeBase>();
        animatorController = GetComponentInChildren<WarriorAnimatorController>();
    }
    private void OnEnable()
    {
        //Because of my pool system, we have to set data every time an object becomes enabled
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
        Debug.DrawRay(raycastMuzzle.position, raycastMuzzle.forward*(attackRange+0.1f));
        if (Physics.Raycast(raycastMuzzle.position, raycastMuzzle.forward, out hitInfo, attackRange+0.1f, enemyLayer))
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
                if (trailParticle != null)
                    trailParticle.Play();
                break;
            case CharacterTypes.Archer:
                rangerAttack.CreateBullet(enemyLayer,damage, Vector3.forward);
                break;
            case CharacterTypes.TwoHanded:
                closestTarget.TakeDamage(damage);
                if (trailParticle != null)
                    trailParticle.Play();
                break;
            case CharacterTypes.Mage:
                rangerAttack.CreateBullet(enemyLayer, damage, Vector3.forward);
                break;
            default:
                break;
        }
    }
    public void AttackEnd()
    {
        Debug.Log("test");
        isAttacking = false;
    }

    private void AttackTimer()
    {
        if (!isAttacking)
        {
            if (Time.time >= attackRate + lastAttackTime)
            {
                isAttacking = true;
                lastAttackTime = Time.time;
                animatorController.AttackAnim();
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
        PoolingSystem.Instance.SpawnObject(PoolingSystem.Instance.GetObjectFromName("LevelUpgraded"),transform.position,Quaternion.identity,transform);
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
