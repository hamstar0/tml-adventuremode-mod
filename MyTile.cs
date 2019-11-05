using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode {
	class AdventureModeTile : GlobalTile {
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
			switch( type ) {
			case ItemID.WoodPlatform:
				return AdventureModeTile.IsStableForPlatform(i, j, -1) || AdventureModeTile.IsStableForPlatform(i, j, 1);
			case ItemID.Rope:
			case ItemID.SilkRope:
			case ItemID.VineRope:
			case ItemID.WebRope:
			case ItemID.Chain:
			case ItemID.MinecartTrack:
				return true;
			default:
				return false;
			}
		}


		////

		public override bool CanKillTile( int i, int j, int type, ref bool blockDamaged ) {
			return false;
		}

		public override void KillTile( int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem ) {
			switch( type ) {
			case TileID.Platforms:
			case TileID.Rope:
			case TileID.SilkRope:
			case TileID.VineRope:
			case TileID.WebRope:
			case TileID.Chain:
			case TileID.MinecartTrack:
			case TileID.Torches:
			case TileID.Heart:
				fail = false;
				effectOnly = false;
				noItem = true;
				break;
			case TileID.Copper:
			case TileID.Tin:
			case TileID.Iron:
			case TileID.Lead:
			case TileID.Silver:
			case TileID.Tungsten:
			case TileID.Gold:
			case TileID.Platinum:
			case TileID.Meteorite:
			case TileID.Demonite:
			case TileID.Crimtane:
			case TileID.Obsidian:
			case TileID.Hellstone:
			case TileID.Cobalt:
			case TileID.Palladium:
			case TileID.Mythril:
			case TileID.Orichalcum:
			case TileID.Adamantite:
			case TileID.Titanium:
			case TileID.Chlorophyte:
			case TileID.LunarOre:
			///
			case TileID.Amethyst:
			case TileID.Sapphire:
			case TileID.Topaz:
			case TileID.Emerald:
			case TileID.Ruby:
			case TileID.Diamond:
			case TileID.DesertFossil:
			case TileID.FossilOre:
			case TileID.Silt:
			case TileID.Slush:
				fail = false;
				effectOnly = false;
				noItem = false;
				break;
			default:
				fail = true;
				effectOnly = true;
				noItem = true;
				break;
			}
		}

		public override bool Slope( int i, int j, int type ) {
			return false;
		}
		public override bool CreateDust( int i, int j, int type, ref int dustType ) {
			return false;
		}
		public override bool KillSound( int i, int j, int type ) {
			return false;
		}
	}
}