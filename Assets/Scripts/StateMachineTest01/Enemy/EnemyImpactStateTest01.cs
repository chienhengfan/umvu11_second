using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactStateTest01 : EnemyBaseStateTest01
{
    private readonly int ImpactHash = Animator.StringToHash("Impact");

    private const float CrossFadeDuration = 0.1f;
    private float duration = 1f;
    public EnemyImpactStateTest01(EnemyStateMachineTest01 stateMachine) : base(stateMachine) { }


    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= Time.deltaTime;

        if (duration <= 0f)
        {
            stateMachine.SwitchState(new EnemyIdleStateTest01(stateMachine));
            return;
        }
    }

    public override void Exit()
    {

    }
}
