using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCaptureState : BaseState
{
    public override void EnterState(StateController stateController)
    {
        if(!stateController.MovementController.IsDestinationReached(stateController.Invader.targetGrid.transform.position))
            stateController.MovementController.Move();
        stateController.Invader.StartInvading();
    }

    public override void ExitState(StateController stateController)
    {
        stateController.Invader.CancelInvade();
        stateController.ChangeState(stateController.moveState);

    }

    public override void UpdateState(StateController stateController)
    {
        if (stateController.AttackController.CheckEnemy())
            ExitState(stateController);

        if (!stateController.AttackController.CheckGrid())
            ExitState(stateController);

        if (!stateController.MovementController.IsDestinationReached(stateController.Invader.targetGrid.transform.position))
            stateController.MovementController.Move();
        else
        {
            stateController.MovementController.Stop();
            stateController.Invader.Invading();
        }
    }
}
