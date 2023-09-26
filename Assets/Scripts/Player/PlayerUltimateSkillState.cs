using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateSkillState : PlayerBaseState
{

    private readonly int UltimateSkillHash = Animator.StringToHash("UltimateSkill");
    private const float CrossFadeDuration = 0.1f;
    private bool IsAnimationPlaying= false;

    public PlayerUltimateSkillState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(UltimateSkillHash, CrossFadeDuration);
        IsAnimationPlaying = IsInAnimation(stateMachine.Animator, "UltimateSkill");

    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        Debug.LogError(IsAnimationPlaying);
        //if(CheckAnimationIsOver(stateMachine.Animator, "UltimateSkill"))
        //{
        //    stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        //}

    }
    public override void Exit()
    {

    }


}
