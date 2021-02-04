using System;
using Terraria;
using Objectives;
using AdventureModeLore.Lore;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadLoreAndObjectives() {
			ObjectivesAPI.AddSubscription( "AdventureMode", (string objectiveName, bool isNew, bool isDone) => {
				if( !isDone ) {
					this.ApplyLoreForObjectiveIf( objectiveName, isNew, isDone );
				}
			} );
		}


		private void ApplyLoreForObjectiveIf( string objectiveName, bool isNew, bool isDone ) {
			switch( objectiveName ) {
			case LoreEvents.ObjectiveTitle_TalkToGoblin:
				this.ApplyLoreAndObjectives_BoundGoblin( isNew, isDone );
				break;
			case LoreEvents.ObjectiveTitle_FindMechanic:
				this.ApplyLoreAndObjectives_BoundMechanic( isNew, isDone );
				break;
			}
		}
	}
}
