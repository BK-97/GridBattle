using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public override void EnterState(StateController stateController)
    {
        stateController.MovementController.Stop();
        if (GameManager.Instance.IsStageCompleted)
            return;
        ExitState(stateController);
    }

    public override void ExitState(StateController stateController)
    {
        stateController.ChangeState(stateController.moveState);
    }

    public override void UpdateState(StateController stateController)
    {

    }
}
