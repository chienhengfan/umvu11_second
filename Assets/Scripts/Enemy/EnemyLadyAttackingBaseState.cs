using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EnemyLadyAttackingBaseState : EnemyBaseState
{
    private int rand;
    public EnemyLadyAttackingBaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        rand = Random.Range(1, 6);
    }

    public override void Tick(float deltaTime)
    {
        switch (rand)
        {
            case 1:
                stateMachine.SwitchState(new EnemyLadyAttacking01State(stateMachine));
                break;
            case 2:
                stateMachine.SwitchState(new EnemyLadyAttacking02State(stateMachine));
                break;
            case 3:
                stateMachine.SwitchState(new EnemyLadyAttacking03State(stateMachine));
                break;
            case 4:
                stateMachine.SwitchState(new EnemyLadyAttacking04State(stateMachine));
                break;
            default:
                stateMachine.SwitchState(new EnemyLadySkill01State(stateMachine));
                break;
        }
    }

    public override void Exit()
    {
        
    }
}
