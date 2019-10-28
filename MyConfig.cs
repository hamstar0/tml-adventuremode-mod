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

		public int TinyWorldManaCrystalShards { get; set; } = 160;
		public int SmallWorldManaCrystalShards { get; set; } = 192;
		public int MediumWorldManaCrystalShards { get; set; } = 384;
		public int LargeWorldManaCrystalShards { get; set; } = 576;
		public int HugeWorldManaCrystalShards { get; set; } = 768;



		////

		/*public override ModConfig Clone() {
			var clone = (AdventureModeConfig)base.Clone();
			return clone;
		}*/
	}
}
