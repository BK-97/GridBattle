using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAttackController))]
[RequireComponent(typeof(EnemyHealthController))]
[RequireComponent(typeof(EnemyMovementController))]
public class StateController : MonoBehaviour
{
    #region Datas
    public EnemyData enemyData;
    #endregion
    #region GetSet
    private EnemyAttackController attackController;
    public EnemyAttackController AttackController { get { return (attackController == null) ? attackController = GetComponent<EnemyAttackController>() : attackController; } }
    private EnemyMovementController movementController;
    public EnemyMovementController MovementController { get { return (movementController == null) ? movementController = GetComponent<EnemyMovementController>() : movementController; } }
    private EnemyHealthController healthController;
    public EnemyHealthController HealthController { get { return (healthController == null) ? healthController = GetComponent<EnemyHealthController>() : healthController; } }
    #endregion
    #region States
    private BaseState currentState;
    public IdleState idleState=new IdleState();
    public MoveState moveState=new MoveState();
    public AttackState attackState=new AttackState();
    #endregion
    
    private void Start()
    {
        SetDatas();
        ChangeState(idleState);
    }
    private void SetDatas()
    {
        AttackController.DataSet(enemyData);
        MovementController.SetSpeed(enemyData.MoveSpeed);
        HealthController.SetHealth(enemyData.Health);
    }
    private void Update()
    {
        if(currentState!=null)
            currentState.UpdateState(this);
    }
    public void ChangeState(BaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

}
