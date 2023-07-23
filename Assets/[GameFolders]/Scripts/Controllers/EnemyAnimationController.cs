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
    public void MoveAnim()
    {
        animator.SetFloat("MoveSpeed",1);
    }
    public void DeathAnim()
    {
        animator.Rebind();
        animator.SetTrigger("Death");

    }
    #endregion
}
