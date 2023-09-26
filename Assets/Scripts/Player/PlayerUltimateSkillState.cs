using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateSkillState : PlayerBaseState
{
    private float previousFrameTime;

    private readonly int UltimateSkillHash = Animator.StringToHash("UltimateSkill");
    private const float CrossFadeDuration = 0.1f;
    

    public PlayerUltimateSkillState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(UltimateSkillHash, CrossFadeDuration);

    }

    public override void Tick(float deltaTime)
    {

        float normalizedTime = stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

        if (normalizedTime >= 1f)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }



    }
    public override void Exit()
    {

    }


}
