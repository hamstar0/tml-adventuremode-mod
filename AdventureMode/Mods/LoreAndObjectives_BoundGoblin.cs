﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Maps;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		private void ApplyLoreAndObjectives_BoundGoblin( bool isNew, bool isDone ) {
			if( isNew && !isDone && !NPC.savedGoblin ) {
				var myworld = ModContent.GetInstance<AMWorld>();
				int goblinX = myworld.UndergroundDesertBounds.X + (myworld.UndergroundDesertBounds.Width / 2);
				int goblinY = myworld.UndergroundDesertBounds.Y + ((2 * myworld.UndergroundDesertBounds.Width) / 3);

				Main.instance.LoadNPC( NPCID.BoundGoblin );

				MapMarkers.SetFullScreenMapMarker(
					id: "AdventureMode_Spawn_BoundGoblin",
					tileX: goblinX,
					tileY: goblinY,
					icon: Main.npcTexture[NPCID.BoundGoblin],
					scale: 1f
				);
			}

			if( isDone || NPC.savedGoblin ) {
				MapMarkers.RemoveFullScreenMapMarker( "AdventureMode_Spawn_BoundGoblin" );
			}
		}
	}
}
