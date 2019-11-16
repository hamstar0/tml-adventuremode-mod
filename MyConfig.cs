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

		[Range( 0, 100 )]
		[DefaultValue( 2 )]
		public int GrappleChainAmmoRate { get; set; } = 2;



		////////////////

		/*public override ModConfig Clone() {
			var clone = (AdventureModeConfig)base.Clone();
			return clone;
		}*/
	}
}
