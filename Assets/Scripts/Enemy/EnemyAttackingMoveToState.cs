using RPGCharacterAnims.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingMoveToState : EnemyBaseState
{
    private readonly int AttackMoveToLeftHash = Animator.StringToHash("MoveToLeft");
    private readonly int AttackMoveToRightHash = Animator.StringToHash("MoveToRight");
    
    private const float TransitionDuration = 0.1f;
    private float timer = 3.0f;

    private Vector3 MoveToTargetPos;
    public EnemyAttackingMoveToState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Vector3 vPlayerToSelf = this.stateMachine.transform.position - stateMachine.Player.transform.position;
        float num = Random.Range(-1f, 1f);
        float randRange = Random.Range(1f, 1.2f);
        vPlayerToSelf = Quaternion.Euler(0, 90 * num, 0) * vPlayerToSelf;
        MoveToTargetPos = stateMachine.Player.transform.position + vPlayerToSelf * randRange;
        Vector3 vToPoint = MoveToTargetPos - stateMachine.transform.position;
        vToPoint.Normalize();
        Vector3 vRight = stateMachine.transform.right;
        float fDotRight = Vector3.Dot(vToPoint, vRight);
        //Target點在正前方的右邊或左邊，用Dot
        int mobIndex = stateMachine.MobEnumIndex;
        if (mobIndex == EnemyStateMachine.MobGroup.ChuCHu.GetHashCode())
        {
            if (fDotRight >= 0f)
            {
                //在右邊
                stateMachine.Animator.CrossFadeInFixedTime(AttackMoveToRightHash, TransitionDuration);
            }
            else
            {
                //在左邊，與 AttackMoveToHash 動畫方向一致
                stateMachine.Animator.CrossFadeInFixedTime(AttackMoveToLeftHash, TransitionDuration);
            }
        }
        else if (mobIndex == EnemyStateMachine.MobGroup.CHuCHuCrossbow.GetHashCode())
        {
            if (fDotRight >= 0f)
            {
                //在右邊
                stateMachine.Animator.CrossFadeInFixedTime(AttackMoveToRightHash, TransitionDuration);
            }
            else
            {
                //在左邊，與 AttackMoveToHash 動畫方向一致
                stateMachine.Animator.CrossFadeInFixedTime(AttackMoveToLeftHash, TransitionDuration);
            }
        }
        timer = 3.0f;
    }

    public override void Tick(float deltaTime)
    {
        
        Vector3 vToPos = MoveToTargetPos - stateMachine.transform.position;
        Debug.Log(vToPos.sqrMagnitude);
        if (vToPos.sqrMagnitude < 0.1f)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }

        vToPos.Normalize();
        stateMachine.Controller.Move(vToPos * stateMachine.MovementSpeed* deltaTime);

        FacePlayer();

        MovingTimer(deltaTime);
    }

    public override void Exit()
    {

    }

    private void MovingTimer(float deltaTime)
    {
        timer -= deltaTime;
        if (timer <= 0)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            timer = 3f;
            return;
        }
    }
}
