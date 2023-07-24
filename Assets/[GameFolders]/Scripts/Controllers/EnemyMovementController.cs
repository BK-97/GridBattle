using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    #region Params
    private int maxSpeed;
    private float currentSpeed;
    public bool canMove;
    private StateController stateController;
    public StateController StateController { get { return (stateController == null) ? stateController = GetComponent<StateController>() : stateController; } }
    private EnemyAnimationController animationController;
    public EnemyAnimationController AnimationController { get { return (animationController == null) ? animationController = GetComponentInChildren<EnemyAnimationController>() : animationController; } }
    #endregion
    #region SetMethods
    public void SetSpeed(int speed)
    {
        maxSpeed = speed;
        canMove = true;
    }
    #endregion
    #region MoveMethods
    public void Move()
    {
        if (!CheckForwardIsEmpty())
            return;
        if (canMove)
        {
            Vector3 movementDirection = Vector3.forward;
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime);
            transform.Translate(movementDirection * currentSpeed * Time.deltaTime);
        }

        AnimationController.MoveAnim(currentSpeed);
    }
    public void Stop()
    {
        currentSpeed = 0;
        AnimationController.MoveAnim(currentSpeed);
    }
    #endregion
    #region CheckMethods
    private bool CheckForwardIsEmpty()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(StateController.raycastPoint.position, StateController.raycastPoint.forward, out hitInfo, 1, LayerMask.GetMask("Enemy")))
        {
            return false;
        }
        else
            return true;
    }
    public bool IsDestinationReached(Vector3 destination)
    {
        if (Vector3.Distance(transform.position, destination) < 1)
            return true;
        else
            return false;
    }
    #endregion
}
