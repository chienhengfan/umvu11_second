using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleStateTest01 : EnemyBaseStateTest01
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int LocomotionMageHash = Animator.StringToHash("Locomotion_Mage");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    public EnemyIdleStateTest01(EnemyStateMachineTest01 stateMachine) : base(stateMachine) { }


    public override void Enter()
    {
        int mobIndex = stateMachine.MobEnumIndex;
        if (mobIndex == EnemyStateMachineTest01.MobGroup.ChuCHu.GetHashCode())
        {
            stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
        }
        else if (mobIndex == EnemyStateMachineTest01.MobGroup.AbyssMage.GetHashCode())
        {
            stateMachine.Animator.CrossFadeInFixedTime(LocomotionMageHash, CrossFadeDuration);
        }

    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if (IsInChasingRange())
        {
            stateMachine.SwitchState(new EnemyChasingStateTest01(stateMachine));
            return;
        }

        FacePlayer();

        stateMachine.Animator.SetFloat(SpeedHash, 0f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {

    }
}
