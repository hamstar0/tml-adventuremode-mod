using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader.Config;
using Terraria.ModLoader;
using PrefabKits.Tiles;
using MountedMagicMirrors.Tiles;


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



	
	public partial class AMConfig : ModConfig {
		public List<string> TilePlaceWhitelist { get; set; } = new List<string> {
			//
			TileID.GetUniqueKey( TileID.Rope ),
			TileID.GetUniqueKey( TileID.SilkRope ),
			TileID.GetUniqueKey( TileID.VineRope ),
			TileID.GetUniqueKey( TileID.WebRope ),
			TileID.GetUniqueKey( TileID.Chain ),
			//TileID.GetUniqueKey( TileID.MinecartTrack ),
			TileID.GetUniqueKey( ModContent.TileType<TrackDeploymentTile>() ),
			TileID.GetUniqueKey( ModContent.TileType<MountedMagicMirrorTile>() ),
			//
			TileID.GetUniqueKey( TileID.OpenDoor ),
			TileID.GetUniqueKey( TileID.ClosedDoor ),
			TileID.GetUniqueKey( TileID.TallGateClosed ),
			TileID.GetUniqueKey( TileID.TallGateOpen ),
			TileID.GetUniqueKey( TileID.CrystalBall ),
			TileID.GetUniqueKey( TileID.AmmoBox ),
			TileID.GetUniqueKey( TileID.SharpeningStation ),
			TileID.GetUniqueKey( TileID.Extractinator ),
			TileID.GetUniqueKey( TileID.Tombstones ),
			TileID.GetUniqueKey( TileID.Banners ),
			TileID.GetUniqueKey( TileID.ImbuingStation ),
			TileID.GetUniqueKey( TileID.BewitchingTable ),
			TileID.GetUniqueKey( TileID.AlchemyTable ),
			TileID.GetUniqueKey( TileID.Bottles ),
			TileID.GetUniqueKey( TileID.Anvils ),
			TileID.GetUniqueKey( TileID.MythrilAnvil ),
			TileID.GetUniqueKey( TileID.Furnaces ),
			TileID.GetUniqueKey( TileID.Hellforge ),
			TileID.GetUniqueKey( TileID.AdamantiteForge ),
			TileID.GetUniqueKey( TileID.Autohammer ),
			TileID.GetUniqueKey( TileID.TinkerersWorkbench ),
			TileID.GetUniqueKey( TileID.Sawmill ),
			TileID.GetUniqueKey( TileID.PiggyBank ),
			TileID.GetUniqueKey( TileID.Safes ),
			TileID.GetUniqueKey( TileID.DefendersForge ),
			TileID.GetUniqueKey( TileID.LunarCraftingStation ),
			TileID.GetUniqueKey( TileID.Cannon ),
			TileID.GetUniqueKey( TileID.SnowballLauncher ),
			TileID.GetUniqueKey( TileID.ElderCrystalStand ),
			//
			TileID.GetUniqueKey( TileID.Timers ),
			TileID.GetUniqueKey( TileID.InletPump ),
			TileID.GetUniqueKey( TileID.OutletPump ),
			TileID.GetUniqueKey( TileID.Lever ),
			TileID.GetUniqueKey( TileID.Statues ),
			TileID.GetUniqueKey( TileID.Explosives ),
			//
			TileID.GetUniqueKey( TileID.Torches ),
			TileID.GetUniqueKey( TileID.Campfire ),
			TileID.GetUniqueKey( TileID.LandMine ),
			TileID.GetUniqueKey( TileID.HangingLanterns ),
			//
			TileID.GetUniqueKey( TileID.Saplings ),
			TileID.GetUniqueKey( TileID.Pumpkins ),
			TileID.GetUniqueKey( TileID.ImmatureHerbs ),
			TileID.GetUniqueKey( TileID.MatureHerbs ),
			TileID.GetUniqueKey( TileID.BloomingHerbs ),
			TileID.GetUniqueKey( TileID.Sunflower ),
			//
			TileID.GetUniqueKey( TileID.Painting2X3 ),
			TileID.GetUniqueKey( TileID.Painting3X2 ),
			TileID.GetUniqueKey( TileID.Painting3X3 ),
			TileID.GetUniqueKey( TileID.Painting4X3 ),
			TileID.GetUniqueKey( TileID.Painting6X4 ),
			TileID.GetUniqueKey( TileID.ChristmasTree ),
			TileID.GetUniqueKey( TileID.HolidayLights ),
			TileID.GetUniqueKey( TileID.WarTableBanner ),
			TileID.GetUniqueKey( TileID.SillyStreamerBlue ),
			TileID.GetUniqueKey( TileID.SillyStreamerGreen ),
			TileID.GetUniqueKey( TileID.SillyStreamerPink ),
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
			/*new HouseKitFurnitureDefinition { TileType = TileID.Anvils, IsHardMode = false },
			new HouseKitFurnitureDefinition { TileType = TileID.Furnaces, IsHardMode = false },
			new HouseKitFurnitureDefinition { TileType = TileID.CookingPots, IsHardMode = false },
			//new HouseKitFurnitureDefinition { TileType = TileID.Bottles, IsHardMode = false },
			new HouseKitFurnitureDefinition { TileType = TileID.Sawmill, IsHardMode = false },
			new HouseKitFurnitureDefinition { TileType = TileID.HeavyWorkBench, IsHardMode = false },
			new HouseKitFurnitureDefinition { TileType = TileID.TinkerersWorkbench, IsHardMode = false },
			new HouseKitFurnitureDefinition { TileType = TileID.Containers, IsHardMode = false },
			//new HouseKitFurnitureDefinition { TileType = TileID.Statues, IsHardMode = false },
			new HouseKitFurnitureDefinition { TileType = TileID.MythrilAnvil, IsHardMode = true },
			new HouseKitFurnitureDefinition { TileType = TileID.AdamantiteForge, IsHardMode = true },
			new HouseKitFurnitureDefinition { TileType = TileID.Bookcases, IsHardMode = false },
			//new HouseKitFurnitureDefinition { TileType = TileID.Safes, IsHardMode = true },*/
			new HouseKitFurnitureDefinition { TileType = TileID.Containers, IsHardMode = false },
			new HouseKitFurnitureDefinition { TileType = TileID.Containers, IsHardMode = true },
		};


		////

		[Range( -1, 1024 )]
		[DefaultValue( 3 )]
		public int MaxFramingPlankVerticalLength { get; set; } = 3;

		[Range( -1, 1024 )]
		[DefaultValue( 5 )]
		public int MaxFramingPlankHorizontalLength { get; set; } = 5;

		[Range( -1, 1024 )]
		[DefaultValue( 8 )]
		public int MaxPlatformBridgeLength { get; set; } = 8;

		////

		[Range( -1, 64 )]
		[DefaultValue( 8 )]
		public int MaxTrackGapPatchWidth { get; set; } = 16;



		////////////////

		private void OnLoadedTiles() {
			if( !this.OverrideRecipeTileRequirements ) {
				this.HouseKitFurnitureSuccession.AddRange( new HouseKitFurnitureDefinition[] {
					new HouseKitFurnitureDefinition { TileType = TileID.Tables, IsHardMode = false },
					new HouseKitFurnitureDefinition { TileType = TileID.Anvils, IsHardMode = false },
					new HouseKitFurnitureDefinition { TileType = TileID.Furnaces, IsHardMode = false },
					new HouseKitFurnitureDefinition { TileType = TileID.CookingPots, IsHardMode = false },
					//new HouseKitFurnitureDefinition { TileType = TileID.Bottles, IsHardMode = false },
					new HouseKitFurnitureDefinition { TileType = TileID.Sawmill, IsHardMode = false },
					new HouseKitFurnitureDefinition { TileType = TileID.HeavyWorkBench, IsHardMode = false },
					new HouseKitFurnitureDefinition { TileType = TileID.TinkerersWorkbench, IsHardMode = false },
					new HouseKitFurnitureDefinition { TileType = TileID.Containers, IsHardMode = false },
					//new HouseKitFurnitureDefinition { TileType = TileID.Statues, IsHardMode = false },
					new HouseKitFurnitureDefinition { TileType = TileID.MythrilAnvil, IsHardMode = true },
					new HouseKitFurnitureDefinition { TileType = TileID.AdamantiteForge, IsHardMode = true },
					new HouseKitFurnitureDefinition { TileType = TileID.Bookcases, IsHardMode = false },
					//new HouseKitFurnitureDefinition { TileType = TileID.Safes, IsHardMode = true },
				} );
			}
		}
	}
}
