using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movementsystem
{
    public class PlayerJumpingState : PlayerAirboneState
    {
        private PlayerJumpData jumpData;
        private bool shouldKeepRotating;
        private bool canStartFalling;

        public PlayerJumpingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            jumpData = airboneData.JumpData;
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            stateMachine.ReusableData.MovementSpeedModifier = 0f;

            stateMachine.ReusableData.MovementDecelerationForce = jumpData.DecelerationForce;

            shouldKeepRotating = stateMachine.ReusableData.MovementInput != Vector2.zero;

            Jump();
        }
        public override void Update()
        {
            base.Update();

            if(!canStartFalling && IsMovingUp(0f))
            {
                canStartFalling = true;
            }

            if(!canStartFalling || GetPlayerVerticalVelecity().y > 0)
            {
                return;
            }

            stateMachine.ChangeState(stateMachine.FallingState);
        }

        public override void Exit()
        {
            base.Exit();

            SetBaseRotationData();

            canStartFalling = false;
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            if (shouldKeepRotating)
            {
                RotateTowardsTargetRotation();
            }

            if (IsMovingUp())
            {
                DecelerateVertically();
            }
        }

        #endregion

        #region Reusable Methods

        protected override void ResetSprintState()
        {
        }
        #endregion

        #region MainMethods
        private void Jump()
        {
            Vector3 jumpForce = stateMachine.ReusableData.CurrentJumpForce;

            Vector3 JumpDirection = stateMachine.Player.transform.forward;

            if (shouldKeepRotating)
            {
                UpdateTargetRotation(GetMovementInputDirection());

                JumpDirection = GetTargetRotationDirection(stateMachine.ReusableData.CurrentTargetRotation.y);
            }

            jumpForce.x *= JumpDirection.x;
            jumpForce.z *= JumpDirection.z;

            Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.ColliderUtility.CapsuleColliderData.Collider.bounds.center;

            Ray downwardRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

            if(Physics.Raycast(downwardRayFromCapsuleCenter, out RaycastHit hit, jumpData.JumpToGroundRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
            {
                float groundAngle = Vector3.Angle(hit.normal, -downwardRayFromCapsuleCenter.direction);

                if (IsMovingUp())
                {
                    float forceModifier = jumpData.JumpForceModifierOnSlopeUpwards.Evaluate(groundAngle);

                    jumpForce.x *= forceModifier;
                    jumpForce.z *= forceModifier;
                }

                if (IsMovingDown())
                {
                    float forceModifier = jumpData.JumpForceModifierOnSlopeDownwards.Evaluate(groundAngle);

                    jumpForce.y *= forceModifier;
                }
            }

            ResetVelocity();

            stateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
        }
        #endregion

        #region Input Method
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
        }
        #endregion
    }

}
