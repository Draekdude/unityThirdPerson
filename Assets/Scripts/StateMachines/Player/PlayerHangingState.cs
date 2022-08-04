using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    private const float CrossFadeTime = 0.1f;
    private readonly int HangingHash = Animator.StringToHash("Hanging");
    private Vector3 _ledgeForward;

    public PlayerHangingState(PlayerStateMachine playerStateMachine, Vector3 ledgeForward) : base(playerStateMachine)
    {
        _ledgeForward = ledgeForward;
    }

    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(_ledgeForward);
        stateMachine.Animator.CrossFadeInFixedTime(HangingHash, CrossFadeTime);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y < 0f)
        {
            stateMachine.CharacterController.Move(Vector3.zero);
            stateMachine.ForceReceiver.Reset();
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }

    }

    public override void Exit()
    {

    }

    private void OnDrop()
    {
        stateMachine.SwitchState(new PlayerFallingState(stateMachine));
    }
}
