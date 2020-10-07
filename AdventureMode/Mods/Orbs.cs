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
			var config = OrbsConfig.Instance;
			var myTileKillWhitelist = new List<string>( config.TileKillWhitelist );
			myTileKillWhitelist.Add( TileID.GetUniqueKey( ModContent.TileType<FramingPlankTile>() ) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey( ModContent.TileType<ManaCrystalShardTile>() ) );

			config.SetOverride( nameof(OrbsConfig.TileKillWhitelist), myTileKillWhitelist );
			config.SetOverride( nameof(OrbsConfig.OnlyGenOrbsInUndergroundChests), false );
			config.SetOverride( nameof(OrbsConfig.AnyOrbPercentChancePerChest), 0.5f );
			config.SetOverride( nameof(OrbsConfig.WhiteOrbPercentChanceForOrbChest), 0f );
			config.SetOverride( nameof(OrbsConfig.IsGeoResonantOrbSoldByDryad), false );
			config.SetOverride( nameof(OrbsConfig.PinkOrbRecipeStack), 0 );
		}
	}
}
