using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Maps;
using HamstarHelpers.Services.Timers;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		private void ApplyLoreAndObjectives_BoundMechanic( bool isNew, bool isDone ) {
			string markerName = "AdventureMode_Spawn_BoundMechanic";

			if( !isDone && !NPC.savedMech ) {
				Timers.RunUntil( () => {
					if( NPC.downedBoss3 && !NPC.savedMech ) {
						this.AddBoundMechanicMapMarker( markerName );
					}
					return !NPC.downedBoss3 && !NPC.savedMech;
				}, false );
			} else {
				MapMarkers.RemoveFullScreenMapMarker( markerName );
			}
		}

		private void AddBoundMechanicMapMarker( string markerName ) {
			var myworld = ModContent.GetInstance<AMWorld>();

			Main.instance.LoadNPC( NPCID.BoundMechanic );

			MapMarkers.SetFullScreenMapMarker(
				id: markerName,
				tileX: myworld.DungeonBottom.TileX * 16,
				tileY: myworld.DungeonBottom.TileY * 16,
				icon: Main.npcTexture[NPCID.BoundMechanic],
				scale: 1f
			);
		}
	}
}
