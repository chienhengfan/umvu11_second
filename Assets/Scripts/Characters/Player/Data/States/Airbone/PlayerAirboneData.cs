using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Movementsystem
{
    [Serializable]
    public class PlayerAirboneData
    {
        [field: SerializeField] public PlayerJumpData JumpData { get; private set; }
    }
}

