using System;
using Terraria;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static void SetBossDefaults( NPC npc ) {
			float maxLifePercent = AdventureModeConfig.Instance.BossMaxLifePercentOnSpawn;

			npc.lifeMax = (int)( (float)npc.lifeMax * maxLifePercent );
			npc.life = npc.lifeMax;
		}
	}
}
