using System;
using System.Collections.Generic;
using UnityEngine;

namespace Movementsystem
{
    [Serializable]

    public class PlayerWalkData
    {
        [field: SerializeField][field: Range(0f, 1f)] public float SpeedModifier { get; private set; } = 0.225f;
        [field: SerializeField] public List<PlayerCameraRecentingData> BackwardsCameraRecentingData { get; private set; }
    }
}