public class DeathState : BaseState
{
    public override void EnterState(StateController stateController)
    {
        stateController.MovementController.canMove = false;
        stateController.AttackController.canAttack = false;
        stateController.AttackController.Invader.CancelInvade();
    }

    public override void ExitState(StateController stateController)
    {

    }

    public override void UpdateState(StateController stateController)
    {

    }
}
