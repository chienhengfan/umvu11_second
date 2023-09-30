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
            if(stateMachine.MobEnumIndex == EnemyStateMachine.MobGroup.AbyssMage.GetHashCode())
            {
                stateMachine.SwitchState(new EnemyMageAttackingState(stateMachine));
                return;
            }


            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));


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
        Debug.Log("PlayerName: " +stateMachine.Player.name);
        Vector3 vPlayerPos = stateMachine.Player.transform.position;
        Vector3 newFor = vPlayerPos - stateMachine.transform.position;
        newFor.Normalize();
        stateMachine.Controller.Move(newFor * stateMachine.MovementSpeed * deltaTime);
    }
}
