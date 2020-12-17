using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using AdventureMode.Tiles;
using FindableManaCrystals.Tiles;
using CursedBrambles.Tiles;
using Orbs;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadOrbs() {
			var config = OrbsConfig.Instance;
			var myTileKillWhitelist = new List<string>( config.TileKillWhitelist );
			myTileKillWhitelist.Add( TileID.GetUniqueKey( ModContent.TileType<FramingPlankTile>() ) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey( ModContent.TileType<ManaCrystalShardTile>() ) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey( ModContent.TileType<CursedBrambleTile>() ) );

			config.SetOverride( nameof(config.TileKillWhitelist), myTileKillWhitelist );
			config.SetOverride( nameof(config.OnlyGenOrbsInUndergroundChests), false );
			config.SetOverride( nameof(config.AnyOrbPercentChancePerChest), 0.5f );
			config.SetOverride( nameof(config.CyanOrbWeightPerOrbChest), 0.25f );
			config.SetOverride( nameof(config.WhiteOrbWeightPerOrbChest), 0f );
			config.SetOverride( nameof(config.IsGeoResonantOrbSoldByDryad), false );

			config.SetOverride( nameof(config.BlueOrbRecipeStack), 0 );
			config.SetOverride( nameof(config.CyanOrbRecipeStack), 0 );
			config.SetOverride( nameof(config.GreenOrbRecipeStack), 0 );
			config.SetOverride( nameof(config.PinkOrbRecipeStack), 0 );
			config.SetOverride( nameof(config.PurpleOrbRecipeStack), 0 );
			config.SetOverride( nameof(config.RedOrbRecipeStack), 0 );
			config.SetOverride( nameof(config.BrownOrbRecipeStack), 0 );
			config.SetOverride( nameof(config.YellowOrbRecipeStack), 0 );
			config.SetOverride( nameof(config.WhiteOrbRecipeStack), 1 );
		}
	}
}
