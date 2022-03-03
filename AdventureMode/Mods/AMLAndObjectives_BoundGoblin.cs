using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using ModLibsMaps.Services.Maps;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		private void ApplyLoreAndObjectives_BoundGoblin( bool isNew, bool isDone ) {
			string markerName = "AdventureMode_Spawn_BoundGoblin";
			
			if( !isDone ) {
				Main.instance.LoadNPC( NPCID.BoundGoblin );
				
				Timers.RunUntil( () => {
					if( NPC.downedGoblins ) {
						if( !NPC.savedGoblin ) {
							this.AddBoundGoblinMapMarker( markerName );
						} else {
							MapMarkersAPI.RemoveFullScreenMapMarker( markerName );
						}
					}

					return NPC.savedGoblin;
				}, false );
			} else {
				MapMarkersAPI.RemoveFullScreenMapMarker( markerName );
			}
		}


		private void AddBoundGoblinMapMarker( string markerName ) {
			var myworld = ModContent.GetInstance<AMWorld>();
			int goblinX = myworld.UndergroundDesertBounds.X + (myworld.UndergroundDesertBounds.Width / 2);
			int goblinY = myworld.UndergroundDesertBounds.Y + ((2 * myworld.UndergroundDesertBounds.Height) / 3);

			MapMarkersAPI.SetFullScreenMapMarker(
				id: markerName,
				tileX: goblinX,
				tileY: goblinY,
				icon: Main.npcTexture[ NPCID.BoundGoblin ],
				scale: 1f
			);
		}
	}
}
