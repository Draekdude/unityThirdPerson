using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

    private const float AnimationDampTime = 0.1f;
    private const float ControllerDeadZone = 0.25f;
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += OnCancel;
        stateMachine.Animator.Play(TargetingBlendTreeHash);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine));
            return;
        }
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        var movement = CalculateMovement();
        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);
        UpdateAnimator(deltaTime);
        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
    }


    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        var movement = new Vector3();
        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
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
