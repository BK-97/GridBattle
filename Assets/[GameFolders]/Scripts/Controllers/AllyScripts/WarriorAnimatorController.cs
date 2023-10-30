using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAnimatorController : MonoBehaviour
{
    #region Params
    public Animator animator;
    private WarriorController warriorController;
    #endregion
    private void Start()
    {
        warriorController = GetComponentInParent<WarriorController>();
        animator.applyRootMotion = false;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    #region AnimationEventMethods
    public void AttackEvent()
    {
        warriorController.Attack();
    }
    public void AttackEnd()
    {
        animator.SetBool("Attack", false);
        warriorController.AttackEnd();
    }
    public void HitEnd()
    {
        warriorController.ControllerOn();
    }
    #endregion
    #region AnimationMethods
    public void AttackAnim()
    {
        animator.SetBool("Attack", true);
    }
    public void MoveAnim()
    {

    }
    public void DeathAnim()
    {
        animator.Rebind();
        animator.applyRootMotion = true;
        animator.SetTrigger("Death");

    }
    public void HitAnim()
    {
        animator.SetTrigger("Hit");
    }
    #endregion
}
