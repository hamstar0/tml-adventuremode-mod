using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadCursedBones() {
			var config = CursedBones.CursedBonesConfig.Instance;

			var cbTile = ModContent.GetInstance<CursedBones.Tiles.CursedBonesTile>();

			cbTile.AddCustomItemDropHook( ( x, y ) => {
				int itemType;
				float myrand = Main.rand.NextFloat();

				if( myrand < (6f/10f) ) {   // 6/10
					itemType = -1;
				} else if( myrand < (9f/10f) ) {    // 3/10
					itemType = ModContent.ItemType<Necrotis.Items.DillutedEctoplasmItem>();
				} else {	// 1/10
					itemType = ModContent.ItemType<Orbs.Items.WhiteOrbItem>();
				}
				
				if( itemType != -1 ) {
					Item.NewItem(
						position: new Vector2( x * 16, y * 16 ),
						Type: itemType
					);
				}

				return true;
			} );
		}
	}
}
