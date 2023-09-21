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

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            ResetSprintState();
        }
        #endregion

        #region Reusable Method
        protected override void OnContactWithGround(Collider collider)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }

        protected virtual void ResetSprintState()
        {
            stateMachine.ReusableData.ShouldSprint = false;
        }
        #endregion
    }
}

