using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    public override void EnterState(StateController stateController)
    {
        stateController.MovementController.Move();
        if (stateController.AttackController.CheckEnemy())
            ExitState(stateController);
    }

    public override void ExitState(StateController stateController)
    {
        stateController.ChangeState(stateController.attackState);
    }

    public override void UpdateState(StateController stateController)
    {
        stateController.MovementController.Move();
        if (stateController.AttackController.CheckEnemy())
            ExitState(stateController);
    }
}
