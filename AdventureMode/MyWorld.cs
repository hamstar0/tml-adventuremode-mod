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
	public class AMWorld : ModWorld {
		public bool IsCurrentWorldAdventure { get; internal set; } = false;

		public int HouseKitFurnitureIdx { get; internal set; } = 0;


		public (int TileX, int TileY) JungleSignTile { get; internal set; } = (0, 0);

		public (int TileX, int TileY) RaftBarrelTile { get; internal set; } = (0, 0);


		public Rectangle UndergroundDesertBounds { get; internal set; } = default( Rectangle );

		public (int TileX, int TileY) DungeonBottom { get; internal set; } = (0, 0);



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
			if( tag.ContainsKey( "und_des_zone_x" ) ) {
				int x = tag.GetInt( "und_des_zone_x" );
				int y = tag.GetInt( "und_des_zone_y" );
				int wid = tag.GetInt( "und_des_zone_wid" );
				int hei = tag.GetInt( "und_des_zone_hei" );
				this.UndergroundDesertBounds = new Rectangle( x, y, wid, hei );
			}
			if( tag.ContainsKey( "dung_bot_x" ) ) {
				int x = tag.GetInt( "dung_bot_x" );
				int y = tag.GetInt( "dung_bot_y" );
				this.DungeonBottom = (x, y);
			}

			WorldLogic.LoadRaftRestockInfo( tag );
		}

		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "is_adventure", this.IsCurrentWorldAdventure },
				{ "house_kit_furniture_idx", this.HouseKitFurnitureIdx },
				{ "jungle_sign_loc_x", this.JungleSignTile.TileX },
				{ "jungle_sign_loc_y", this.JungleSignTile.TileY },
				{ "und_des_zone_x", this.UndergroundDesertBounds.X },
				{ "und_des_zone_y", this.UndergroundDesertBounds.Y },
				{ "und_des_zone_wid", this.UndergroundDesertBounds.Width },
				{ "und_des_zone_hei", this.UndergroundDesertBounds.Height },
				{ "dung_bot_x", this.DungeonBottom.TileX },
				{ "dung_bot_y", this.DungeonBottom.TileY }
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

				int undX = reader.ReadInt32();
				int undY = reader.ReadInt32();
				int undWid = reader.ReadInt32();
				int undHei = reader.ReadInt32();
				this.UndergroundDesertBounds = new Rectangle( undX, undY, undWid, undHei );

				int dunX = reader.ReadInt32();
				int dunY = reader.ReadInt32();
				this.DungeonBottom = (dunX, dunY);
			} catch { }
		}

		public override void NetSend( BinaryWriter writer ) {
			try {
				writer.Write( (bool)this.IsCurrentWorldAdventure );
				writer.Write( (int)this.HouseKitFurnitureIdx );
				writer.Write( (int)this.JungleSignTile.TileX );
				writer.Write( (int)this.JungleSignTile.TileY );
				writer.Write( (int)this.UndergroundDesertBounds.X );
				writer.Write( (int)this.UndergroundDesertBounds.Y );
				writer.Write( (int)this.UndergroundDesertBounds.Width );
				writer.Write( (int)this.UndergroundDesertBounds.Height );
				writer.Write( (int)this.DungeonBottom.TileX );
				writer.Write( (int)this.DungeonBottom.TileY );
			} catch { }
		}


		////////////////

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
