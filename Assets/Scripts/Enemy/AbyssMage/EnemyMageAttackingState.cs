using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyMageAttackingState : EnemyBaseState
{
    private readonly int MageAttackHash = Animator.StringToHash("MageAttack");

    private const float TransitionDuration = 0.1f;
    private float verticalVelocity = 0f;
    private float stiffTime;

    public EnemyMageAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        foreach (var weapon in stateMachine.Weapons)
        {
            weapon.SetAttack(stateMachine.AttackDamage);
        }
        if(stiffTime <= 0)
        {
            stateMachine.Animator.CrossFadeInFixedTime(MageAttackHash, TransitionDuration);
            stiffTime = stateMachine.maxStiffTime;
            //Debug.LogError(stiffTime);
        }



    }

    public override void Tick(float deltaTime)
    {
        //stiffTime = stateMachine.maxStiffTime;
        //stiffTime -= Time.deltaTime;
        //Debug.LogError("now" + stiffTime);

        if (verticalVelocity < 0f && stateMachine.Controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        //Debug.Log(stateMachine.gameObject.name + verticalVelocity);
        Move(Vector3.up * verticalVelocity, deltaTime);

        float normalizedTime = stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if(normalizedTime >= 1f)
        {
            stateMachine.SwitchState(new EnemyMageAttackingIdleState(stateMachine));
            return;
        }

    }

    public override void Exit()
    {

    }
}
