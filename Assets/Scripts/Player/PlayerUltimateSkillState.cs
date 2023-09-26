using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateSkillState : PlayerBaseState
{
    private GameObject genyuBall;
    private float frozenMoveTime = 1f;

    private readonly int UltimateSkillHash = Animator.StringToHash("UltimateSkill");
    private const float CrossFadeDuration = 0.1f;

    public PlayerUltimateSkillState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        genyuBall = stateMachine.GenyuBall;
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(UltimateSkillHash, CrossFadeDuration);
        genyuBall.SetActive(true);

    }

    public override void Tick(float deltaTime)
    {
        Move(frozenMoveTime);
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));

    }
    public override void Exit()
    {
        throw new System.NotImplementedException();
    }


}
