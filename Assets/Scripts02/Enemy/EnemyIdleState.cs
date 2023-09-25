using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int LocomotionMageHash = Animator.StringToHash("Locomotion_Mage");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    
    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }
    

    public override void Enter()
    {
<<<<<<< HEAD:Assets/Scripts02/Enemy/EnemyIdleState.cs
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
        
=======
        int mobIndex = stateMachine.MobEnumIndex;
        if (mobIndex == EnemyStateMachineTest01.MobGroup.ChuCHu.GetHashCode())
        {
            stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
        }
        else if (mobIndex == EnemyStateMachineTest01.MobGroup.AbyssMage.GetHashCode())
        {
            stateMachine.Animator.CrossFadeInFixedTime(LocomotionMageHash, CrossFadeDuration);
        }

>>>>>>> ecc60abc53d8dd8df62ad1db3331fbeff1705e69:Assets/Scripts/StateMachineTest01/Enemy/EnemyIdleStateTest01.cs
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

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
