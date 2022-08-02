using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private const float CrossFadeTime = 0.1f;
    private readonly int JumpHash = Animator.StringToHash("Jump");
    private Vector3 momentum; 

    public PlayerJumpingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeTime);
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);
        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);
        if(stateMachine.CharacterController.velocity.y <= 0)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }
        FaceTarget();
    }

    public override void Exit()
    {

    }

}
