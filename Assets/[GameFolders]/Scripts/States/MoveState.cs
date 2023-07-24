using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    public override void EnterState(StateController stateController)
    {
        stateController.MovementController.Move();
        if (stateController.AttackController.CheckGrid())
            ExitState(stateController);
    }

    public override void ExitState(StateController stateController)
    {
        stateController.MovementController.Stop();
        if (stateController.AttackController.CheckEnemy())
            stateController.ChangeState(stateController.attackState);
        else
            stateController.ChangeState(stateController.captureState);
    }

    public override void UpdateState(StateController stateController)
    {
        stateController.MovementController.Move();
        if (stateController.AttackController.CheckGrid())
            ExitState(stateController);
    }
}
