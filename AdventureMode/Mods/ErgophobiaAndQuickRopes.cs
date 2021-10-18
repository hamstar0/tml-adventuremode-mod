using System;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadErgophobiaAndQuickRopes() {
			QuickRope.QuickRopeMod.AddRopePlacementHook( (Player player, Item ropeItem, int tileX, int tileY) => {
				return Ergophobia.ErgophobiaAPI.CanPlaceRope( tileX, tileY );
			} );
		}
	}
}
