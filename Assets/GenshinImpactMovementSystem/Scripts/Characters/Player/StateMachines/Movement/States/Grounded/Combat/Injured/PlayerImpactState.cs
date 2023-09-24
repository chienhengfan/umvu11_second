using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    public class PlayerImpactState : PlayerGroundedState
    {
        private const float CrossFadeDuration = 0.1f;

        private float duration = 1f;
        public PlayerImpactState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Method
        public override void Enter()
        {
            base.Enter();
            stateMachine.Player.Animator.CrossFadeInFixedTime(stateMachine.Player.AnimationData.ImpactHash, CrossFadeDuration);
            stateMachine.ReusableData.MovementSpeedModifier = 0f;
        }

        public override void Update()
        {
            duration -= Time.deltaTime;
            if(duration <= 0f)
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
            }
        }
        #endregion
    }
}

