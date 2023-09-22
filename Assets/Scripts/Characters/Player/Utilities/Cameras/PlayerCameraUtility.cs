using UnityEngine;
using System;
using Cinemachine;

namespace Movementsystem
{
    [Serializable]
    public class PlayerCameraUtility
    {
        [field: SerializeField] public CinemachineVirtualCamera VirtualCamera { get; private set; }
        [field: SerializeField] public float DefaultHorizontalWaitTime { get; private set; } = 0f;
        [field: SerializeField] public float DefaultHorizontalRecentingTime { get; private set; } = 4f;

        private CinemachinePOV cinemachinePOV;

        public void Initialize()
        {
            cinemachinePOV = VirtualCamera.GetCinemachineComponent<CinemachinePOV>();
        }

        public void EnableRecenting(float waitTime = -1f, float recentingTime = -1f, float baseMovementSpeed = 1f, float movementSpeed = 1f)
        {
            cinemachinePOV.m_HorizontalRecentering.m_enabled = true;
            cinemachinePOV.m_HorizontalRecentering.CancelRecentering();

            if(waitTime == -1f)
            {
                waitTime = DefaultHorizontalWaitTime;
            }

            if (recentingTime == -1f)
            {
                recentingTime = DefaultHorizontalRecentingTime;
            }

            recentingTime = recentingTime * baseMovementSpeed / movementSpeed;

            cinemachinePOV.m_HorizontalRecentering.m_WaitTime = waitTime;
            cinemachinePOV.m_HorizontalRecentering.m_RecenteringTime = recentingTime;

        }

        public void DisableRecenting()
        {
            cinemachinePOV.m_HorizontalRecentering.m_enabled = false;
        }
    }
}

