using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");
    private const float CrossFadeTime = 0.1f;

    public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine){}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, CrossFadeTime);
        stateMachine.Weapon.SetAttack(stateMachine.AttackDamage, stateMachine.KnockBack);
    }

    public override void Tick(float deltaTime)
    {
        if(GetNormalizedTime(stateMachine.Animator, "Attack") >= 1)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
        FacePlayer();
    }

    public override void Exit()
    {

    }

}
