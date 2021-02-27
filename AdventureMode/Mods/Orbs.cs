using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using AdventureModeLore.Tiles;
using CursedBrambles.Tiles;
using Ergophobia.Tiles;
using FindableManaCrystals.Tiles;
using Orbs;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadOrbs() {
			var config = OrbsConfig.Instance;
			var myTileKillWhitelist = new List<string>( config.TileKillWhitelist );
			myTileKillWhitelist.Add( TileID.GetUniqueKey( TileID.Candles ) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey( TileID.WaterCandle ) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey( TileID.PeaceCandle ) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey( TileID.Containers ) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey( ModContent.TileType<FramingPlankTile>() ) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey( ModContent.TileType<ManaCrystalShardTile>() ) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey( ModContent.TileType<CursedBrambleTile>() ) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey( ModContent.TileType<FallenCyborgTile>() ) );

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
