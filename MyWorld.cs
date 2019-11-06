using HamstarHelpers.Helpers.Debug;
using System;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.World.Generation;


namespace AdventureMode {
	class AdventureModeWorld : ModWorld {
		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			int idx = tasks.FindIndex( pass => pass.Name.Equals("Grass Wall") );

			if( idx != -1 ) {
				tasks.Insert( idx + 1, new PassLegacy( "Adventure Mode: Set Default Spawn", ( progress ) => {
					AdventureModeWorldGen.SetBeachSpawn( progress );
					progress.Value = 1f;
				} ) );
				tasks.Insert( idx + 2, new PassLegacy( "Adventure Mode: Create Spawn Boat", ( progress ) => {
					AdventureModeWorldGen.CreateBoat( progress );
					progress.Value = 1f;
				} ) );
			}
		}
	}
}
