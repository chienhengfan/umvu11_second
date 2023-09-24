using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movementsystem
{
    public class PlayerMediumStoppingState : PlayerStoppingState
    {
        public PlayerMediumStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        #region IState Methods
        public override void Enter()
        {
            stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.MediumDecelerationForce;
            stateMachine.ReusableData.CurrentJumpForce = airboneData.JumpData.MediumForce;
        }
        #endregion
    }
}

