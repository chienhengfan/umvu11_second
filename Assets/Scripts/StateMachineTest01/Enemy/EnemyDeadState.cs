using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseStateTest01
{
    public EnemyDeadState(EnemyStateMachineTest01 stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        
    }

    public override void Tick(float deltaTime) { }

    public override void Exit() { }
    
}
