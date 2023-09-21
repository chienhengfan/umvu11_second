using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movementsystem
{
    public class PlayerAirboneState : PlayerMovementState
    {
        public PlayerAirboneState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region Reusable Method
        protected override void OnContactWithGround(Collider collider)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
        #endregion
    }
}

