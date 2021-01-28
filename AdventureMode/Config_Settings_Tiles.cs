using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader.Config;


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
