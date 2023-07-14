using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    #region Params
    private int moveSpeed;
    public bool canMove;
    #endregion
    #region SetMethods
    public void SetSpeed(int speed)
    {
        moveSpeed = speed;
        canMove = true;
    }
    #endregion
    #region MoveMethods
    public void Move()
    {
        if (!CheckForwardIsEmpty())
            return;
        if(canMove)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

    }
    #endregion
    #region CheckMethods
    private bool CheckForwardIsEmpty()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 2, LayerMask.GetMask("Enemy")))
        {
            return false;
        }
        else
            return true;
    }
    #endregion
}
