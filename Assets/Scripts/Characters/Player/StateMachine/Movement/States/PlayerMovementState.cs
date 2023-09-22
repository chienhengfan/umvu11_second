using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movementsystem
{
    public class PlayerMovementState : IState
    {
        protected PlayerMovementStateMachine stateMachine;

        protected PlayerGroundedData movementData;
        protected PlayerAirboneData airboneData;


        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            stateMachine = playerMovementStateMachine;
            movementData = stateMachine.Player.Data.GroundedData;
            airboneData = stateMachine.Player.Data.AirboneData;

            SetBaseCameraRecentingData();

            InitializeData();
        }



        private void InitializeData()
        {
            SetBaseRotationData();

        }


        #region IState Method
        public virtual void Enter()
        {
            Debug.LogError("State: " + GetType().Name);
            AddInputActionCallbacks();
        }



        public virtual void Exit()
        {
            RemoveInputActionCallbacks();
        }



        public virtual void Handleinput()
        {
            ReadMovementInput();
        }


        public virtual void Update()
        {

        }
        public virtual void PhysicsUpdate()
        {
            Move();
        }
        public virtual void OnAnimationEnterEvent()
        {
        }

        public virtual void OnAnimationExitEvent()
        {
        }

        public virtual void OnAnimationTransitionEvent()
        {
        }

        public virtual void OnTriggerEnter(Collider collider)
        {
            if (stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
            {
                OnContactWithGround(collider);
                return;
            }
        }

        public void OnTriggerExit(Collider collider)
        {
            if (stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
            {
                OnContactWithGroundExited(collider);
                return;
            }
        }


        #endregion

        #region Main Methods
        public void ReadMovementInput()
        {
            stateMachine.ReusableData.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        }
        private void Move()
        {
            if (stateMachine.ReusableData.MovementInput == Vector2.zero || stateMachine.ReusableData.MovementSpeedModifier==0f) 
            {
                return;
            }
            Vector3 movementDirection = GetMovementInputDirection();

            float targetRotationYAngle = Rotate(movementDirection);
            Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);
            float movementSpeed = GetMovementSpeed();

            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            stateMachine.Player.Rigidbody.AddForce(targetRotationDirection *movementSpeed -currentPlayerHorizontalVelocity , ForceMode.VelocityChange);
        }
        private float Rotate(Vector3 direction)
        {
            float directionAngle = UpdateTargetRotation(direction);
            RotateTowardsTargetRotation();
            return directionAngle;
        }
        private float AddCameraRotationAngle(float angle)
        {
            angle += stateMachine.Player.MainCameraTransform.eulerAngles.y;
            if (angle > 360f)
            {
                angle -= 360f;
            }

            return angle;
        }
        private void UpdateTargerRotationDate(float targetAngle)
        {
            stateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;
            stateMachine.ReusableData.DampedTargetRotationPassedTime.y = 0f;
        }
        private float GetDirectionAngle(Vector3 direction)
        {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            if (directionAngle < 0f)
            {
                directionAngle += 360f;
            }

            return directionAngle;
        }

        #endregion
        #region Reuseable Methods
        protected Vector3 GetMovementInputDirection()
        {
            return new Vector3(stateMachine.ReusableData.MovementInput.x, 0f, stateMachine.ReusableData.MovementInput.y);
        }
        protected float GetMovementSpeed(bool shouldConsiderSlope = true)
        {
            float movementSpeed = movementData.BaseSpeed * stateMachine.ReusableData.MovementSpeedModifier;

            if (shouldConsiderSlope)
            {
                movementSpeed *= stateMachine.ReusableData.MovementOnSlopesSpeedModifier;
            }

            return movementSpeed;
        }

        protected Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;
            playerHorizontalVelocity.y = 0f;
            return playerHorizontalVelocity;
        }

        protected Vector3 GetPlayerVerticalVelecity()
        {
            return new Vector3(0f, stateMachine.Player.Rigidbody.velocity.y, 0f);
        }
        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;
            float targetYAngle = stateMachine.ReusableData.CurrentTargetRotation.y;


            float angleDifference = Mathf.Abs(Mathf.DeltaAngle(currentYAngle, targetYAngle));


            float rotationSpeed = angleDifference / stateMachine.ReusableData.TimeToReachTargetRotation.y * 3.05f;

            Quaternion currentRotation = Quaternion.Euler(0f, currentYAngle, 0f);
            Quaternion targetRotation = Quaternion.Euler(0f, targetYAngle, 0f);

            Quaternion newRotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
            stateMachine.Player.Rigidbody.MoveRotation(newRotation);
        }

        protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotatoion = true)
        {
            float directionAngle = GetDirectionAngle(direction);
            if (shouldConsiderCameraRotatoion)
            {
                directionAngle = AddCameraRotationAngle(directionAngle);
            }

            

            if (directionAngle != stateMachine.ReusableData.CurrentTargetRotation.y)
            {
                UpdateTargerRotationDate(directionAngle);
            }

            return directionAngle;
        }
        protected Vector3 GetTargetRotationDirection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        protected void ResetVelocity()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            stateMachine.Player.Rigidbody.velocity = playerHorizontalVelocity;
        }

        protected void ResetVerticalVelocity()
        {
            stateMachine.Player.Rigidbody.velocity = Vector3.zero;
        }
        protected virtual void AddInputActionCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started += OnWalkToggleStarted;

            stateMachine.Player.Input.PlayerActions.Look.started += OnMouseMovementStarted;

            stateMachine.Player.Input.PlayerActions.Movement.performed += OnMovementPerformed;

            stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
        }


        protected virtual void RemoveInputActionCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started -= OnWalkToggleStarted;

            stateMachine.Player.Input.PlayerActions.Look.started -= OnMouseMovementStarted;

            stateMachine.Player.Input.PlayerActions.Movement.performed -= OnMovementPerformed;

            stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        }

        protected void DecelerateHorizontally()
        {
            Vector3 playerHorizontalVelecity = GetPlayerHorizontalVelocity();

            stateMachine.Player.Rigidbody.AddForce(-playerHorizontalVelecity * stateMachine.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
        }

        protected void DecelerateVertically()
        {
            Vector3 playerVertitalVelecity = GetPlayerVerticalVelecity();

            stateMachine.Player.Rigidbody.AddForce(-playerVertitalVelecity * stateMachine.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
        }

        protected bool IsMovingHorizontally(float minimumMagnitude = 0.1f)
        {
            Vector3 playerHorizontalVelecity = GetPlayerHorizontalVelocity();
            Vector2 playerHorizontalMovement = new Vector2(playerHorizontalVelecity.x, playerHorizontalVelecity.z);
            return playerHorizontalMovement.magnitude > minimumMagnitude;
        }

        protected void SetBaseRotationData()
        {
            stateMachine.ReusableData.RotationData = movementData.BaseRotationData;
            stateMachine.ReusableData.TimeToReachTargetRotation = stateMachine.ReusableData.RotationData.TargetRotationReachTime;
        }

        protected virtual void OnContactWithGroundExited(Collider collider)
        {

        }
        protected virtual void OnContactWithGround(Collider collider)
        {
        }

        protected bool IsMovingUp(float minimumVelecity = 0.1f)
        {
            return GetPlayerVerticalVelecity().y > minimumVelecity;
        }

        protected bool IsMovingDown(float minimumVelecity = 0.1f)
        {
            return GetPlayerVerticalVelecity().y < -minimumVelecity;
        }

        protected void UpdateCameraRecentingState(Vector2 movementInput)
        {
            if(movementInput == Vector2.zero)
            {
                return;
            }

            if(movementInput == Vector2.up)
            {
                DisableableCameraRecenting();
                return;
            }

            float cameraVerticalAngle = stateMachine.Player.MainCameraTransform.eulerAngles.x;

            if(cameraVerticalAngle >= 270f)
            {
                cameraVerticalAngle -= 360f;
            }

            cameraVerticalAngle = Mathf.Abs(cameraVerticalAngle);

            if(movementInput == Vector2.down)
            {
                SetCameraRecentingState(cameraVerticalAngle, stateMachine.ReusableData.BackwardsCameraRecentingData);
                return;
            }

            SetCameraRecentingState(cameraVerticalAngle, stateMachine.ReusableData.SidewaysCameraRecentingData);
        }

        protected void SetCameraRecentingState(float cameraVerticalAngle, List<PlayerCameraRecentingData> cameraRecentingData)
        {
            foreach (PlayerCameraRecentingData recentingData in cameraRecentingData)
            {
                if (!recentingData.IsWithinRange(cameraVerticalAngle))
                {
                    continue;
                }

                EnableCameraRecenting(recentingData.WaitTime, recentingData.RecentingTime);

                return;
            }

            DisableableCameraRecenting();
        }
        protected void EnableCameraRecenting(float waitTime = -1f, float recentingTime = -1f)
        {
            float movementSpeed = GetMovementSpeed();

            if(movementSpeed == 0f)
            {
                movementSpeed = movementData.BaseSpeed;
            }

            stateMachine.Player.CameraUtility.EnableRecenting(waitTime, recentingTime, movementData.BaseSpeed, movementSpeed);
        }

        protected void DisableableCameraRecenting()
        {
            stateMachine.Player.CameraUtility.DisableRecenting();
        }

        #endregion

        #region Input Methobs
        protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            stateMachine.ReusableData.ShouldWalk = !stateMachine.ReusableData.ShouldWalk;
        }

        protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
        {
            DisableableCameraRecenting();
        }

        private void OnMouseMovementStarted(InputAction.CallbackContext context)
        {
            UpdateCameraRecentingState(stateMachine.ReusableData.MovementInput);
        }

        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            UpdateCameraRecentingState(context.ReadValue<Vector2>());
        }

        protected void SetBaseCameraRecentingData()
        {
            stateMachine.ReusableData.BackwardsCameraRecentingData = movementData.BackwardsCameraRecentingData;
            stateMachine.ReusableData.SidewaysCameraRecentingData = movementData.SidewaysCameraRecentingData;
        }
        #endregion
    }
}