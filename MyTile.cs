using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode {
	/*class AdventureModeTile : GlobalTile {
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
				return false;
			}
		}


		////

		public override bool CanKillTile( int i, int j, int type, ref bool blockDamaged ) {
			return true;
			bool fail = false, effectOnly = false, noItem = false;
			this.KillTile( i, j, type, ref fail, ref effectOnly, ref noItem );
			return !fail;
		}

		public override void KillTile( int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem ) {
			switch( type ) {
			case TileID.Trees:
			case TileID.MushroomTrees:
			case TileID.PalmTree:
			case TileID.Plants:
			case TileID.Plants2:
			case TileID.CorruptPlants:
			case TileID.CorruptThorns:
			case TileID.FleshWeeds:
			case TileID.CrimtaneThorns:
			case TileID.HallowedPlants:
			case TileID.HallowedPlants2:
			case TileID.HallowedVines:
			case TileID.JunglePlants:
			case TileID.JunglePlants2:
			case TileID.JungleVines:
			case TileID.Coral:
			case TileID.ImmatureHerbs:
			case TileID.BloomingHerbs:
			case TileID.MatureHerbs:
			///
			case TileID.Torches:
			case TileID.Platforms:
			case TileID.Rope:
			case TileID.SilkRope:
			case TileID.VineRope:
			case TileID.WebRope:
			case TileID.Chain:
			case TileID.MinecartTrack:
			case TileID.Heart:
			///
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
			///
			case TileID.DesertFossil:
			case TileID.FossilOre:
			case TileID.Silt:
			case TileID.Slush:
				//fail = false;
				//effectOnly = false;
				//noItem = false;
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
			bool fail=false, effectOnly=false, noItem=false;
			this.KillTile( i, j, type, ref fail, ref effectOnly, ref noItem );
			return !fail || effectOnly;
		}
		public override bool KillSound( int i, int j, int type ) {
			bool fail = false, effectOnly = false, noItem = false;
			this.KillTile( i, j, type, ref fail, ref effectOnly, ref noItem );
			return !fail || effectOnly;
		}
	}*/
}