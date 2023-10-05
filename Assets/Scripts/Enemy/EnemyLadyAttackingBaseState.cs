using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EnemyLadyAttackingBaseState : EnemyBaseState
{
    private int rand;
    private float tick = 0f;
    private float bossActonFreezeTime = 0f;
    public EnemyLadyAttackingBaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        if(stateMachine.Health.health <= stateMachine.Health.maxHealth / 2)
        {
            rand = Random.Range(1, 6);
        }
        else
        {
            rand = Random.Range(1, 5);
        }


        tick = 0f;

    }

    public override void Tick(float deltaTime)
    {
        tick += Time.deltaTime;
        if(tick >= bossActonFreezeTime)
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

    }

    public override void Exit()
    {
        
    }

}
