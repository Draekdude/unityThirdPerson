using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    public PlayerTestState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {
        var input = stateMachine.InputReader.MovementValue;
        Vector3 movement = new Vector3(input.x, 0, input.y);
        stateMachine.CharacterController.Move(movement * deltaTime * stateMachine.FreeLookMovementSpeed);
        if (input == Vector2.zero) {
            stateMachine.Animator.SetFloat("FreeLookSpeed", 0, 0.1f, deltaTime);
            return;
        }
        stateMachine.Animator.SetFloat("FreeLookSpeed", 1, 0.1f, deltaTime);
        stateMachine.transform.rotation = Quaternion.LookRotation(movement); 
    }

    public override void Exit()
    {

    }

}
