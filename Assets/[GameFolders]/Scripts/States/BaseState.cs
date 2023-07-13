using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(StateController stateController);
    public abstract void UpdateState(StateController stateController);
    public abstract void ExitState(StateController stateController);
}

