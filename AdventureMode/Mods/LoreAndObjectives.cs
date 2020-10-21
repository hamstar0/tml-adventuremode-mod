using System;
using Terraria;
using AdventureModeLore.Logic;
using Objectives;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadLoreAndObjectives() {
			ObjectivesAPI.AddSubscription( "AdventureMode", (string objectiveName, bool isNew, bool isDone) => {
				if( objectiveName.Equals(AMLLogic.FindGoblinTitle) ) {
					this.LoadLoreAndObjectives_BoundGoblin( isNew, isDone );
				}
				if( objectiveName.Equals(AMLLogic.FindMechanicTitle) ) {
					this.LoadLoreAndObjectives_BoundMechanic( isNew, isDone );
				}
			} );
		}
	}
}
