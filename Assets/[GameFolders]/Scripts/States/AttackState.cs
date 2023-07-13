using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public override void EnterState(StateController stateController)
    {
        if (stateController.AttackController.CheckEnemy())
            stateController.AttackController.AttackTimer();
    }

    public override void ExitState(StateController stateController)
    {
        stateController.ChangeState(stateController.moveState);
    }

    public override void UpdateState(StateController stateController)
    {
        if (stateController.AttackController.CheckEnemy())
            stateController.AttackController.AttackTimer();
        else
            ExitState(stateController);
    }
}
