using HamstarHelpers.Classes.UI.ModConfig;
using HamstarHelpers.Services.Configs;
using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace AdventureMode {
	class MyFloatInputElement : FloatInputElement { }



	
	public partial class AdventureModeConfig : StackableModConfig {
		public static AdventureModeConfig Instance => ModConfigStack.GetMergedConfigs<AdventureModeConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;


		////////////////

		public bool DebugModeInfo { get; set; } = false;

		////

		[Range( 0, 100 )]
		[DefaultValue( 2 )]
		public int GrappleChainAmmoRate { get; set; } = 2;


		[DefaultValue(true)]
		public bool AddRodOfDiscordRecipe { get; set; } = true;

		[Range( 60, 60 * 60 * 60 * 24 )]
		[DefaultValue( 60 * 60 )]
		public int AddedRodOfDiscordChaosStateTime { get; set; } = 60 * 60;



		////////////////

		/*public override ModConfig Clone() {
			var clone = (AdventureModeConfig)base.Clone();
			return clone;
		}*/
	}
}
