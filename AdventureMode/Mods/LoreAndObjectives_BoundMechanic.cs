﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Services.Timers;
using ModLibsMaps.Services.Maps;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		private void ApplyLoreAndObjectives_BoundMechanic( bool isNew, bool isDone ) {
			string markerName = "AdventureMode_Spawn_BoundMechanic";

			if( !isDone ) {
				Timers.RunUntil( () => {
					if( !isDone && NPC.downedBoss3 && !NPC.savedMech ) {
						this.AddBoundMechanicMapMarker( markerName );

						return false;
					}
					return !isDone;
				}, false );
			} else {
				MapMarkersAPI.RemoveFullScreenMapMarker( markerName );
			}
		}

		private void AddBoundMechanicMapMarker( string markerName ) {
			var myworld = ModContent.GetInstance<AMWorld>();

			Main.instance.LoadNPC( NPCID.BoundMechanic );

			MapMarkersAPI.SetFullScreenMapMarker(
				id: markerName,
				tileX: myworld.DungeonBottom.TileX,
				tileY: myworld.DungeonBottom.TileY,
				icon: Main.npcTexture[ NPCID.BoundMechanic ],
				scale: 1f
			);
		}
	}
}
