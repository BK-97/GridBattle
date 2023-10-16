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
    #region Params
    public Transform raycastPoint;
    #endregion
    #region GetSet
    private EnemyAttackController attackController;
    public EnemyAttackController AttackController { get { return (attackController == null) ? attackController = GetComponent<EnemyAttackController>() : attackController; } }
    private EnemyMovementController movementController;
    public EnemyMovementController MovementController { get { return (movementController == null) ? movementController = GetComponent<EnemyMovementController>() : movementController; } }
    private EnemyHealthController healthController;
    public EnemyHealthController HealthController { get { return (healthController == null) ? healthController = GetComponent<EnemyHealthController>() : healthController; } }
    private Invader invader;
    public Invader Invader { get { return (invader == null) ? invader = GetComponent<Invader>() : invader; } }
    #endregion
    #region States
    private BaseState currentState;
    public IdleState idleState=new IdleState();
    public MoveState moveState=new MoveState();
    public GridCaptureState captureState=new GridCaptureState();
    public AttackState attackState=new AttackState();
    public DeathState deathState=new DeathState();
    #endregion
    #region MonoBehaviourMethods
    private void OnEnable()
    {
        //Because of my pool system, we have to set data every time an object becomes enabled
        SetDatas();
        ChangeState(idleState);
    }
    private void Update()
    {
        if (GameManager.Instance.IsStageCompleted)
            ChangeState(idleState);
        else
        {
            if (currentState != null)
                currentState.UpdateState(this);
        }
    }
    #endregion
    #region MyMethods

    private void SetDatas()
    {
        AttackController.DataSet(enemyData);
        MovementController.SetSpeed(enemyData.MoveSpeed);
        HealthController.SetHealth(enemyData.Health);
    }

    public void ChangeState(BaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
    #endregion
}
