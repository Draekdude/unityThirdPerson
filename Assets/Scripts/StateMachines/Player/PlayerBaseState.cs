using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        var movement = stateMachine.ForceReceiver.Movement + motion;
        stateMachine.CharacterController.Move(movement * deltaTime);
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void FaceTarget()
    {
        if(stateMachine.Targeter.CurrentTarget == null) { return; }
        var lookPosition = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
        lookPosition.y = 0;
        stateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
    }
}
