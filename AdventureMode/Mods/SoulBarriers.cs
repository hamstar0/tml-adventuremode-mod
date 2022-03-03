using System;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadSoulBarriers() {
			var config = SoulBarriers.SoulBarriersConfig.Instance;

			config.SetOverride( nameof(config.NPCBarrierRandomPercentChance), 0f );
		}
	}
}
