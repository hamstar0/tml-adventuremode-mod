using System;
using System.Collections.Generic;
using System.IO;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;


namespace AdventureMode {
	public class AdventureModeWorld : ModWorld {
		public bool IsCurrentWorldAdventure { get; internal set; } = false;

		public int HouseKitFurnitureIdx { get; internal set; } = 0;

		public (int TileX, int TileY) JungleSignLocation { get; internal set; } = (0, 0);



		////////////////

		public override void Initialize() {
			this.IsCurrentWorldAdventure = false;
			this.HouseKitFurnitureIdx = 0;
			this.JungleSignLocation = (0, 0);
		}

		////

		public override void Load( TagCompound tag ) {
			if( tag.ContainsKey( "is_adventure" ) ) {
				this.IsCurrentWorldAdventure = tag.GetBool( "is_adventure" );
			}
			if( tag.ContainsKey( "house_kit_furniture_idx" ) ) {
				this.HouseKitFurnitureIdx = tag.GetInt( "house_kit_furniture_idx" );
			}
			if( tag.ContainsKey( "jungle_sign_loc_x" ) ) {
				int x = tag.GetInt( "jungle_sign_loc_x" );
				int y = tag.GetInt( "jungle_sign_loc_y" );
				this.JungleSignLocation = (x, y);
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "is_adventure", this.IsCurrentWorldAdventure },
				{ "house_kit_furniture_idx", this.HouseKitFurnitureIdx },
				{ "jungle_sign_loc_x", this.JungleSignLocation.TileX },
				{ "jungle_sign_loc_y", this.JungleSignLocation.TileY }
			};

			return tag;
		}


		////////////////

		public override void NetReceive( BinaryReader reader ) {
			try {
				this.IsCurrentWorldAdventure = reader.ReadBoolean();
				this.HouseKitFurnitureIdx = reader.ReadInt32();

				int signX = reader.ReadInt32();
				int signY = reader.ReadInt32();
				this.JungleSignLocation = (signX, signY);
			} catch { }
		}

		public override void NetSend( BinaryWriter writer ) {
			try {
				writer.Write( (bool)this.IsCurrentWorldAdventure );
				writer.Write( (int)this.HouseKitFurnitureIdx );
				writer.Write( (int)this.JungleSignLocation.TileX );
				writer.Write( (int)this.JungleSignLocation.TileY );
			} catch { }
		}


		////////////////

		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			int idx = tasks.FindIndex( pass => pass.Name.Equals("Grass Wall") );

			if( idx != -1 ) {
				if( AdventureModeConfig.Instance.SetDefaultSpawnToBeach ) {
					tasks.Insert( idx + 1, new PassLegacy( "Adventure Mode: Set Default Spawn", ( progress ) => {
						AdventureModeWorldGen.SetBeachSpawn( progress );
						progress.Value = 1f;
					} ) );
				}
			}

			tasks.Add( new PassLegacy( "Adventure Mode: Create Spawn Boat", ( progress ) => {
				AdventureModeWorldGen.PlaceRaft( progress );
				progress.Value = 1f;
			} ) );

			tasks.Add( new PassLegacy( "Adventure Mode: Create Jungle Sign", ( progress ) => {
				AdventureModeWorldGen.PlaceJungleSign( progress );
				progress.Value = 1f;
			} ) );
		}

		public override void PostWorldGen() {
			this.IsCurrentWorldAdventure = true;
		}
	}
}
