using System;
using Terraria;
using Terraria.ID;
using ModLibsGeneral.Libraries.NPCs;
using ModLibsGeneral.Libraries.World;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		internal static void PostSetDefaultsForInvasion( NPC npc ) {
			//DebugLibraries.Print( "invasionstate", "invasionSize:"+Main.invasionSize
			//	+", invasionProgress:"+Main.invasionProgress
			//	+", invasionProgressMax:"+Main.invasionProgressMax
			//	+", invasionType:"+Main.invasionType
			//	+", y:"+(npc.position.Y < WorldLibraries.DirtLayerTopTileY)
			//	+", wet:"+npc.wet);

			// Is above ground
			if( npc.position.Y < (WorldLocationLibraries.DirtLayerTopTileY * 16) ) {
				// Is wet (has spawned underwater)
				if( npc.wet ) {
					NPCLibraries.Remove( npc, Main.netMode == NetmodeID.Server );
				}
			}
		}
	}
}
