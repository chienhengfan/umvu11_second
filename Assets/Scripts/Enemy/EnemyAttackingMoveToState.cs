using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingMoveToState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("MoveTo");
    private const float TransitionDuration = 0.1f;
    private float timer = 3.0f;

    private Vector3 MoveToPos;
    public EnemyAttackingMoveToState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Vector3 cToPlayer = stateMachine.Player.transform.position - this.stateMachine.transform.position;
        float num = Random.Range(-1f, 1f);
        float randRange = Random.Range(1f, 1.2f);
        cToPlayer = Quaternion.Euler(0, 90 * num, 0) * cToPlayer;
        MoveToPos = stateMachine.Player.transform.position + cToPlayer * randRange;
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
        timer = 3.0f;
    }

    public override void Tick(float deltaTime)
    {
        Vector3 cToPlayer = stateMachine.Player.transform.position - this.stateMachine.transform.position;
        Vector3 vLeftToward = Quaternion.Euler(0, -90, 0) * cToPlayer;
        Vector3 vToPos = MoveToPos - stateMachine.transform.position;
        Debug.Log(vToPos.sqrMagnitude);
        if (vToPos.sqrMagnitude < 0.1f)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }
        
        vLeftToward.Normalize();
        stateMachine.Controller.Move( vLeftToward * Time.deltaTime);

        FacePlayer();
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            timer = 3f;
            return;
        }
        
        
    }

    public override void Exit()
    {

    }


}
