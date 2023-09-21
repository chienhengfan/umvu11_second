using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movementsystem
{
    public class PlayerSprintingState : PlayerMovingState
    {
        private PlayerSprintData sprintData;

        private float startTime;

        private bool keepSprinting;
        private bool shouldResetSprintState;
        public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            sprintData = movementData.SprintData;
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            stateMachine.ReusableData.MovementSpeedModifier = sprintData.SpeedModifier;
            stateMachine.ReusableData.CurrentJumpForce = airboneData.JumpData.StrongForce;
            startTime = Time.time;

            shouldResetSprintState = true;
        }

        public override void Exit()
        {
            base.Exit();
            if (shouldResetSprintState)
            {
                keepSprinting = false;
                shouldResetSprintState = false;
            }

            
        }

        public override void Update()
        {
            base.Update();
            if (keepSprinting)
            {
                return;
            }

            if(Time.time < startTime + sprintData.SprintToRunTime)
            {
                return;
            }

            StopSprinting();
        }


        #region Main Methods
        private void StopSprinting()
        {
            if(stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
                return;
            }
            stateMachine.ChangeState(stateMachine.RunningState);
        }

        #endregion


        #endregion

        #region Reusable Methods
        protected override void AddInputActionCallbacks()
        {
            base.AddInputActionCallbacks();
            stateMachine.Player.Input.PlayerActions.Sprint.performed += OnSprintPerformed;
        }


        protected override void RemoveInputActionCallbacks()
        {
            base.RemoveInputActionCallbacks();
            stateMachine.Player.Input.PlayerActions.Sprint.performed -= OnSprintPerformed;
        }

        protected override void OnFall()
        {

            shouldResetSprintState = false;
            base.OnFall();
        }
        #endregion

        #region Input Methods
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.HardStoppingState);
        }

        protected override void OnJumpStarted(InputAction.CallbackContext context)
        {
            shouldResetSprintState = false;
            base.OnJumpStarted(context);
        }
        private void OnSprintPerformed(InputAction.CallbackContext context)
        {
            keepSprinting = true;
            stateMachine.ReusableData.ShouldSprint = true;

        }
        #endregion
    }
}
