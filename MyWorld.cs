using HamstarHelpers.Helpers.Debug;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;


namespace AdventureMode {
	class AdventureModeWorld : ModWorld {
		public int HouseKitFurnitureIdx { get; internal set; } = 0;



		////////////////

		public override void Load( TagCompound tag ) {
			if( tag.ContainsKey( "house_kit_furniture_idx" ) ) {
				this.HouseKitFurnitureIdx = tag.GetInt( "house_kit_furniture_idx" );
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "house_kit_furniture_idx", this.HouseKitFurnitureIdx }
			};

			return tag;
		}


		////////////////

		public override void NetReceive( BinaryReader reader ) {
			try {
				this.HouseKitFurnitureIdx = reader.ReadInt32();
			} catch { }
		}

		public override void NetSend( BinaryWriter writer ) {
			try {
				writer.Write( (int)this.HouseKitFurnitureIdx );
			} catch { }
		}


		////////////////

		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			int idx = tasks.FindIndex( pass => pass.Name.Equals("Grass Wall") );

			if( idx != -1 ) {
				if( AdventureModeConfig.Instance.SetDefaultSpawnToBeach ) {
					tasks.Insert( idx - 1, new PassLegacy( "Adventure Mode: Set Default Spawn", ( progress ) => {
						AdventureModeWorldGen.SetBeachSpawn( progress );
						progress.Value = 1f;
					} ) );
				}
			}

			tasks.Add( new PassLegacy( "Adventure Mode: Create Spawn Boat", ( progress ) => {
				AdventureModeWorldGen.PlaceRaft( progress );
				progress.Value = 1f;
			} ) );
		}
	}
}
