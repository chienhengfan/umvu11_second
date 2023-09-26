using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;

    private Attack attack;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        //Debug.Log(attackIndex);
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetAttack(attack.Damage);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        AutoAim();
        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator);

        if(normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {


            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            // Go back to locomotion
            if(stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
        previousFrameTime = normalizedTime;
    }

    public override void Exit()
    {
        
    }

    private void TryComboAttack(float normalizedTime)
    {
        if(attack.ComboAttackTime == -1) { return; }

        if(normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState
        (
            new PlayerAttackingState
            (
                stateMachine,
                attack.ComboStateIndex
            )
        );
    }

    private void AutoAim(float aimRadius = 30f)
    {
        float closeDistance = aimRadius * 10;
        IDictionary<Collider, float> temp = new Dictionary<Collider, float>();
        Collider[] colliders = Physics.OverlapSphere(stateMachine.Player.transform.position, aimRadius);
        foreach (var hitcollider in colliders)
        {
            if (hitcollider.tag == "Enemy")
            {
                float distance = Vector3.Distance(stateMachine.Player.transform.position, hitcollider.transform.position);
                temp.Add(hitcollider, distance);
                closeDistance = Mathf.Min(distance, closeDistance);
            }

        }
        foreach (var target in temp)
        {
            if (target.Value == closeDistance)
            {
                Vector3 aimPoint = new Vector3(target.Key.transform.position.x, stateMachine.Player.transform.position.y, target.Key.transform.position.z);
                stateMachine.Player.transform.LookAt(aimPoint);
            }
        }

    }


}
