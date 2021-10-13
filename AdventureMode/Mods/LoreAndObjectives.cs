using System;
using Terraria;
using ModLibsCore.Libraries.Debug;
using Objectives;
using AdventureModeLore.Lore.Dialogues.Events;


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
			case DialogueLoreEventDefinitions.ObjectiveTitle_TalkToGoblin:
			case DialogueLoreEventDefinitions.ObjectiveTitle_RescueGoblin:
				this.ApplyLoreAndObjectives_BoundGoblin( isNew, isDone );
				break;
			case DialogueLoreEventDefinitions.ObjectiveTitle_FindMechanic:
				this.ApplyLoreAndObjectives_BoundMechanic( isNew, isDone );
				break;
			}
		}
	}
}
