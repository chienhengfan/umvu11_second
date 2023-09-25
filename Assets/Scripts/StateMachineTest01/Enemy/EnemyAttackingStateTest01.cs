using RPGCharacterAnims.Lookups;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingStateTest01 : EnemyBaseStateTest01
{
    private readonly int AttackHash = Animator.StringToHash("Attack");
    private readonly int MageAttackHash = Animator.StringToHash("MageAttack");

    private const float TransitionDuration = 0.1f;

    private int howDice = 0;

    public EnemyAttackingStateTest01(EnemyStateMachineTest01 stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        foreach (var weapon in stateMachine.Weapons)
        {
            weapon.SetAttack(stateMachine.AttackDamage);
        }

        int mobIndex = stateMachine.MobEnumIndex;
        if (mobIndex == EnemyStateMachineTest01.MobGroup.ChuCHu.GetHashCode())
        {
            stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
        }
        else if (mobIndex == EnemyStateMachineTest01.MobGroup.AbyssMage.GetHashCode())
        {
            stateMachine.Animator.CrossFadeInFixedTime(MageAttackHash, TransitionDuration);
        }

    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingStateTest01(stateMachine));
            return;
            howDice = Random.Range(0, 2);
        }

        //�����@����A������m
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
