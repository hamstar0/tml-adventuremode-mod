using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.Debug;


namespace AdventureMode {
	partial class AdventureModePlayer : ModPlayer {
		private bool IsAlertedToBossesWhileDead = false;
		private bool IsChaosStateChecked = false;

		////

		public float NecrotisAmount { get; internal set; } = 0f;

		public bool IsAdventurer { get; private set; } = false;

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
			void addItem( int itemType, int stack ) {
				var item = new Item();
				item.SetDefaults( itemType );
				item.stack = stack;
				items.Add( item );
			}

			if( !mediumcoreDeath ) {
				this.IsAdventurer = true;

				addItem( ItemID.WoodenHammer, 1 );
				if( !AdventureModeConfig.Instance.EnableTorchRecipes ) {
					addItem( ItemID.Torch, 10 );
				}
				addItem( ItemID.RopeCoil, 20 );
			}
		}
	}
}
