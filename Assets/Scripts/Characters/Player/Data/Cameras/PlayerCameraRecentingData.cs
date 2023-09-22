using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Movementsystem
{
    [Serializable]
    public class PlayerCameraRecentingData
    {
        [field: SerializeField][field: Range(0f, 360f)] public float MinimumAngle { get; private set; }
        [field: SerializeField] [field: Range(0f, 360f)] public float MaximumAngle { get; private set; }

        [field: SerializeField] [field: Range(-1f, 20f)] public float WaitTime { get; private set; }
        [field: SerializeField] [field: Range(-1f, 20f)] public float RecentingTime { get; private set; }

        public bool IsWithinRange(float angle)
        {
            return angle >= MinimumAngle && angle <= MaximumAngle;
        }
    }
}

