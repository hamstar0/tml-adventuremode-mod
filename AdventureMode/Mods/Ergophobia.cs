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
		public void LoadErgophobia() {
			var ergConfig = Ergophobia.ErgophobiaConfig.Instance;

			Ergophobia.ErgophobiaAPI.OnPostHouseFurnish( this.OnHouseFurnish );

			ergConfig.SetOverride( nameof(ergConfig.TrackKitSoldByMerchant), false );

			ergConfig.SetOverride( nameof(ergConfig.FurnishedCustomFurnitureTile), (int)TileID.Containers );
			ergConfig.SetOverride( nameof(ergConfig.FurnishedCustomFloorTile), (int)TileID.Mudstone );
			ergConfig.SetOverride( nameof(ergConfig.HouseFramingKitPrice), Item.buyPrice(gold: 12) );
			ergConfig.SetOverride( nameof(ergConfig.HouseFurnishingKitPrice), Item.buyPrice(gold: 7) );
			ergConfig.SetOverride( nameof(ergConfig.TrackDeploymentKitRecipeTile), -1 );
			ergConfig.SetOverride( nameof(ergConfig.TrackDeploymentKitRecipeExtraIngredient), new Dictionary<ItemDefinition, int> {
				//{ new ItemDefinition(ItemID.SilkRopeCoil), 30 },
				{ new ItemDefinition(ItemID.Chain), 15 },
				//{ new ItemDefinition(ItemID.Minecart), 1 },
				//{ new ItemDefinition(ItemID.WoodenBeam), 50 }
			} );
		}


		////////////////

		private void OnHouseFurnish(
					(int x, int y) innerTL,
					(int x, int y) innerTR,
					(int x, int y) outerTL,
					(int x, int y) outerTR,
					int floorL,
					int floorR,
					int floorY,
					(int x, int y) farTL,
					(int x, int y) farTR ) {
			this.CycleCustomFurnishes();

			for( int i=floorL; i<floorR; i++ ) {
				Tile tileU = Framing.GetTileSafely( i, floorY );
				Tile tileD = Framing.GetTileSafely( i, floorY+1 );

				if( tileU.type == TileID.Containers && tileD.type == TileID.Mudstone ) {
					tileD.type = TileID.Platforms;
					tileD.frameX = 0;
					tileD.frameY = 0;
					WorldGen.SquareTileFrame( i, floorY+1 );
				}
			}
		}


		////////////////

		private void CycleCustomFurnishes() {
			var ergConfig = Ergophobia.ErgophobiaConfig.Instance;
			var cycle = ergConfig.Get<List<Ergophobia.HouseKitFurnitureDefinition>>( nameof(ergConfig.HouseKitFurnitureSuccession) );
			var myworld = ModContent.GetInstance<AMWorld>();

			if( myworld.HouseKitFurnitureIdx >= cycle.Count ) {
				return;
			}

			Ergophobia.HouseKitFurnitureDefinition furnDef = cycle[ myworld.HouseKitFurnitureIdx ];
			if( furnDef.IsHardMode && !Main.hardMode ) {
				return;
			}

			ergConfig.SetOverride( nameof( ergConfig.FurnishedCustomFurnitureTile ), (int)furnDef.TileType );

			myworld.HouseKitFurnitureIdx++;
		}
	}
}
