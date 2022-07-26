using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private Attack attack;
    private bool addedForce;
    public PlayerAttackingState(PlayerStateMachine playerStateMachine, int attackIndex) : base(playerStateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.WeaponDamage.SetAttack(attack.Damage, attack.KnockBack);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();
        float normalizedTime = GetNormalizedTime(stateMachine.Animator);

        if(normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }
            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if(stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
        previousFrameTime = normalizedTime;
    }

    public override void Exit()
    {

    }

    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) { return; }
        if (normalizedTime < attack.ComboAttackTime) { return; }
        stateMachine.SwitchState
        (
            new PlayerAttackingState(stateMachine, attack.ComboStateIndex)
        );
    }

    private void TryApplyForce()
    {
        if (addedForce) { return; }
        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);
        addedForce = true;
    }

}
