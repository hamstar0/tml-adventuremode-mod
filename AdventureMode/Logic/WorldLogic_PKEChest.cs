using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.Items;
using ModLibsGeneral.Libraries.UI;
using LostExpeditions;


namespace AdventureMode.Logic {
	static partial class WorldLogic {
		private static bool IsPKEChestDiscovered = false;



		////////////////

		private static (int tileX, int tileY)? GetPKEChestLocation_Caches( AMWorld myworld ) {
			if( myworld.CachedPKEChestTile.HasValue ) {
				return myworld.CachedPKEChestTile;
			}

			//

			int pkeItemType = ModContent.ItemType<PKEMeter.Items.PKEMeterItem>();

			(int x, int y)[] expeditions = LostExpeditionsAPI.GetLostExpeditionLocations();

			foreach( (int x, int y) in expeditions ) {
				int chestIdx = -1;

				for( int i=0; i<Main.chest.Length; i++ ) {
					Chest chest = Main.chest[i];
					if( chest?.x == x && chest?.y == y ) {
						chestIdx = i;

						break;
					}
					if( chest?.x == x-1 && chest?.y == y ) {
						chestIdx = i;

						break;	//?
					}
					if( chest?.x == x && chest?.y == y-1 ) {
						chestIdx = i;

						break;
					}
					if( chest?.x == x-1 && chest?.y == y-1 ) {
						chestIdx = i;

						break;
					}
				}

				//

				if( chestIdx != -1 ) {
					Item pke = Main.chest[chestIdx]?
						.item?
						.FirstOrDefault( i => i?.Is(pkeItemType) == true );

					if( pke != null ) {
						myworld.CachedPKEChestTile = (x+1, y);
						myworld.CachedPKEChestIdx = chestIdx;

						break;
					}
				}
			}

			return myworld.CachedPKEChestTile;
		}


		////////////////

		public static bool HighlightPKEChest_Local_If( AMWorld myworld, out bool hasDrawn ) {
			if( WorldLogic.IsPKEChestDiscovered ) {
				hasDrawn = false;
				return false;
			}

			//

			(int x, int y)? pkeChestTile = WorldLogic.GetPKEChestLocation_Caches( myworld );
//DebugLibraries.Print( "pke chest", "tile: "+pkeChestTile );
			if( !pkeChestTile.HasValue ) {
				hasDrawn = false;
				return false;
			}

			//

			int pkeItemType = ModContent.ItemType<PKEMeter.Items.PKEMeterItem>();

			if( Main.LocalPlayer.inventory.Any(i => i?.active == true && i.type == pkeItemType) ) {
				WorldLogic.IsPKEChestDiscovered = true;

				hasDrawn = false;
				return true;
			}

			//

			Item chestPKEItem = Main.chest[ myworld.CachedPKEChestIdx ]?
				.item?
				.FirstOrDefault( i => i.type == pkeItemType );

			if( chestPKEItem == null ) {
				WorldLogic.IsPKEChestDiscovered = true;

				hasDrawn = false;
				return true;
			}

			//

			float pulse = (float)Main.mouseTextColor / 255f;
			Vector2 dim = Main.fontMouseText.MeasureString( "V" );

			var wldPos = new Vector2( pkeChestTile.Value.x, pkeChestTile.Value.y );
			wldPos *= 16f;
			//wldPos.X += 8f;
			wldPos.Y -= 8f;
			var pos = new Vector2(
				wldPos.X - Main.screenPosition.X,
				wldPos.Y - Main.screenPosition.Y
			);
			pos = UIZoomLibraries.ApplyZoomFromScreenCenter( pos, null, false );

			//

			if( pos.X < -Main.screenWidth || pos.X > (Main.screenWidth * 2) ) {
				hasDrawn = false;
				return true;
			}

			if( pos.Y < -Main.screenHeight || pos.Y > (Main.screenHeight * 2) ) {
				hasDrawn = false;
				return true;
			}

			//

			Main.spriteBatch.Begin();
			Utils.DrawBorderStringFourWay(
				sb: Main.spriteBatch,
				font: Main.fontMouseText,
				text: "V",
				x: pos.X,
				y: pos.Y,
				textColor: Color.Lime * pulse,
				borderColor: Color.Black * pulse,
				origin: dim * 0.5f,
				scale: 2f * pulse
			);
			Main.spriteBatch.End();

			//

			hasDrawn = true;
			return true;
		}
	}
}
