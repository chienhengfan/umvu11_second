using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movementsystem
{
    public class PlayerFallingState : PlayerAirboneState
    {
        private PlayerFallData fallData;
        public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            fallData = airboneData.FallData;
        }

        #region IState Method
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementOnSlopesSpeedModifier = 0f;
            ResetVerticalVelocity();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            LimitVerticalVelocity();
        }


        #endregion

        #region Reusable Method
        protected override void ResetSprintState()
        {
            base.ResetSprintState();
        }
        #endregion

        #region Main Method
        private void LimitVerticalVelocity()
        {
            Vector3 PlayerVerticalVelocity = GetPlayerVerticalVelecity();
            if(PlayerVerticalVelocity.y >= -fallData.FallSpeedLimit)
            {
                return;
            }

            Vector3 limitVelocity = new Vector3(0f, -fallData.FallSpeedLimit - PlayerVerticalVelocity.y, 0f);

            stateMachine.Player.Rigidbody.AddForce(limitVelocity, ForceMode.VelocityChange);
        }
        #endregion
    }
}

