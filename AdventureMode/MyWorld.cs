using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ModLibsCore.Libraries.Debug;
using AdventureMode.Logic;
using AdventureMode.WorldGeneration;


namespace AdventureMode {
	public partial class AMWorld : ModWorld {
		public bool IsCurrentWorldAdventure { get; internal set; } = false;

		public int HouseKitFurnitureIdx { get; internal set; } = 0;


		public (int TileX, int TileY) JungleSignTile { get; internal set; } = (0, 0);

		public RaftComponents Raft { get; internal set; } = new RaftComponents();


		public Rectangle UndergroundDesertBounds { get; internal set; } = default( Rectangle );

		public (int TileX, int TileY) DungeonBottom { get; internal set; } = (0, 0);

		public (int TileX, int TileY) OldSpawn { get; internal set; } = (0, 0);

		public (int TileX, int TileY) NewSpawn { get; internal set; } = (0, 0);



		////////////////

		public override void Initialize() {
			this.IsCurrentWorldAdventure = false;
			this.HouseKitFurnitureIdx = 0;
			this.JungleSignTile = (0, 0);
			this.Raft = new RaftComponents();
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
			if( tag.ContainsKey( "old_spawn_x" ) ) {
				int x = tag.GetInt( "old_spawn_x" );
				int y = tag.GetInt( "old_spawn_y" );
				this.OldSpawn = (x, y);
				x = tag.GetInt( "new_spawn_x" );
				y = tag.GetInt( "new_spawn_y" );
				this.NewSpawn = (x, y);
			}

			WorldLogic.LoadRaftInfo( tag );
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
				{ "dung_bot_y", this.DungeonBottom.TileY },
				{ "old_spawn_x", this.OldSpawn.TileX },
				{ "old_spawn_y", this.OldSpawn.TileY },
				{ "new_spawn_x", this.NewSpawn.TileX },
				{ "new_spawn_y", this.NewSpawn.TileY }
			};

			WorldLogic.SaveRaftInfo( tag );

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

				int oldSX = reader.ReadInt32();
				int oldSY = reader.ReadInt32();
				this.OldSpawn = (oldSX, oldSY);

				int newSX = reader.ReadInt32();
				int newSY = reader.ReadInt32();
				this.NewSpawn = (newSX, newSY);
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
				writer.Write( (int)this.DungeonBottom.TileX );
				writer.Write( (int)this.OldSpawn.TileX );
				writer.Write( (int)this.OldSpawn.TileY );
				writer.Write( (int)this.NewSpawn.TileX );
				writer.Write( (int)this.NewSpawn.TileY );
			} catch { }
		}


		////////////////

		public override void PostDrawTiles() {
			WorldLogic.HighlightRaftMirror( this );
		}
	}
}
