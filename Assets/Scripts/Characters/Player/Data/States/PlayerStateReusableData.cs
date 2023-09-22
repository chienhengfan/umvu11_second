using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movementsystem
{
    public class PlayerStateReusableData
    {
        public Vector2 MovementInput { get; set; }
        public float MovementSpeedModifier { get; set; } = 1f;
        public float MovementOnSlopesSpeedModifier { get; set; } = 1f;
        public float MovementDecelerationForce { get; set; } = 1f;
        public List<PlayerCameraRecentingData> SidewaysCameraRecentingData { get; set; }
        public List<PlayerCameraRecentingData> BackwardsCameraRecentingData { get; set; }
        public PlayerRotationData RotationData { get; set; }

        public bool ShouldWalk { get; set; }
        public bool ShouldSprint { get; set; }

        private Vector3 currentTargetRotation;
        private Vector3 timeToReachTargetRotation;
        private Vector3 dampedTargetRotationCureentVelecity;
        private Vector3 dampedTargetRotationPassedTime;

        public ref Vector3 CurrentTargetRotation
        {
            get
            {
                return ref currentTargetRotation;
            }
        }

        public ref Vector3 TimeToReachTargetRotation
        {
            get
            {
                return ref timeToReachTargetRotation;
            }
            
        }
        public ref Vector3 DampedTargetRotationCureentVelecity
        {
            get
            {
                return ref dampedTargetRotationCureentVelecity;
            }
           
        }
        public ref Vector3 DampedTargetRotationPassedTime
        {
            get
            {
                return ref dampedTargetRotationPassedTime;
            }
            
        }

        public Vector3 CurrentJumpForce { get; set; }
    }
}