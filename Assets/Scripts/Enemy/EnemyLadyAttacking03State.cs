using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLadyAttacking03State : EnemyBaseState
{
    private readonly int AttackLady03Hash = Animator.StringToHash("LadyAttack03");

    private const float TransitionDuration = 0.1f;

    private int howDice = 0;
    int r;

    public EnemyLadyAttacking03State(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        foreach (var weapon in stateMachine.Weapons)
        {
            weapon.SetAttack(stateMachine.AttackDamage);
        }

        stateMachine.Animator.CrossFadeInFixedTime(AttackLady03Hash, TransitionDuration);
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
