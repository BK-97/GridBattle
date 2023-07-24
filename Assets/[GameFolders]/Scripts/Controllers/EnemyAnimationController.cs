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
    }
    #region AnimationEventMethods
    public void AttackEvent()
    {
        MoveAnim(0);
        attackController.Attack();
    }
    public void AttackEnd()
    {
        animator.SetBool("Attack", false);
        attackController.AttackEnd();
    }
    #endregion
    #region AnimationMethods
    public void AttackAnim()
    {
        animator.SetBool("Attack", true);
    }
    public void MoveAnim(float speed)
    {
        animator.SetFloat("MoveSpeed", speed);
    }
    public void DeathAnim()
    {
        MoveAnim(0);
        animator.Rebind();
        animator.SetTrigger("Death");

    }
    #endregion
}
