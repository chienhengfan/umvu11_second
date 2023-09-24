using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GenshinImpactMovementSystem
{
    public class PlayerCombatStateMachine : StateMachine
    {
        public Player Player { get; }
        public PlayerStateReusableData ReusableData { get; }

        public PlayerBasicAttackState BasicAttackState { get; }
        public PlayerUltimateSkillState UltimateSkillState { get; }
        public PlayerInjuredState InjuredState { get; }
        public PlayerDeadState DeadState { get; }

        public PlayerCombatStateMachine(Player player)
        {
            Player = player;
            ReusableData = new PlayerStateReusableData();

            BasicAttackState = new PlayerBasicAttackState(this);
            UltimateSkillState = new PlayerUltimateSkillState(this);
            InjuredState = new PlayerInjuredState(this);
            DeadState = new PlayerDeadState(this);


        }
    }
}

