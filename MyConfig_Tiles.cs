﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader.Config;
using HamstarHelpers.Services.Configs;


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
			//
			TileID.GetUniqueKey( TileID.Rope ),
			TileID.GetUniqueKey( TileID.SilkRope ),
			TileID.GetUniqueKey( TileID.VineRope ),
			TileID.GetUniqueKey( TileID.WebRope ),
			TileID.GetUniqueKey( TileID.Chain ),
			TileID.GetUniqueKey( TileID.MinecartTrack ),
			//
			TileID.GetUniqueKey( TileID.ClosedDoor ),
			TileID.GetUniqueKey( TileID.CrystalBall ),
			TileID.GetUniqueKey( TileID.AmmoBox ),
			TileID.GetUniqueKey( TileID.SharpeningStation ),
			TileID.GetUniqueKey( TileID.Extractinator ),
			TileID.GetUniqueKey( TileID.Tombstones ),
			TileID.GetUniqueKey( TileID.Banners ),
			TileID.GetUniqueKey( TileID.ImbuingStation ),
			TileID.GetUniqueKey( TileID.BewitchingTable ),
			TileID.GetUniqueKey( TileID.Bottles ),
			TileID.GetUniqueKey( TileID.Autohammer ),
			TileID.GetUniqueKey( TileID.Cannon ),
			TileID.GetUniqueKey( TileID.Anvils ),
			TileID.GetUniqueKey( TileID.MythrilAnvil ),
			TileID.GetUniqueKey( TileID.Furnaces ),
			TileID.GetUniqueKey( TileID.AdamantiteForge ),
			TileID.GetUniqueKey( TileID.TinkerersWorkbench ),
			TileID.GetUniqueKey( TileID.Sawmill ),
			TileID.GetUniqueKey( TileID.PiggyBank ),
			TileID.GetUniqueKey( TileID.Safes ),
			TileID.GetUniqueKey( TileID.DefendersForge ),
			TileID.GetUniqueKey( TileID.LunarCraftingStation ),
			//
			TileID.GetUniqueKey( TileID.Torches ),
			TileID.GetUniqueKey( TileID.Campfire ),
			//
			TileID.GetUniqueKey( TileID.Saplings ),
			TileID.GetUniqueKey( TileID.Pumpkins ),
			TileID.GetUniqueKey( TileID.ImmatureHerbs ),
			TileID.GetUniqueKey( TileID.MatureHerbs ),
			TileID.GetUniqueKey( TileID.BloomingHerbs ),
			TileID.GetUniqueKey( TileID.Sunflower ),
			//
			// TileID.GetUniqueKey( TileID.Sand ),
			// TileID.GetUniqueKey( TileID.Ebonsand ),
			// TileID.GetUniqueKey( TileID.Crimsand ),
			// TileID.GetUniqueKey( TileID.Pearlsand ),
			//
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


		////////////////

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
