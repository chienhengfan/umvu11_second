using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace GenshinImpactMovementSystem
{
    [Serializable]
    public class PlayerCombatData : MonoBehaviour
    {
        [field: SerializeField] public float PlayerMaxHP { get; private set; }
        [field: SerializeField] public float PlayerBasicAttack { get; private set; }
    }
}

