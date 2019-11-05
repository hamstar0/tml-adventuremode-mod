using HamstarHelpers.Helpers.Debug;
using HouseFurnishingKit.Items;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode {
	class AdventureModePlayer : ModPlayer {
		public override bool CloneNewInstances => false;



		////////////////

		public override void OnEnterWorld( Player player ) {
			if( player.whoAmI != Main.myPlayer ) {
				return;
			}
		}


		////////////////

		public override void PreUpdate() {
			if( Main.netMode != 2 ) {
				this.PreUpdateLocal();
			}
		}
		
		////

		private void PreUpdateLocal() {
			if( Main.myPlayer != this.player.whoAmI ) {
				return;
			}
		}


		////////////////

		public override void SetupStartInventory( IList<Item> items, bool mediumcoreDeath ) {
			if( !mediumcoreDeath ) {
				var binocs = new Item();
				binocs.SetDefaults( ItemID.Binoculars );
				binocs.stack = 1;
				var torches = new Item();
				torches.SetDefaults( ItemID.Torch );
				torches.stack = 10;
				var houseKits = new Item();
				houseKits.SetDefaults( ModContent.ItemType<HouseFurnishingKitItem>() );
				houseKits.stack = 3;

				items.Add( binocs );
				items.Add( torches );
				items.Add( houseKits );
			}
		}
	}
}
