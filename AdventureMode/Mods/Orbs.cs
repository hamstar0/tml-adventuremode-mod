using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadOrbs() {
			var orbsConfig = Orbs.OrbsConfig.Instance;

			var myTileKillWhitelist = orbsConfig.Get<List<string>>( nameof(orbsConfig.BreakableTilesWhitelist) );
			myTileKillWhitelist = new List<string>( myTileKillWhitelist );
			myTileKillWhitelist.Add( TileID.GetUniqueKey(TileID.Candles) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey(TileID.WaterCandle) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey(TileID.PeaceCandle) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey(TileID.Containers) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey(TileID.Pumpkins) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey(ModContent.TileType<Ergophobia.Tiles.FramingPlankTile>()) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey(ModContent.TileType<FindableManaCrystals.Tiles.ManaCrystalShardTile>()) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey(ModContent.TileType<CursedBrambles.Tiles.CursedBrambleTile>()) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey(ModContent.TileType<AdventureModeLore.Tiles.FallenCyborgTile>()) );
			myTileKillWhitelist.Add( TileID.GetUniqueKey(ModContent.TileType<CursedBones.Tiles.CursedBonesTile>()) );

			var wlExceptions = orbsConfig.Get<Dictionary<string, List<int>>>(
				nameof( orbsConfig.BreakableTilesWhitelistFrameExceptions )
			);
			wlExceptions = new Dictionary<string, List<int>>( wlExceptions );
			wlExceptions[ TileID.GetUniqueKey(TileID.Containers) ] = new List<int> { 180, 198 };	// unbreakable barrels

			orbsConfig.SetOverride( nameof(orbsConfig.BreakableTilesWhitelist), myTileKillWhitelist );
			orbsConfig.SetOverride( nameof(orbsConfig.BreakableTilesWhitelistFrameExceptions), wlExceptions );
			orbsConfig.SetOverride( nameof(orbsConfig.OnlyGenOrbsInUndergroundChests), false );
			orbsConfig.SetOverride( nameof(orbsConfig.AnyOrbPercentChancePerChest), 0.65f );
			orbsConfig.SetOverride( nameof(orbsConfig.CyanOrbWeightPerOrbChest), 0.25f );
			orbsConfig.SetOverride( nameof(orbsConfig.WhiteOrbWeightPerOrbChest), 0f );
			orbsConfig.SetOverride( nameof(orbsConfig.IsGeoResonantOrbSoldByDryad), false );

			orbsConfig.SetOverride( nameof(orbsConfig.BlueOrbRecipeStack), 0 );
			orbsConfig.SetOverride( nameof(orbsConfig.CyanOrbRecipeStack), 0 );
			orbsConfig.SetOverride( nameof(orbsConfig.GreenOrbRecipeStack), 0 );
			orbsConfig.SetOverride( nameof(orbsConfig.PinkOrbRecipeStack), 0 );
			orbsConfig.SetOverride( nameof(orbsConfig.PurpleOrbRecipeStack), 0 );
			orbsConfig.SetOverride( nameof(orbsConfig.RedOrbRecipeStack), 0 );
			orbsConfig.SetOverride( nameof(orbsConfig.BrownOrbRecipeStack), 0 );
			orbsConfig.SetOverride( nameof(orbsConfig.YellowOrbRecipeStack), 0 );
			orbsConfig.SetOverride( nameof(orbsConfig.WhiteOrbRecipeStack), 1 );
		}
	}
}
