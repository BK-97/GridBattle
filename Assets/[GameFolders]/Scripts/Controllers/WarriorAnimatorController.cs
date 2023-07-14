using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAnimatorController : MonoBehaviour
{
    public Animator animator;
    private WarriorController warriorController;
    private void Start()
    {
        warriorController = GetComponentInParent<WarriorController>();
    }
    public void AttackAnim()
    {
        animator.SetBool("Attack",true);
    }
    public void AttackEvent()
    {
        warriorController.Attack();
    }
    public void AttackEnd()
    {
        animator.SetBool("Attack", false);
        warriorController.AttackEnd();
    }
    public void MoveAnim()
    {

    }
    public void DeathAnim()
    {

    }
}
