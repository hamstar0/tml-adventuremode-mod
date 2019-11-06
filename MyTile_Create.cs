using HamstarHelpers.Helpers.TModLoader;
using MountedMagicMirrors.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode {
	partial class AdventureModeTile : GlobalTile {
		public static bool IsStableForPlatform( int tileX, int tileY, int dirX ) {
			for( int i=1; i<=8; i++ ) {
				Tile tile = Framing.GetTileSafely( tileX + (i * dirX), tileY );
				if( !tile.active() || !Main.tileSolid[tile.type] ) {
					break;
				}

				if( !Main.tileSolidTop[tile.type] && tile.type != TileID.MagicalIceBlock ) {
					return true;
				}
			}

			return false;
		}



		////////////////

		public override bool CanPlace( int i, int j, int type ) {
			if( !LoadHelpers.IsCurrentPlayerInGame() ) {
				return true;
			}

			switch( type ) {
			case TileID.Platforms:
				return AdventureModeTile.IsStableForPlatform(i, j, -1) || AdventureModeTile.IsStableForPlatform(i, j, 1);
			case TileID.Rope:
			case TileID.SilkRope:
			case TileID.VineRope:
			case TileID.WebRope:
			case TileID.Chain:
			case TileID.MinecartTrack:
			///
			case TileID.Torches:
			case TileID.Campfire:
			///
			case TileID.Saplings:
			case TileID.Pumpkins:
			case TileID.ImmatureHerbs:
			case TileID.MatureHerbs:
			case TileID.BloomingHerbs:
			case TileID.Sunflower:
			///
			//case TileID.Plants:
			//case TileID.Plants2:
			//case TileID.JunglePlants:
			//case TileID.JunglePlants2:
			//case TileID.MushroomPlants:
			//case TileID.HallowedPlants:
			//case TileID.HallowedPlants2:
			//case TileID.CorruptPlants:
			//case TileID.FleshWeeds:
				return true;
			default:
				if( type == ModContent.TileType<MountedMagicMirrorTile>() ) {
					return true;
				}

				return false;
			}
		}
	}
}