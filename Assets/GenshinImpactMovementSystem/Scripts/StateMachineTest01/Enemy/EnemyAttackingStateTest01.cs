using RPGCharacterAnims.Lookups;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingStateTest01 : EnemyBaseStateTest01
{
    private readonly int AttackHash = Animator.StringToHash("Attack");

    private const float TransitionDuration = 0.1f;

    private int howDice = 0;

    public EnemyAttackingStateTest01(EnemyStateMachineTest01 stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        foreach (var weapon in stateMachine.weapons)
        {
            weapon.SetAttack(stateMachine.AttackDamage);
        }
        
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            
            howDice = Random.Range(0, 2);
        }

        //攻擊一次後，切換位置
        if (howDice == 1)
        {
            stateMachine.SwitchState(new EnemyAttackingMoveToStateTest01(stateMachine));
            howDice = 0;
            return;
        }
        else if ((howDice == 2))
        {
            stateMachine.SwitchState(new EnemyAttackingMoveToStateTest01(stateMachine));
            howDice = 0;
            return;
        }
    }

    public override void Exit()
    {

    }
}
