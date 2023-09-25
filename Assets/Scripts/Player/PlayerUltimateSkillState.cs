using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateSkillState : PlayerBaseState
{
    private GameObject skill;
    private ParticleSystem particle;

    public PlayerUltimateSkillState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        skill = stateMachine.Ultimateskill;
        Debug.LogError(skill);
    }

    public override void Enter()
    {
        skill.SetActive(true);
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void Tick(float deltaTime)
    {
        particle = skill.GetComponentInChildren<ParticleSystem>();
        Debug.LogError(particle);
        particle.Play();
    }
}
