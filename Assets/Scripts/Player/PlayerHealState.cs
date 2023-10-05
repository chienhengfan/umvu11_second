using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealState : PlayerBaseState
{
    private int healNumber = 50;
    public PlayerHealState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        if(stateMachine.Health.health <= stateMachine.Health.maxHealth - healNumber)
        stateMachine.Health.health += healNumber;
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {

    }


}
