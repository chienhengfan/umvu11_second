using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{

    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int LocomotionMageHash = Animator.StringToHash("Locomotion_Mage");
    

    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    private int randNum = 0;
    private float verticalVelocity = 0f;
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) { }


    public override void Enter()
    {

        int mobIndex = stateMachine.MobEnumIndex;
        if (mobIndex == EnemyStateMachine.MobGroup.ChuCHu.GetHashCode())
        {
            stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
        }
        else if (mobIndex == EnemyStateMachine.MobGroup.AbyssMage.GetHashCode())
        {
            stateMachine.Animator.CrossFadeInFixedTime(LocomotionMageHash, CrossFadeDuration);
        }
        else if (mobIndex == EnemyStateMachine.MobGroup.BossLady.GetHashCode())
        {
            stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
        }
        randNum = UnityEngine.Random.Range(0, 5);

    }
    public override void Tick(float deltaTime)
    {

        if (verticalVelocity < 0f && stateMachine.Controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        Move(Vector3.up * verticalVelocity, Time.deltaTime);

        if (!IsInChasingRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        else if (IsInAttackingRange())
        {

            if (stateMachine.name == "LaSignora_Harbinger_NewVersion")
            {
                Debug.Log("INChasing");
                switch (randNum) 
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

            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
            return;
        }

        

        MoveToPlayer(deltaTime);

        FacePlayer();

        stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {

    }

    private void MoveToPlayer(float deltaTime)
    {
        //往玩家移動的寫法參考老師的
        Debug.Log("PlayerName: " +stateMachine.Player.name);
        Vector3 vPlayerPos = stateMachine.Player.transform.position;
        //Vector3 vPlayerFor = stateMachine.Player.transform.forward;
        //Vector3 nextPlayerPos = vPlayerPos + vPlayerFor * deltaTime;
        //Vector3 newFor = (nextPlayerPos - stateMachine.transform.position).normalized;
        Vector3 newFor = vPlayerPos - stateMachine.transform.position;
        newFor.Normalize();
        stateMachine.transform.position += newFor * stateMachine.MovementSpeed * deltaTime;
    }
}
