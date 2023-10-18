using UnityEngine;

public static class AnimKeys 
{
    public static readonly int ATTACK_BOOL = Animator.StringToHash("Attack");
    public static readonly int GET_HIT_ANIMATION = Animator.StringToHash("Hit");
    public static readonly int SPEED = Animator.StringToHash("MoveSpeed");
    public static readonly int DIE = Animator.StringToHash("Death");
    public static readonly int INVADE = Animator.StringToHash("Invade");
    public static readonly int CHEER = Animator.StringToHash("Cheer");
}
