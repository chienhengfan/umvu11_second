using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{

    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int LocomotionMageHash = Animator.StringToHash("Locomotion_Mage");

    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) { }


    public override void Enter()
    {
        int mobIndex = stateMachine.MobEnumIndex;
        if (mobIndex == EnemyStateMachine.MobGroup.ChuCHu.GetHashCode())
        {
            stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
        }
        else if (mobIndex == EnemyStateMachine.MobGroup.AbyssMage.GetHashCode())
        {
            stateMachine.Animator.CrossFadeInFixedTime(LocomotionMageHash, CrossFadeDuration);
        }

    }
    public override void Tick(float deltaTime)
    {
        if (!IsInChasingRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        else if (IsInAttackingRange())
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
            return;
        }

        MoveToPlayer(deltaTime);

        FacePlayer();

        stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {

    }

    private void MoveToPlayer(float deltaTime)
    {
        //往玩家移動的寫法參考老師的
        Vector3 vPlayerPos = stateMachine.Player.transform.position;
        Vector3 vPlayerFor = stateMachine.Player.transform.forward;
        Vector3 nextPlayerPos = vPlayerPos + vPlayerFor * deltaTime;
        Vector3 newFor = (nextPlayerPos - stateMachine.transform.position).normalized;
        stateMachine.transform.position += newFor * stateMachine.MovementSpeed * deltaTime;
    }
}
