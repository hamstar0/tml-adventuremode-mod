using System;
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

		public static void HighlightRaftMirror( AMWorld myworld ) {
			if( WorldLogic.IsRaftMirrorDiscovered ) {
				return;
			}
			if( !myworld.Raft.IsInitialized ) {
				return;
			}

			IList<(int x, int y)> mirrors = MountedMagicMirrorsAPI.GetDiscoveredMirrors( Main.LocalPlayer );
			int mirrorX = myworld.Raft.Mirror.TileX;
			int mirrorY = myworld.Raft.Mirror.TileY;

			foreach( (int x, int y) in mirrors ) {
				if( Math.Abs( mirrorX - x ) <= 2 && Math.Abs( mirrorY - y ) <= 2 ) {
					WorldLogic.IsRaftMirrorDiscovered = true;
					return;
				}
			}

			float pulse = (float)Main.mouseTextColor / 255f;
			Vector2 dim = Main.fontMouseText.MeasureString( "V" );

			Vector2 pos = new Vector2(
				(mirrorX * 16) - 8 - Main.screenPosition.X,
				(mirrorY * 16) - 8 - Main.screenPosition.Y
			);
			pos = UIZoomLibraries.ApplyZoomFromScreenCenter( pos, null, false, null, null );

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
		}
	}
}
