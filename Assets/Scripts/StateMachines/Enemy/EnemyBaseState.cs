using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        var movement = stateMachine.ForceReceiver.Movement + motion;
        stateMachine.Controller.Move(movement * deltaTime);
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected bool IsInChaseRange()
    {
        if (stateMachine.PlayerHealth.IsDead) { return false; }
        var distance = Vector3.Distance(stateMachine.transform.position, stateMachine.PlayerHealth.transform.position);
        return distance <= stateMachine.PlayerChasingRange;
    }

    protected void FacePlayer()
    {
        var lookPosition = stateMachine.PlayerHealth.transform.position - stateMachine.transform.position;
        lookPosition.y = 0;
        stateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
    }

}
