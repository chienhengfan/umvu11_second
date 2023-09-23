using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingStateTest01 : EnemyBaseStateTest01
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    public EnemyChasingStateTest01(EnemyStateMachineTest01 stateMachine) : base(stateMachine) { }


    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);

    }
    public override void Tick(float deltaTime)
    {
        if (!IsInChasingRange())
        {
            stateMachine.SwitchState(new EnemyIdleStateTest01(stateMachine));
            return;
        }
        else if (IsInAttackingRange())
        {
            stateMachine.SwitchState(new EnemyAttackingStateTest01(stateMachine));
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
        Vector3 newFor = (nextPlayerPos-stateMachine.transform.position).normalized;
        stateMachine.transform.position += newFor * stateMachine.MovementSpeed * deltaTime;
    }

    private bool IsInAttackingRange()
    {
        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
    }
}
