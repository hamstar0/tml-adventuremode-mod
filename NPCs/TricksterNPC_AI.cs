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

		////

		private float ElapsedStateTicks {
			get { return this.npc.localAI[1]; }
			set { this.npc.localAI[1] += value; }
		}



		////////////////

		public TricksterStates GetState() => (TricksterStates)(int)this.npc.localAI[0];
		private void SetState( TricksterStates state ) => this.npc.localAI[0] = (float)state;

		public int GetCurrentStateDuration() {
			switch( this.GetState() ) {
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
			if( this.ElapsedStateTicks++ < this.GetCurrentStateDuration() ) {
				return;
			}

			this.ElapsedStateTicks = 0f;

			switch( this.GetState() ) {
			case TricksterStates.Idle:
				this.SetState( TricksterStates.Attack );
				break;
			case TricksterStates.Attack:
				this.LaunchAttack();
				this.SetState( TricksterStates.Cooldown );
				break;
			case TricksterStates.Cooldown:
				this.Evade();
				this.SetState( TricksterStates.Attack );
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
