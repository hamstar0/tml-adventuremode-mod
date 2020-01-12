using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using AdventureMode.Tiles;
using FindableManaCrystals.Tiles;
using Orbs;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadOrbs() {
			var myTileKillWhitelist = new List<string>( OrbsConfig.Instance.TileKillWhitelist );
			myTileKillWhitelist.Add( TileID.GetUniqueKey( ModContent.TileType<FramingPlankTile>() ) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey( ModContent.TileType<ManaCrystalShardTile>() ) );

			OrbsConfig.Instance.OverlayChanges( new OrbsConfig {
				TileKillWhitelist = myTileKillWhitelist,
				OnlyGenOrbsInUndergroundChests = false,
				AnyOrbPercentChancePerChest = 0.5f,
				WhiteOrbPercentChanceForOrbChest = 0f,
				IsGeoResonantOrbSoldByDryad = false	// Mod Helpers adds this
			} );
		}
	}
}
