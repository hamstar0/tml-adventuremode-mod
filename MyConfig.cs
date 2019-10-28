using HamstarHelpers.Classes.UI.ModConfig;
using System;
using Terraria.ModLoader.Config;


namespace AdventureMode {
	class MyFloatInputElement : FloatInputElement { }




	public class AdventureModeConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;


		////

		public bool DebugModeInfo { get; set; } = false;

		////

		public int ManaCrystalShardsPerManaCrystal { get; set; } = 4;



		////

		/*public override ModConfig Clone() {
			var clone = (AdventureModeConfig)base.Clone();
			return clone;
		}*/
	}
}
