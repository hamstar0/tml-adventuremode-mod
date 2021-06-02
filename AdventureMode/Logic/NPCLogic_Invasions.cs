using System;
using Terraria;
using ModLibsGeneral.Libraries.NPCs;
using ModLibsGeneral.Libraries.World;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static void SetInvasionDefaults( NPC npc ) {
			//DebugLibraries.Print( "invasionstate", "invasionSize:"+Main.invasionSize
			//	+", invasionProgress:"+Main.invasionProgress
			//	+", invasionProgressMax:"+Main.invasionProgressMax
			//	+", invasionType:"+Main.invasionType
			//	+", y:"+(npc.position.Y < WorldLibraries.DirtLayerTopTileY)
			//	+", wet:"+npc.wet);
			if( npc.position.Y < (WorldLocationLibraries.DirtLayerTopTileY * 16) && npc.wet ) {
				NPCLibraries.Remove( npc );
			}
		}
	}
}
