using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Maps;
using Objectives;
using AdventureModeLore.Logic;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		private void LoadLoreAndObjectives_BoundGoblin() {
			ObjectivesAPI.AddSubscription( "AdventureMode", (string objectiveName, bool isNew, bool isDone) => {
				if( objectiveName.Equals(AMLLogic.FindGoblinTitle) ) {
					if( isNew && !isDone ) {
						var myworld = ModContent.GetInstance<AdventureModeWorld>();
						int goblinX = myworld.UndergroundDesertBounds.X + (myworld.UndergroundDesertBounds.Width / 2);
						int goblinY = myworld.UndergroundDesertBounds.Y + ((2 * myworld.UndergroundDesertBounds.Width) / 3);

						Main.instance.LoadNPC( NPCID.BoundGoblin );

						MapMarkers.AddFullScreenMapMarker(
							tileX: goblinX,
							tileY: goblinY,
							label: "AdventureModeBoundGoblin",
							icon: Main.npcTexture[NPCID.BoundGoblin]
						);
					}
					if( isDone ) {
						MapMarkers.RemoveFullScreenMapMarker( "AdventureModeBoundGoblin" );
					}
				}
			} );
		}
	}
}
