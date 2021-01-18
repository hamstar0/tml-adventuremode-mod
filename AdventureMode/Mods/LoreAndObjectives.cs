using System;
using Terraria;
using AdventureModeLore.Lore;
using Objectives;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadLoreAndObjectives() {
			ObjectivesAPI.AddSubscription( "AdventureMode", (string objectiveName, bool isNew, bool isDone) => {
				if( !isDone ) {
					this.ApplyLoreForObjectiveIf( objectiveName, isNew );
				}
			} );
		}


		private void ApplyLoreForObjectiveIf( string objectiveName, bool isNew ) {
			switch( objectiveName ) {
			case LoreEvents.ObjectiveTitle_TalkToGoblin:
				this.ApplyLoreAndObjectives_BoundGoblin( isNew, true );
				break;
			case LoreEvents.ObjectiveTitle_FindMechanic:
				this.ApplyLoreAndObjectives_BoundMechanic( isNew, true );
				break;
			}
		}
	}
}
