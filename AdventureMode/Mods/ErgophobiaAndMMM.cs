using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadErgophobiaAndMountedMagicMirrors() {
			var ergConfig = Ergophobia.ErgophobiaConfig.Instance;

			ushort mirrorTileType = (ushort)ModContent.TileType<MountedMagicMirrors.Tiles.MountedMagicMirrorTile>();
			string mirrorUid = TileID.GetUniqueKey( mirrorTileType );
			List<string> wl = ergConfig.Get<List<string>>( nameof(ergConfig.TilePlaceWhitelist) )
				?? ergConfig.TilePlaceWhitelist.ToList();
			
			if( !wl.Contains(mirrorUid) ) {
				wl.Add( mirrorUid );
			}

			ergConfig.SetOverride( nameof(ergConfig.TilePlaceWhitelist), wl );

			ergConfig.SetOverride( nameof(ergConfig.FurnishedCustomWallMount1Tile), (int)mirrorTileType );
		}
	}
}
