using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadGrappletech() {
			var grapConfig = Grappletech.GrappletechConfig.Instance;

			var wl = grapConfig.Get<HashSet<string>>( nameof(grapConfig.GrappleableTileWhitelist) );
			wl = new HashSet<string>( wl );

			wl.Add( TileID.GetUniqueKey(ModContent.TileType<Ergophobia.Tiles.FramingPlankTile>()) );

			grapConfig.SetOverride( nameof(grapConfig.GrappleableTileWhitelist), wl );
		}
	}
}
