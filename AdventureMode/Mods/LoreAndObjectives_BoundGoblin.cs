﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Maps;
using HamstarHelpers.Services.Timers;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		private void ApplyLoreAndObjectives_BoundGoblin( bool isNew, bool isDone ) {
			string markerName = "AdventureMode_Spawn_BoundGoblin";

			if( !isDone && !NPC.savedGoblin ) {
				Timers.RunUntil( () => {
					if( NPC.downedGoblins && !NPC.savedGoblin ) {
						this.AddBoundGoblinMapMarker(  markerName );
					}
					return !NPC.downedGoblins && !NPC.savedGoblin;
				}, false );
			} else {
				MapMarkers.RemoveFullScreenMapMarker( markerName );
			}
		}

		private void AddBoundGoblinMapMarker( string markerName ) {
			var myworld = ModContent.GetInstance<AMWorld>();
			int goblinX = myworld.UndergroundDesertBounds.X + (myworld.UndergroundDesertBounds.Width / 2);
			int goblinY = myworld.UndergroundDesertBounds.Y + ((2 * myworld.UndergroundDesertBounds.Width) / 3);

			Main.instance.LoadNPC( NPCID.BoundGoblin );

			MapMarkers.SetFullScreenMapMarker(
				id: markerName,
				tileX: goblinX,
				tileY: goblinY,
				icon: Main.npcTexture[NPCID.BoundGoblin],
				scale: 1f
			);
		}
	}
}
