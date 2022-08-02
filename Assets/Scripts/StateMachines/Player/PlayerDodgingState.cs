using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{
    private const float CrossFadeTime = 0.1f;
    private readonly int DodgingBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
    private readonly int DodgingForwardHash = Animator.StringToHash("DodgeForward");
    private readonly int DodgingRightHash = Animator.StringToHash("DodgeRight");
    private Vector3 _dodgingDirectionInput;
    private float _remainingDodgeTime;

    public PlayerDodgingState(PlayerStateMachine playerStateMachine, Vector3 dodgingDirectionInput) : base(playerStateMachine)
    {
        _dodgingDirectionInput = dodgingDirectionInput;
    }

    public override void Enter()
    {
        _remainingDodgeTime = stateMachine.DodgeDuration;
        stateMachine.Animator.SetFloat(DodgingForwardHash, _dodgingDirectionInput.y);
        stateMachine.Animator.SetFloat(DodgingRightHash, _dodgingDirectionInput.x);
        stateMachine.Animator.CrossFadeInFixedTime(DodgingBlendTreeHash, CrossFadeTime);
        stateMachine.Health.SetInvulnerable(true);
    }

    public override void Tick(float deltaTime)
    {
        var movement = new Vector3();
        movement += stateMachine.transform.right * _dodgingDirectionInput.x * (stateMachine.DodgeLength / stateMachine.DodgeDuration);
        movement += stateMachine.transform.forward * _dodgingDirectionInput.y * (stateMachine.DodgeLength / stateMachine.DodgeDuration);
        Move(movement, deltaTime);
        FaceTarget();
        _remainingDodgeTime -= deltaTime;
        if (_remainingDodgeTime <= 0f)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
    }
}
