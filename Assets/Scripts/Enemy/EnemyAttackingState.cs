using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");
    private readonly int MageAttackHash = Animator.StringToHash("MageAttack");

    private const float TransitionDuration = 0.1f;

    private int howDice = 0;
    private float verticalVelocity = 0f;

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        foreach (var weapon in stateMachine.Weapons)
        {
            weapon.SetAttack(stateMachine.AttackDamage);
        }

        int mobIndex = stateMachine.MobEnumIndex;
        if (mobIndex == EnemyStateMachine.MobGroup.ChuCHu.GetHashCode())
        {
            stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
        }
        else if (mobIndex == EnemyStateMachine.MobGroup.AbyssMage.GetHashCode())
        {
            stateMachine.Animator.CrossFadeInFixedTime(MageAttackHash, TransitionDuration);
        }

    }

    public override void Tick(float deltaTime)
    {
        if (verticalVelocity < 0f && stateMachine.Controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        Debug.Log(stateMachine.gameObject.name + verticalVelocity);
        Move(Vector3.up * verticalVelocity, deltaTime);


        if (GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
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
