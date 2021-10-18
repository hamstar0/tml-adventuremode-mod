using System;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadErgophobiaAndQuickRope() {
			QuickRope.QuickRopeMod.AddRopePlacementHook( (Player player, Item ropeItem, int tileX, int tileY) => {
//Main.NewText("rope at "+tileX+", "+tileY);
				return Ergophobia.ErgophobiaAPI.CanPlaceRope( tileX, tileY );
			} );
		}
	}
}
