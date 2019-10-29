using HamstarHelpers.Classes.UI.ModConfig;
using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace AdventureMode {
	class MyFloatInputElement : FloatInputElement { }




	public class AdventureModeConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;



		////

		public bool DebugModeInfo { get; set; } = false;


		////

		[Range( 0, 100 )]
		[DefaultValue( 4 )]
		public int ManaCrystalShardsPerManaCrystal { get; set; } = 4;

		[Range( 10, 1000 )]
		[DefaultValue( 80 )]
		public int ManaCrystalShardTeleportRadius { get; set; } = 80;


		[DefaultValue( 160 )]
		public int TinyWorldManaCrystalShards { get; set; } = 160;

		[DefaultValue( 192 )]
		public int SmallWorldManaCrystalShards { get; set; } = 192;

		[DefaultValue( 384 )]
		public int MediumWorldManaCrystalShards { get; set; } = 384;

		[DefaultValue( 576 )]
		public int LargeWorldManaCrystalShards { get; set; } = 576;

		[DefaultValue( 768 )]
		public int HugeWorldManaCrystalShards { get; set; } = 768;



		////

		/*public override ModConfig Clone() {
			var clone = (AdventureModeConfig)base.Clone();
			return clone;
		}*/
	}
}
