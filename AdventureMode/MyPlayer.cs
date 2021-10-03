using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ModLibsCore.Libraries.Debug;
using AdventureMode.Logic;


namespace AdventureMode {
	partial class AMPlayer : ModPlayer {
		internal bool IsAlertedToBossesWhileDead = false;
		internal bool IsChaosStateChecked = false;

		////

		public float NecrotisAmount { get; internal set; } = 0f;

		public bool IsAdventurer { get; internal set; } = false;

		public Vector2 ResurfacePoint { get; internal set; } = default;

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
			if( !mediumcoreDeath ) {
				PlayerLogic.SetupInitialSpawnInventory( this, items );
			}
		}
	}
}
