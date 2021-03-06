using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeTime = 0.1f;
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine){}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeTime);
    }

    public override void Tick(float deltaTime){
        if (IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }
        Move(deltaTime);
        FacePlayer();
        stateMachine.Animator.SetFloat(SpeedHash, 0f, AnimatorDampTime, deltaTime);
    }

    public override void Exit() { }
}
