using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadStateTest01 : EnemyBaseStateTest01
{
    private readonly int DeadHash = Animator.StringToHash("Dead");

    private const float CrossFadeDuration = 0.1f;
    public EnemyDeadStateTest01(EnemyStateMachineTest01 stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //stateMachine.Weapon.gameObject.SetActive(false);
        //GameObject.Destroy(stateMachine.Target);
        stateMachine.Animator.CrossFadeInFixedTime(DeadHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {

    }

}
