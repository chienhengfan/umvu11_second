using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingMoveToStateTest01 : EnemyBaseStateTest01
{
    private readonly int AttackHash = Animator.StringToHash("MoveTo");
    private const float TransitionDuration = 0.1f;

    private Vector3 MoveToPos;
    public EnemyAttackingMoveToStateTest01(EnemyStateMachineTest01 stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Vector3 cToPlayer = stateMachine.Player.transform.position - this.stateMachine.transform.position;
        float num = Random.Range(90f, 180f);
        cToPlayer = Quaternion.Euler(0, num, 0) * cToPlayer;
        MoveToPos = stateMachine.Player.transform.position + cToPlayer;
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 cToPlayer = stateMachine.Player.transform.position - this.stateMachine.transform.position;
        Vector3 vLeftToward = Quaternion.Euler(0, -90, 0) * cToPlayer;
        Vector3 vToPos  = MoveToPos - stateMachine.transform.position;
        Debug.Log(vToPos.sqrMagnitude);
        if (vToPos.sqrMagnitude < 0.1f)
        {
            stateMachine.SwitchState(new EnemyChasingStateTest01(stateMachine));
            return;
        }
        vLeftToward.Normalize();
        stateMachine.transform.position += vLeftToward * Time.deltaTime;

        FacePlayer();
    }

    public override void Exit()
    {
        
    }

    
}
