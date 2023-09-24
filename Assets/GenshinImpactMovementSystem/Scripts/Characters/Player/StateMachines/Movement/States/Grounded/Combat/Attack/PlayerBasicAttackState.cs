using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GenshinImpactMovementSystem
{
    public class PlayerBasicAttackState : PlayerGroundedState
    {
        private float previousFrameTime;


        private PlayerAttackData attackData;
        public PlayerBasicAttackState(PlayerMovementStateMachine playerMovementStateMachine, int attackIndex) : base(playerMovementStateMachine)
        {
            attackData = playerMovementStateMachine.Player.Data.GroundedData.AttackDataList[attackIndex];
        }

        #region IState Method
        public override void Enter()
        {
            base.Enter();
            stateMachine.Player.Weapon.SetAttack(attackData.Damage, attackData.Knockback);
            stateMachine.Player.Animator.CrossFadeInFixedTime(attackData.AnimationName, attackData.TransitionDuration);
            stateMachine.ReusableData.MovementSpeedModifier = 0f;
        }

        public override void Update()
        {
            float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator);

            if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
            {

                if (stateMachine.Player.Input.IsAttacking)
                {
                    TryComboAttack(normalizedTime);
                }
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
            }
            previousFrameTime = normalizedTime;
        }

        public override void Exit()
        {
            base.Exit();
        }


        #endregion

        private void TryComboAttack(float normalizedTime)
        {
            if (attackData.ComboAttackTime == -1) { return; }

            if (normalizedTime < attackData.ComboAttackTime) { return; }

            stateMachine.ChangeState(new PlayerBasicAttackState(stateMachine, attackData.ComboStateIndex));

        }
    }
}

