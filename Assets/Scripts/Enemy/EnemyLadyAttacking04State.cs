using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLadyAttacking04State : EnemyBaseState
{
    private readonly int AttackLady04Hash = Animator.StringToHash("LadyAttack04");

    private const float TransitionDuration = 0.1f;

    private int howDice = 0;
    int r;

    public EnemyLadyAttacking04State(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        foreach (var weapon in stateMachine.Weapons)
        {
            weapon.SetAttack(stateMachine.AttackDamage);
        }

        stateMachine.Animator.CrossFadeInFixedTime(AttackLady04Hash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            Debug.Log("ChangeToChase");
            return;
        }
    }

    public override void Exit()
    {

    }
}
