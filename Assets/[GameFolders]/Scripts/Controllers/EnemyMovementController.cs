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
        if (canMove)
        {
            float distance= CheckDistanceForward();
            Vector3 movementDirection = Vector3.forward;
            if (distance == 0)
                currentSpeed = 0;
            else
                currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed* distance, Time.deltaTime);
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
    public LayerMask stopMask;
    private float CheckDistanceForward()
    {
        RaycastHit hitInfo;

        Debug.DrawRay(StateController.raycastPoint.position, StateController.raycastPoint.forward);

        if (Physics.Raycast(StateController.raycastPoint.position, StateController.raycastPoint.forward, out hitInfo, 2, stopMask))
        {

            float distance = Vector3.Distance(StateController.raycastPoint.position, hitInfo.point);

            if (distance <= 1)
                return 0;
            else
                return distance - 1;
        }
        return 1;

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
