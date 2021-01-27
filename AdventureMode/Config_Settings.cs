using System;
using Terraria;
using Terraria.ModLoader.Config;


namespace AdventureMode {
	public partial class AMConfig : ModConfig {
		public bool DebugModeInfo { get; set; } = false;

		public bool DebugModeSkipPlayerValidityCheck { get; set; } = false;

		public bool DebugModeSkipWorldValidityCheck { get; set; } = false;
	}
}
