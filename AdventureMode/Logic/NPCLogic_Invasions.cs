using System;
using Terraria;
using HamstarHelpers.Helpers.NPCs;
using HamstarHelpers.Helpers.World;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static void SetInvasionDefaults( NPC npc ) {
			//DebugHelpers.Print( "invasionstate", "invasionSize:"+Main.invasionSize
			//	+", invasionProgress:"+Main.invasionProgress
			//	+", invasionProgressMax:"+Main.invasionProgressMax
			//	+", invasionType:"+Main.invasionType
			//	+", y:"+(npc.position.Y < WorldHelpers.DirtLayerTopTileY)
			//	+", wet:"+npc.wet);
			if( npc.position.Y < (WorldHelpers.DirtLayerTopTileY * 16) && npc.wet ) {
				NPCHelpers.Remove( npc );
			}
		}
	}
}
