using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Logic;


namespace AdventureMode {
	partial class AMPlayer : ModPlayer {
		internal bool IsAlertedToBossesWhileDead = false;
		internal bool IsChaosStateChecked = false;

		////

		public float NecrotisAmount { get; internal set; } = 0f;

		public bool IsAdventurer { get; internal set; } = false;

		////

		public override bool CloneNewInstances => false;



		////////////////

		public override void Load( TagCompound tag ) {
			if( tag.ContainsKey("is_adventurer") ) {
				this.IsAdventurer = tag.GetBool( "is_adventurer" );
			}
		}

		public override TagCompound Save() {
			return new TagCompound { { "is_adventurer", this.IsAdventurer } };
		}


		////////////////

		public override void SetupStartInventory( IList<Item> items, bool mediumcoreDeath ) {
			PlayerLogic.SetupStartInventory( this, items, mediumcoreDeath );
		}
	}
}
