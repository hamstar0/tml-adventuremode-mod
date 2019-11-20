using System;
using Terraria;
using Terraria.ModLoader;


namespace AdventureMode.NPCs {
	public enum TricksterStates : int {
		Idle = 0,
		Attack = 1,
		Cooldown = 2
	}




	partial class TricksterNPC : ModNPC {
		public int GetCurrentStateTickDuration() {
			switch( this.State ) {
			default:
			case TricksterStates.Idle:
				return TricksterNPC.IdleDurationTicks;
			case TricksterStates.Attack:
				return TricksterNPC.AttackDurationTicks;
			case TricksterStates.Cooldown:
				return TricksterNPC.CooldownDurationTicks;
			}
		}

		////////////////

		public void SetState( TricksterStates newState ) {
			TricksterStates oldState = this.State;
			this.State = newState;

			switch( newState ) {
			case TricksterStates.Idle:
				break;
			case TricksterStates.Attack:
				this.Evade();
				break;
			case TricksterStates.Cooldown:
				if( oldState == TricksterStates.Attack ) {
					this.LaunchAttack();
				}
				break;
			}
		}


		////////////////

		private void RunAI() {
			if( this.ElapsedStateTicks++ < this.GetCurrentStateTickDuration() ) {
				return;
			}

			this.ElapsedStateTicks = 0;

			switch( this.State ) {
			case TricksterStates.Idle:
				this.SetState( TricksterStates.Attack );
				break;
			case TricksterStates.Attack:
				this.SetState( TricksterStates.Cooldown );
				break;
			case TricksterStates.Cooldown:
				this.SetState( TricksterStates.Attack );
				break;
			}
		}
	}
}
