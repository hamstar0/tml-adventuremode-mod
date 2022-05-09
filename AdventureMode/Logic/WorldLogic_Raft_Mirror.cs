﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.UI;
using MountedMagicMirrors;


namespace AdventureMode.Logic {
	static partial class WorldLogic {
		private static bool IsRaftMirrorDiscovered = false;
		


		////

		public static bool HighlightRaftMirror_Local_If( AMWorld myworld, out bool hasDrawn ) {
			if( WorldLogic.IsRaftMirrorDiscovered ) {
				hasDrawn = false;
				return false;
			}
			if( !myworld.Raft.IsInitialized ) {
				hasDrawn = false;
				return false;
			}

			//

			IList<(int x, int y)> mirrors = MountedMagicMirrorsAPI.GetDiscoveredMirrors( Main.LocalPlayer );
			int mirrorX = myworld.Raft.Mirror.TileX;
			int mirrorY = myworld.Raft.Mirror.TileY;

			foreach( (int x, int y) in mirrors ) {
				if( Math.Abs(mirrorX - x) <= 2 && Math.Abs(mirrorY - y) <= 2 ) {
					WorldLogic.IsRaftMirrorDiscovered = true;

					hasDrawn = false;
					return true;
				}
			}

			//

			float pulse = (float)Main.mouseTextColor / 255f;
			Vector2 dim = Main.fontMouseText.MeasureString( "V" );

			Vector2 pos = new Vector2(
				(mirrorX * 16) - 8 - Main.screenPosition.X,
				(mirrorY * 16) - 8 - Main.screenPosition.Y
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
