using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Movementsystem
{
    [Serializable]
    public class PlayerCapsuleColliderUtility : CapsuleColliderUtility
    {
        [field: SerializeField] public PlayerTriggerColliderData TriggerColliderData { get; private set; }
    }

}
