using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    private int moveSpeed;
    public bool canMove;
    public void SetSpeed(int speed)
    {
        moveSpeed = speed;
        canMove = true;
    }
    public void Move()
    {
        if(canMove)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

}
