using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLadyAttacking01State : EnemyBaseState
{
    private readonly int AttackLady01Hash = Animator.StringToHash("LadyAttack01");

    private const float TransitionDuration = 0.1f;

    private int howDice = 0;
    int r;

    public EnemyLadyAttacking01State(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        foreach (var weapon in stateMachine.Weapons)
        {
            weapon.SetAttack(stateMachine.AttackDamage);
        }

        stateMachine.Animator.CrossFadeInFixedTime(AttackLady01Hash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }

        //攻擊一次後，切換位置
        //if (howDice == 1)
        //{
        //    stateMachine.SwitchState(new EnemyAttackingMoveToStateTest01(stateMachine));
        //    howDice = 0;
        //    return;
        //}
        //else if ((howDice == 2))
        //{
        //    stateMachine.SwitchState(new EnemyAttackingMoveToStateTest01(stateMachine));
        //    howDice = 0;
        //    return;
        //}
    }

    public override void Exit()
    {

    }
}
