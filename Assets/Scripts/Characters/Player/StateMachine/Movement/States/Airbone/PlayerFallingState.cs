using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movementsystem
{
    public class PlayerFallingState : PlayerAirboneState
    {
        private PlayerFallData fallData;

        private Vector3 playerPositionOnEnter;
        public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            fallData = airboneData.FallData;
        }

        #region IState Method
        public override void Enter()
        {
            base.Enter();

            playerPositionOnEnter = stateMachine.Player.transform.position;
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
        protected override void OnContactWithGround(Collider collider)
        {
            float fallDistance = playerPositionOnEnter.y - stateMachine.Player.transform.position.y;

            if(fallDistance < fallData.MinimumDistanceToBeConsideredHardFall)
            {
                stateMachine.ChangeState(stateMachine.LightLandingState);
            }

            if(stateMachine.ReusableData.ShouldWalk && !stateMachine.ReusableData.ShouldSprint || stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.HardLandingState);
                return;
            }

            stateMachine.ChangeState(stateMachine.RollingState);

        }
        protected override void ResetSprintState()
        {
            base.ResetSprintState();
        }
        #endregion

        #region Main Method
        private void LimitVerticalVelocity()
        {
            Vector3 PlayerVerticalVelocity = GetPlayerVerticalVelecity();
            if(PlayerVerticalVelocity.y >= -airboneData.FallData.FallSpeedLimit)
            {
                return;
            }

            Vector3 limitedVelocityForce = new Vector3(0f, -airboneData.FallData.FallSpeedLimit - PlayerVerticalVelocity.y, 0f);

            stateMachine.Player.Rigidbody.AddForce(limitedVelocityForce, ForceMode.VelocityChange);
        }
        #endregion
    }
}

