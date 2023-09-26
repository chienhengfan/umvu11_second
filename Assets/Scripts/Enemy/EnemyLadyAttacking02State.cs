using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLadyAttacking02State : EnemyBaseState
{
    private readonly int AttackLady02Hash = Animator.StringToHash("LadyAttack02");

    private const float TransitionDuration = 0.1f;

    private int howDice = 0;
    int r;

    public EnemyLadyAttacking02State(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        foreach (var weapon in stateMachine.Weapons)
        {
            weapon.SetAttack(stateMachine.AttackDamage);
        }

        stateMachine.Animator.CrossFadeInFixedTime(AttackLady02Hash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {

    }
}
