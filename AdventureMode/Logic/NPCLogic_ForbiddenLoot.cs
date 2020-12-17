using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static void ApplyForbiddenLoot() {
			NPCLoader.blockLoot.Add( ItemID.Hook );
			NPCLoader.blockLoot.Add( ItemID.Present );
		}
	}
}
