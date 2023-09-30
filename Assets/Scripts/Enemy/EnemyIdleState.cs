using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyIdleState : EnemyBaseState
{

    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int LocomotionMageHash = Animator.StringToHash("Locomotion_Mage");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    private float verticalVelocity = 0f;
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }


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
        else if(mobIndex == EnemyStateMachine.MobGroup.CHuCHuCrossbow.GetHashCode())
        {
            stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
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
        Move(Vector3.up * verticalVelocity, Time.deltaTime);

        if (IsInChasingRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }

        FacePlayer();

        stateMachine.Animator.SetFloat(SpeedHash, 0f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {

    }
}
