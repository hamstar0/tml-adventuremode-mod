using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.NPCs;
using HamstarHelpers.Helpers.TModLoader;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;


namespace AdventureMode {
	partial class AdventureModeTile : GlobalTile {
		public static bool IsKillable( int tileType ) {
			switch( tileType ) {
			case TileID.WoodBlock:
			case TileID.BorealWood:
			case TileID.RichMahogany:
			///
			case TileID.Trees:
			case TileID.MushroomTrees:
			case TileID.PalmTree:
			case TileID.Plants:
			case TileID.Plants2:
			case TileID.Vines:
			case TileID.CorruptPlants:
			case TileID.CorruptThorns:
			case TileID.FleshWeeds:
			case TileID.CrimtaneThorns:
			case TileID.CrimsonVines:
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
			case TileID.Cobweb:
			case TileID.IceBrick:
			case TileID.MagicalIceBlock:
			case TileID.BlueMoss:
			case TileID.BrownMoss:
			case TileID.GreenMoss:
			case TileID.LavaMoss:
			case TileID.LongMoss:
			case TileID.PurpleMoss:
			case TileID.RedMoss:
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
			case TileID.Pots:
			case TileID.ShadowOrbs:
			case TileID.DemonAltar:
			case TileID.LifeFruit:
			case TileID.PlanteraBulb:
			case TileID.Bottles:
			case TileID.Books:
			case TileID.WaterCandle:
			case TileID.PeaceCandle:
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
			case TileID.ExposedGems:
			///
			case TileID.DesertFossil:
			case TileID.FossilOre:
			case TileID.Silt:
			case TileID.Slush:
			///
			case TileID.CopperCoinPile:
			case TileID.SilverCoinPile:
			case TileID.GoldCoinPile:
			case TileID.PlatinumCoinPile:
			///
			case TileID.Boulder:
				return true;
			}
			return false;
		}



		////////////////

		/*public override bool CanKillTile( int i, int j, int type, ref bool blockDamaged ) {
			bool fail = false, effectOnly = false, noItem = false;
			this.KillTile( i, j, type, ref fail, ref effectOnly, ref noItem );
			return !fail;
		}*/

		public override void KillTile( int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem ) {
//LogHelpers.LogAndPrintOnce( "KillTile "+TileID.Search.GetName(type)+" at "+i+","+j);
			if( !LoadHelpers.IsCurrentPlayerInGame() ) {
				return;
			}

			if( !AdventureModeTile.IsKillable(type) ) {
				fail = true;
				effectOnly = true;
				noItem = true;
			}
			//else {
			//fail = false;
			//effectOnly = false;
			//noItem = false;
			//}

			// Arachnophobes, rejoince!
			if( type == TileID.Pots ) {
LogHelpers.Log("1!");
				UnifiedRandom rand = TmlHelpers.SafelyGetRand();

				if( rand.NextFloat() < AdventureModeConfig.Instance.PotSurprisePercentChange ) {
LogHelpers.Log("2!");
					noItem = true;

					if( Main.netMode != 1 ) {
LogHelpers.Log("3!");
						int spiderType = NPCID.WallCreeper;
						if( Main.hardMode ) {
							spiderType = NPCID.BlackRecluse;
						}

						int npcWho = NPC.NewNPC( i << 4, ( j + 1 ) << 4, spiderType );
						Main.npc[npcWho].netUpdate = true;
LogHelpers.Log("4!");

						if( Main.netMode == 2 && npcWho < Main.npc.Length - 1 ) {
LogHelpers.Log("5!");
							NetMessage.SendData( MessageID.SyncNPC, -1, -1, null, npcWho, 0f, 0f, 0f, 0, 0, 0 );
						}
					}
				}
			}
		}

		public override bool Slope( int i, int j, int type ) {
			if( !LoadHelpers.IsCurrentPlayerInGame() ) {
				return true;
			}

			return false;
		}

		public override bool CreateDust( int i, int j, int type, ref int dustType ) {
			if( !LoadHelpers.IsCurrentPlayerInGame() ) {
				return true;
			}

			return AdventureModeTile.IsKillable( type );
			//bool fail=false, effectOnly=false, noItem=false;
			//this.KillTile( i, j, type, ref fail, ref effectOnly, ref noItem );
			//return !fail || effectOnly;
		}

		/*public override bool KillSound( int i, int j, int type ) {
			if( !LoadHelpers.IsCurrentPlayerInGame() ) {
				return true;
			}

			return AdventureModeTile.IsKillable( type );
			//bool fail = false, effectOnly = false, noItem = false;
			//this.KillTile( i, j, type, ref fail, ref effectOnly, ref noItem );
			//return !fail || effectOnly;
		}*/
	}
}