using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    private const float CrossFadeTime = 0.1f;
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private float _duration = 1f;

    public PlayerImpactState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeTime);
    }

    public override void Tick(float deltaTime)
    {
        _duration -= deltaTime;
        if (_duration <= 0f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {

    }
}
