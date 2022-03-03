using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadCursedBones() {
			var config = CursedBones.CursedBonesConfig.Instance;
			var cbTile = ModContent.GetInstance<CursedBones.Tiles.CursedBonesTile>();

			float ectoPerc = 3f / 20f;
			float orbPerc = 1f / 15f;

			//

			cbTile.AddCustomItemDropHook( ( x, y ) => {
				int itemType = -1;
				float myrand = Main.rand.NextFloat();

				if( myrand < ectoPerc ) {
					itemType = ModContent.ItemType<Necrotis.Items.DillutedEctoplasmItem>();
				} else if( myrand < (ectoPerc + orbPerc) ) {
					itemType = ModContent.ItemType<Orbs.Items.WhiteOrbItem>();
				}

				//
				
				if( itemType != -1 ) {
					Item.NewItem(
						position: new Vector2( x * 16, y * 16 ),
						Type: itemType
					);
				}
			} );
		}
	}
}
