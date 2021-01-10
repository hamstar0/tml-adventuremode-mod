using System;
using Terraria;
using AdventureModeLore.Lore;
using Objectives;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadLoreAndObjectives() {
			ObjectivesAPI.AddSubscription( "AdventureMode", (string objectiveName, bool isNew, bool isDone) => {
				if( objectiveName.Equals(LoreEvents.ObjectiveTitle_TalkToGoblin) ) {
					this.LoadLoreAndObjectives_BoundGoblin( isNew, isDone );
				}
				if( objectiveName.Equals( LoreEvents.ObjectiveTitle_FindMechanic) ) {
					this.LoadLoreAndObjectives_BoundMechanic( isNew, isDone );
				}
			} );
		}
	}
}
