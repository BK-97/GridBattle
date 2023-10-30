using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    public override void EnterState(StateController stateController)
    {
        stateController.MovementController.Move();
        if (stateController.AttackController.CheckGridInvadable())
            ExitState(stateController);
        if(stateController.AttackController.CheckEnemy())
            ExitState(stateController);
        if (stateController.AttackController.CheckBase())
            ExitState(stateController);

    }

    public override void ExitState(StateController stateController)
    {
        stateController.MovementController.Stop();

        if (stateController.AttackController.CheckGridInvadable())
        {
            stateController.ChangeState(stateController.captureState);
        }

        else if (stateController.AttackController.CheckEnemy())
        {
            stateController.ChangeState(stateController.attackState);

        }
        else if (stateController.AttackController.CheckBase())
        {
            stateController.ChangeState(stateController.idleState);

        }
    }

    public override void UpdateState(StateController stateController)
    {
        stateController.MovementController.Move();

        if (stateController.AttackController.CheckBase())
            ExitState(stateController);

        if (stateController.AttackController.CheckEnemy())
            ExitState(stateController);

        if (stateController.AttackController.CheckGridInvadable())
            ExitState(stateController);

    }
}
