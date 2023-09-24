using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  GenshinImpactMovementSystem
{
    public class PlayerDeadState : PlayerGroundedState
    {
        public PlayerDeadState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }


    }
}

