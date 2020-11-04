using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Logic;
using AdventureMode.WorldGeneration;


namespace AdventureMode {
	public class AdventureModeWorld : ModWorld {
		public bool IsCurrentWorldAdventure { get; internal set; } = false;

		public int HouseKitFurnitureIdx { get; internal set; } = 0;


		public (int TileX, int TileY) JungleSignTile { get; internal set; } = (0, 0);

		public (int TileX, int TileY) RaftBarrelTile { get; internal set; } = (0, 0);


		public Rectangle UndergroundDesertBounds { get; internal set; } = default( Rectangle );

		public (int tileX, int tileY) DungeonBottom { get; internal set; } = (0, 0);



		////////////////

		public override void Initialize() {
			this.IsCurrentWorldAdventure = false;
			this.HouseKitFurnitureIdx = 0;
			this.JungleSignTile = (0, 0);
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
				this.JungleSignTile = (x, y);
			}

			WorldLogic.LoadRaftRestockInfo( tag );
		}

		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "is_adventure", this.IsCurrentWorldAdventure },
				{ "house_kit_furniture_idx", this.HouseKitFurnitureIdx },
				{ "jungle_sign_loc_x", this.JungleSignTile.TileX },
				{ "jungle_sign_loc_y", this.JungleSignTile.TileY }
			};

			WorldLogic.SaveRaftRestockInfo( tag );

			return tag;
		}


		////////////////

		public override void NetReceive( BinaryReader reader ) {
			try {
				this.IsCurrentWorldAdventure = reader.ReadBoolean();
				this.HouseKitFurnitureIdx = reader.ReadInt32();

				int signX = reader.ReadInt32();
				int signY = reader.ReadInt32();
				this.JungleSignTile = (signX, signY);
			} catch { }
		}

		public override void NetSend( BinaryWriter writer ) {
			try {
				writer.Write( (bool)this.IsCurrentWorldAdventure );
				writer.Write( (int)this.HouseKitFurnitureIdx );
				writer.Write( (int)this.JungleSignTile.TileX );
				writer.Write( (int)this.JungleSignTile.TileY );
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
				AdventureModeWorldGen.PlaceRaft( this, progress );
				progress.Value = 1f;
			} ) );

			tasks.Add( new PassLegacy( "Adventure Mode: Create Jungle Sign", ( progress ) => {
				AdventureModeWorldGen.PlaceJungleSign( progress );
				progress.Value = 1f;
			} ) );

			tasks.Add( new PassLegacy( "Adventure Mode: Underground Desert Scan", ( progress ) => {
				AdventureModeWorldGen.ScanUndergroundDesert( progress );
				progress.Value = 1f;
			} ) );

			tasks.Add( new PassLegacy( "Adventure Mode: Dungeon Scan", ( progress ) => {
				AdventureModeWorldGen.ScanDungeon( progress );
				progress.Value = 1f;
			} ) );
		}

		public override void PostWorldGen() {
			this.IsCurrentWorldAdventure = true;

			int guideWho = NPC.FindFirstNPC( NPCID.Guide );
			if( guideWho != -1 ) {
				Main.npc[ guideWho ].Center = new Vector2(Main.spawnTileX, Main.spawnTileY - 1) * 16f;
			}
		}


		////////////////

		public int GetRaftBarrelChestIndex() {
			int raftX = this.RaftBarrelTile.TileX - 1;
			int raftY = this.RaftBarrelTile.TileY - 1;

			for( int i=0; i<2; i++ ) {
				for( int j=0; j<2; j++ ) {
					int chestIdx = Chest.FindChest( raftX + i, raftY + j );
					if( chestIdx != -1 ) { return chestIdx; }
				}
			}

			return -1;
		}
	}
}
