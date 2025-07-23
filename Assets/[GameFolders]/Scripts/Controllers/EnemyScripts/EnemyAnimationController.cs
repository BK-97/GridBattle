using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    #region Params
    public Animator animator;
    private EnemyAttackController attackController;
    #endregion
    private void Start()
    {
        attackController = GetComponentInParent<EnemyAttackController>();
        animator.applyRootMotion = false;

    }
    private void OnEnable()
    {
        animator.applyRootMotion = false;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.localPosition= Vector3.zero;
        
        Debug.Log("local rotation=> "+transform.localRotation);
        GameManager.Instance.OnStageLoose.AddListener(CheerAnim);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnStageLoose.RemoveListener(CheerAnim);
    }
    #region AnimationEventMethods
    public void AttackEvent()
    {
        MoveAnim(0);
        attackController.Attack();
    }
    public void AttackEnd()
    {
        animator.SetBool(AnimKeys.ATTACK_BOOL, false);
        attackController.AttackEnd();
    }
    #endregion
    #region AnimationMethods
    public void AttackAnim()
    {
        animator.SetBool(AnimKeys.ATTACK_BOOL, true);
    }
    public void MoveAnim(float speed)
    {
        animator.SetFloat(AnimKeys.SPEED, speed);
    }
    public void InvadeAnim(bool status)
    {
        animator.SetBool(AnimKeys.INVADE, status);
    }
    public void CheerAnim()
    {
        MoveAnim(0);
        animator.Rebind();
        animator.SetTrigger(AnimKeys.CHEER);
    }
    public void DeathAnim()
    {
        MoveAnim(0);
        animator.Rebind();
        animator.applyRootMotion = true;
        animator.SetTrigger(AnimKeys.DIE);

    }
    #endregion
}
