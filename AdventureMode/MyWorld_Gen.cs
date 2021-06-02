using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;
using AdventureMode.WorldGeneration;


namespace AdventureMode {
	public partial class AMWorld : ModWorld {
		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			int idx = tasks.FindIndex( pass => pass.Name.Equals("Grass Wall") );

			if( idx != -1 ) {
				if( AMConfig.Instance.SetDefaultSpawnToBeach ) {
					tasks.Insert( idx + 1, new PassLegacy( "Adventure Mode: Set Default Spawn", ( progress ) => {
						AMWorldGen.SetBeachSpawn( progress );
						progress.Value = 1f;
					} ) );
				}
			}
			
			tasks.Add( new PassLegacy( "Adventure Mode: Create Spawn Boat", ( progress ) => {
				AMWorldGen.PlaceRaft( this, progress );
				progress.Value = 1f;
			} ) );

			tasks.Add( new PassLegacy( "Adventure Mode: Create Jungle Sign", ( progress ) => {
				AMWorldGen.PlaceJungleSign( progress );
				progress.Value = 1f;
			} ) );

			tasks.Add( new PassLegacy( "Adventure Mode: Create Corrupt Sign", ( progress ) => {
				AMWorldGen.PlaceCorruptionSign( progress );
				progress.Value = 1f;
			} ) );

			tasks.Add( new PassLegacy( "Adventure Mode: Create Snow Sign", ( progress ) => {
				AMWorldGen.PlaceSnowSign( progress );
				progress.Value = 1f;
			} ) );

			tasks.Add( new PassLegacy( "Adventure Mode: Underground Desert Scan", ( progress ) => {
				progress.Message = "Scanning for underground desert expanse";
				AMWorldGen.ScanUndergroundDesert( progress );
				progress.Value = 1f;
			} ) );

			tasks.Add( new PassLegacy( "Adventure Mode: Dungeon Scan", ( progress ) => {
				AMWorldGen.ScanDungeon( progress );
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
			int raftX = this.Raft.Barrel.TileX - 1;
			int raftY = this.Raft.Barrel.TileY - 1;

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
