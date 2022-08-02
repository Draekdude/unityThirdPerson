using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

    private const float AnimationDampTime = 0.1f;
    private const float CrossFadeTime = 0.1f;
    private const float ControllerDeadZone = 0.25f;
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");
    private Vector2 dodgingDirectionInput;
    private float remainingDodgeTime;

    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += OnCancel;
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, CrossFadeTime);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        var movement = CalculateMovement(deltaTime);
        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);
        UpdateAnimator(deltaTime);
        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }


    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private void OnDodge()
    {
        if(Time.time - stateMachine.PreviousDodgeTime < stateMachine.DodgeCoolDown) { return; }
        stateMachine.SetDodgeTime(Time.time);
        dodgingDirectionInput = stateMachine.InputReader.MovementValue;
        remainingDodgeTime = stateMachine.DodgeDuration;
    }

    private Vector3 CalculateMovement(float deltaTime)
    {
        var movement = new Vector3();
        if (remainingDodgeTime > 0f)
        {
            movement += stateMachine.transform.right * dodgingDirectionInput.x * (stateMachine.DodgeLength / stateMachine.DodgeDuration);
            movement += stateMachine.transform.forward * dodgingDirectionInput.y * (stateMachine.DodgeLength / stateMachine.DodgeDuration);
            remainingDodgeTime = Mathf.Max(remainingDodgeTime - deltaTime, 0f);
        } else
        {
            movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
        }
        return movement;
    }

    private void UpdateAnimator(float deltaTime)
    {
        float forward = Mathf.Abs(stateMachine.InputReader.MovementValue.y) < ControllerDeadZone ? 0f : Mathf.Sign(stateMachine.InputReader.MovementValue.y);
        float right = Mathf.Abs(stateMachine.InputReader.MovementValue.x) < ControllerDeadZone ? 0f : Mathf.Sign(stateMachine.InputReader.MovementValue.x);
        stateMachine.Animator.SetFloat(TargetingForwardHash, forward, AnimationDampTime, deltaTime);
        stateMachine.Animator.SetFloat(TargetingRightHash, right, AnimationDampTime, deltaTime);
    }

}
