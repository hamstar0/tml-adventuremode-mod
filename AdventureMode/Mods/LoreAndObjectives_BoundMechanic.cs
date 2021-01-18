using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Maps;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		private void ApplyLoreAndObjectives_BoundMechanic( bool isNew, bool isDone ) {
			if( isNew && !isDone && !NPC.savedMech ) {
				var myworld = ModContent.GetInstance<AMWorld>();

				Main.instance.LoadNPC( NPCID.BoundMechanic );

				MapMarkers.SetFullScreenMapMarker(
					id: "AdventureMode_Spawn_BoundMechanic",
					tileX: myworld.DungeonBottom.tileX * 16,
					tileY: myworld.DungeonBottom.tileY * 16,
					icon: Main.npcTexture[NPCID.BoundMechanic],
					scale: 1f
				);
			}

			if( isDone || NPC.savedMech ) {
				MapMarkers.RemoveFullScreenMapMarker( "AdventureMode_Spawn_BoundMechanic" );
			}
		}
	}
}
