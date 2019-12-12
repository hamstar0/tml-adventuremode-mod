using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Services.Configs;
using AdventureMode.Tiles;
using FindableManaCrystals.Tiles;


namespace AdventureMode {
	public class HouseKitFurnitureDefinition {
		public ushort TileType { get; set; }
		public bool IsHardMode { get; set; }



		////////////////

		public override bool Equals( object obj ) {
			var kitObj = obj as HouseKitFurnitureDefinition;
			if( kitObj == null ) {
				return false;
			}

			return this.TileType == kitObj.TileType && this.IsHardMode == kitObj.IsHardMode;
		}

		public override int GetHashCode() {
			return this.TileType.GetHashCode() ^ (this.IsHardMode ? -1 : 0);
		}
	}




	public partial class AdventureModeConfig : StackableModConfig {
		public List<string> TilePlaceWhitelist { get; set; } = new List<string> {
			TileID.GetUniqueKey( TileID.LunarCraftingStation ),
			///
			//TileID.GetUniqueKey( TileID.Rope ),	//<-Rope coils only
			//TileID.GetUniqueKey( TileID.SilkRope ),
			//TileID.GetUniqueKey( TileID.VineRope ),
			//TileID.GetUniqueKey( TileID.WebRope ),
			TileID.GetUniqueKey( TileID.Chain ),
			TileID.GetUniqueKey( TileID.MinecartTrack ),
			///
			TileID.GetUniqueKey( TileID.ClosedDoor ),
			TileID.GetUniqueKey( TileID.CrystalBall ),
			TileID.GetUniqueKey( TileID.AmmoBox ),
			TileID.GetUniqueKey( TileID.SharpeningStation ),
			TileID.GetUniqueKey( TileID.Extractinator ),
			TileID.GetUniqueKey( TileID.Tombstones ),
			TileID.GetUniqueKey( TileID.Banners ),
			TileID.GetUniqueKey( TileID.ImbuingStation ),
			TileID.GetUniqueKey( TileID.BewitchingTable ),
			TileID.GetUniqueKey( TileID.Autohammer ),
			TileID.GetUniqueKey( TileID.Cannon ),
			TileID.GetUniqueKey( TileID.Anvils ),
			TileID.GetUniqueKey( TileID.MythrilAnvil ),
			TileID.GetUniqueKey( TileID.Furnaces ),
			TileID.GetUniqueKey( TileID.AdamantiteForge ),
			TileID.GetUniqueKey( TileID.TinkerersWorkbench ),
			TileID.GetUniqueKey( TileID.Sawmill ),
			///
			TileID.GetUniqueKey( TileID.Torches ),
			TileID.GetUniqueKey( TileID.Campfire ),
			///
			TileID.GetUniqueKey( TileID.Saplings ),
			TileID.GetUniqueKey( TileID.Pumpkins ),
			TileID.GetUniqueKey( TileID.ImmatureHerbs ),
			TileID.GetUniqueKey( TileID.MatureHerbs ),
			TileID.GetUniqueKey( TileID.BloomingHerbs ),
			TileID.GetUniqueKey( TileID.Sunflower ),
			///
			// TileID.GetUniqueKey( TileID.Sand ),
			// TileID.GetUniqueKey( TileID.Ebonsand ),
			// TileID.GetUniqueKey( TileID.Crimsand ),
			// TileID.GetUniqueKey( TileID.Pearlsand ),
			///
			// TileID.GetUniqueKey(case TileID.Plants ),
			// TileID.GetUniqueKey(case TileID.Plants2 ),
			// TileID.GetUniqueKey(case TileID.JunglePlants ),
			// TileID.GetUniqueKey(case TileID.JunglePlants2 ),
			// TileID.GetUniqueKey(case TileID.MushroomPlants ),
			// TileID.GetUniqueKey(case TileID.HallowedPlants ),
			// TileID.GetUniqueKey(case TileID.HallowedPlants2 ),
			// TileID.GetUniqueKey(case TileID.CorruptPlants ),
			// TileID.GetUniqueKey(case TileID.FleshWeeds ),
		};

		public List<string> TileKillWhitelist { get; set; } = new List<string> {
			TileID.GetUniqueKey( TileID.LunarCraftingStation ),
			///
			TileID.GetUniqueKey( TileID.WoodBlock ),
			TileID.GetUniqueKey( TileID.BorealWood ),
			TileID.GetUniqueKey( TileID.RichMahogany ),
			TileID.GetUniqueKey( TileID.LivingWood ),
			TileID.GetUniqueKey( TileID.LeafBlock ),
			TileID.GetUniqueKey( TileID.WoodenBeam ),
			TileID.GetUniqueKey( TileID.ClosedDoor ),
			TileID.GetUniqueKey( TileID.OpenDoor ),
			TileID.GetUniqueKey( TileID.MushroomBlock ),
			TileID.GetUniqueKey( TileID.CactusBlock ),
			TileID.GetUniqueKey( ModContent.TileType<FramingPlankTile>() ),
			///
			TileID.GetUniqueKey( TileID.Trees ),
			TileID.GetUniqueKey( TileID.MushroomTrees ),
			TileID.GetUniqueKey( TileID.PalmTree ),
			TileID.GetUniqueKey( TileID.Plants ),
			TileID.GetUniqueKey( TileID.Plants2 ),
			TileID.GetUniqueKey( TileID.Vines ),
			TileID.GetUniqueKey( TileID.MushroomPlants ),
			TileID.GetUniqueKey( TileID.CorruptPlants ),
			TileID.GetUniqueKey( TileID.CorruptThorns ),
			TileID.GetUniqueKey( TileID.FleshWeeds ),
			TileID.GetUniqueKey( TileID.CrimtaneThorns ),
			TileID.GetUniqueKey( TileID.CrimsonVines ),
			TileID.GetUniqueKey( TileID.HallowedPlants ),
			TileID.GetUniqueKey( TileID.HallowedPlants2 ),
			TileID.GetUniqueKey( TileID.HallowedVines ),
			TileID.GetUniqueKey( TileID.JunglePlants ),
			TileID.GetUniqueKey( TileID.JunglePlants2 ),
			TileID.GetUniqueKey( TileID.JungleVines ),
			TileID.GetUniqueKey( TileID.JungleThorns ),
			TileID.GetUniqueKey( TileID.Coral ),
			TileID.GetUniqueKey( TileID.BeachPiles ),
			TileID.GetUniqueKey( TileID.Cactus ),
			TileID.GetUniqueKey( TileID.DyePlants ),
			TileID.GetUniqueKey( TileID.ImmatureHerbs ),
			TileID.GetUniqueKey( TileID.BloomingHerbs ),
			TileID.GetUniqueKey( TileID.MatureHerbs ),
			TileID.GetUniqueKey( TileID.Cobweb ),
			TileID.GetUniqueKey( TileID.MagicalIceBlock ),
			TileID.GetUniqueKey( TileID.BlueMoss ),
			TileID.GetUniqueKey( TileID.BrownMoss ),
			TileID.GetUniqueKey( TileID.GreenMoss ),
			TileID.GetUniqueKey( TileID.LavaMoss ),
			TileID.GetUniqueKey( TileID.LongMoss ),
			TileID.GetUniqueKey( TileID.PurpleMoss ),
			TileID.GetUniqueKey( TileID.RedMoss ),
			TileID.GetUniqueKey( TileID.HoneyBlock ),
			TileID.GetUniqueKey( TileID.CrispyHoneyBlock ),
			TileID.GetUniqueKey( TileID.Hive ),
			///
			TileID.GetUniqueKey( TileID.Torches ),
			TileID.GetUniqueKey( TileID.Platforms ),
			TileID.GetUniqueKey( TileID.Rope ),
			TileID.GetUniqueKey( TileID.SilkRope ),
			TileID.GetUniqueKey( TileID.VineRope ),
			TileID.GetUniqueKey( TileID.WebRope ),
			TileID.GetUniqueKey( TileID.Chain ),
			TileID.GetUniqueKey( TileID.MinecartTrack ),
			TileID.GetUniqueKey( TileID.Heart ),
			TileID.GetUniqueKey( TileID.Pots ),
			TileID.GetUniqueKey( TileID.ShadowOrbs ),
			TileID.GetUniqueKey( TileID.DemonAltar ),
			TileID.GetUniqueKey( TileID.LifeFruit ),
			TileID.GetUniqueKey( TileID.PlanteraBulb ),
			TileID.GetUniqueKey( TileID.Bottles ),
			TileID.GetUniqueKey( TileID.Books ),
			TileID.GetUniqueKey( TileID.WaterCandle ),
			TileID.GetUniqueKey( TileID.PeaceCandle ),
			///
			TileID.GetUniqueKey( TileID.Copper ),
			TileID.GetUniqueKey( TileID.Tin ),
			TileID.GetUniqueKey( TileID.Iron ),
			TileID.GetUniqueKey( TileID.Lead ),
			TileID.GetUniqueKey( TileID.Silver ),
			TileID.GetUniqueKey( TileID.Tungsten ),
			TileID.GetUniqueKey( TileID.Gold ),
			TileID.GetUniqueKey( TileID.Platinum ),
			TileID.GetUniqueKey( TileID.Meteorite ),
			TileID.GetUniqueKey( TileID.Demonite ),
			TileID.GetUniqueKey( TileID.Crimtane ),
			TileID.GetUniqueKey( TileID.Obsidian ),
			TileID.GetUniqueKey( TileID.Hellstone ),
			TileID.GetUniqueKey( TileID.Cobalt ),
			TileID.GetUniqueKey( TileID.Palladium ),
			TileID.GetUniqueKey( TileID.Mythril ),
			TileID.GetUniqueKey( TileID.Orichalcum ),
			TileID.GetUniqueKey( TileID.Adamantite ),
			TileID.GetUniqueKey( TileID.Titanium ),
			TileID.GetUniqueKey( TileID.Chlorophyte ),
			TileID.GetUniqueKey( TileID.LunarOre ),
			///
			TileID.GetUniqueKey( TileID.Amethyst ),
			TileID.GetUniqueKey( TileID.Sapphire ),
			TileID.GetUniqueKey( TileID.Topaz ),
			TileID.GetUniqueKey( TileID.Emerald ),
			TileID.GetUniqueKey( TileID.Ruby ),
			TileID.GetUniqueKey( TileID.Diamond ),
			TileID.GetUniqueKey( TileID.ExposedGems ),
			///
			TileID.GetUniqueKey( TileID.SnowBlock ),
			TileID.GetUniqueKey( TileID.Cloud ),
			TileID.GetUniqueKey( TileID.RainCloud ),
			TileID.GetUniqueKey( TileID.SnowCloud ),
			TileID.GetUniqueKey( TileID.HoneyDrip ),
			TileID.GetUniqueKey( TileID.LavaDrip ),
			TileID.GetUniqueKey( TileID.SandDrip ),
			TileID.GetUniqueKey( TileID.WaterDrip ),
			///
			TileID.GetUniqueKey( TileID.Sand ),
			TileID.GetUniqueKey( TileID.Pearlsand ),
			TileID.GetUniqueKey( TileID.Crimsand ),
			TileID.GetUniqueKey( TileID.Ebonsand ),
			TileID.GetUniqueKey( TileID.DesertFossil ),
			TileID.GetUniqueKey( TileID.FossilOre ),
			TileID.GetUniqueKey( TileID.Silt ),
			TileID.GetUniqueKey( TileID.Slush ),
			///
			TileID.GetUniqueKey( TileID.CopperCoinPile ),
			TileID.GetUniqueKey( TileID.SilverCoinPile ),
			TileID.GetUniqueKey( TileID.GoldCoinPile ),
			TileID.GetUniqueKey( TileID.PlatinumCoinPile ),
			TileID.GetUniqueKey( TileID.Stalactite ),
			TileID.GetUniqueKey( TileID.SmallPiles ),
			TileID.GetUniqueKey( TileID.LargePiles ),
			TileID.GetUniqueKey( TileID.LargePiles2 ),
			///
			TileID.GetUniqueKey( TileID.Boulder ),
			TileID.GetUniqueKey( TileID.BeeHive ),
			TileID.GetUniqueKey( TileID.Tombstones ),
			///
			TileID.GetUniqueKey( ModContent.TileType<ManaCrystalShardTile>() ),
		};

		[DefaultValue( true )]
		public bool HardmodeBreakableDirt { get; set; } = true;

		////

		[ReloadRequired]
		public List<HouseKitFurnitureDefinition> HouseKitFurnitureSuccession { get; set; } = new List<HouseKitFurnitureDefinition> {
			new HouseKitFurnitureDefinition { TileType = TileID.Anvils, IsHardMode = false },
			new HouseKitFurnitureDefinition { TileType = TileID.Furnaces, IsHardMode = false },
			new HouseKitFurnitureDefinition { TileType = TileID.CookingPots, IsHardMode = false },
			//new HouseKitFurnitureDefinition { TileType = TileID.Bottles, IsHardMode = false },
			new HouseKitFurnitureDefinition { TileType = TileID.Sawmill, IsHardMode = false },
			new HouseKitFurnitureDefinition { TileType = TileID.Anvils, IsHardMode = false },
			new HouseKitFurnitureDefinition { TileType = TileID.Furnaces, IsHardMode = false },
			new HouseKitFurnitureDefinition { TileType = TileID.TinkerersWorkbench, IsHardMode = false },
			new HouseKitFurnitureDefinition { TileType = TileID.PiggyBank, IsHardMode = false },
			//new HouseKitFurnitureDefinition { TileType = TileID.Statues, IsHardMode = false },
			new HouseKitFurnitureDefinition { TileType = TileID.MythrilAnvil, IsHardMode = true },
			new HouseKitFurnitureDefinition { TileType = TileID.AdamantiteForge, IsHardMode = true },
			new HouseKitFurnitureDefinition { TileType = TileID.Safes, IsHardMode = true },
		};


		////

		[Range( -1, 1024 )]
		[DefaultValue( 4 )]
		public int MaxFramingPlankLength { get; set; } = 4;

		[Range( -1, 1024 )]
		[DefaultValue( 8 )]
		public int MaxPlatformBridgeLength { get; set; } = 8;
	}
}
