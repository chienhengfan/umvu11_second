using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMageAttackingIdleState : EnemyBaseState
{
    private readonly int MageAttackIdleHash = Animator.StringToHash("MageAttackIdle");

    private const float TransitionDuration = 0.1f;

    private float howDice = 0f;

    public EnemyMageAttackingIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("MageAttackIdleHash");
        stateMachine.Animator.CrossFadeInFixedTime(MageAttackIdleHash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        FacePlayer();

        float normalizedTime = stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (normalizedTime >= 1f)
        {
            //Debug.Log("normalizedTime: " + normalizedTime);
            //攻擊一次後，切換位置
            if (howDice <= 1f)
            {
                stateMachine.SwitchState(new EnemyMageAttackingMoveToState(stateMachine));
                howDice = 0;
                return;
            }
            else if ((howDice <= 2f && howDice > 1f))
            {
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
                howDice = 0;
                return;
            }
        }

        GetNormalizedTime(stateMachine.Animator);
    }

    public override void Exit()
    {
        
    }
}
