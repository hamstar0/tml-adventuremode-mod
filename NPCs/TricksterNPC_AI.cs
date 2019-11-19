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
		public static readonly int IdleDurationTicks = 60 * 2;
		public static readonly int AttackDurationTicks = 60 * 5;
		public static readonly int CooldownDurationTicks = 60 * 1;



		////////////////

		public int ElapsedStateTicks { get; private set; } = 0;

		public TricksterStates State { get; private set; } = TricksterStates.Idle;



		////////////////

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

		private void RunAI() {
			if( this.ElapsedStateTicks++ < this.GetCurrentStateTickDuration() ) {
				return;
			}

			this.ElapsedStateTicks = 0;

			switch( this.State ) {
			case TricksterStates.Idle:
				this.State = TricksterStates.Attack;
				break;
			case TricksterStates.Attack:
				this.LaunchAttack();
				this.State = TricksterStates.Cooldown;
				break;
			case TricksterStates.Cooldown:
				this.Evade();
				this.State = TricksterStates.Attack;
				break;
			}
		}


		////////////////

		public void LaunchAttack() {

		}


		////////////////

		public void Evade() {

		}


		////////////////

		public void Flee() {

		}
	}
}
