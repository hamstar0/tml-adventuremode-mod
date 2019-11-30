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

		[DefaultValue( true )]
		public bool ReducedManaCrystalStatIncrease { get; set; } = true;

		[DefaultValue( true )]
		public bool ReducedLifeCrystalStatIncrease { get; set; } = true;

		[Range( -1, 60 * 60 * 60 * 2 )]
		[DefaultValue( 60 * 60 * 15 )]
		public int NecrotisTickDuration = 60 * 60 * 15;

		[DefaultValue( true )]
		[ReloadRequired]
		public bool EnableMechBossItemRecipes { get; set; } = true;

		////

		[DefaultValue(true)]
		public bool AddRodOfDiscordRecipe { get; set; } = true;

		[Range( 60, 60 * 60 * 60 * 24 )]
		[DefaultValue( 60 * 30 )]
		public int AddedRodOfDiscordChaosStateTime { get; set; } = 60 * 30;

		[Range( 0f, 100f )]
		[DefaultValue( 2f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RodOfDiscordPainIncreaseMultiplier { get; set; } = 2f;

		////

		[Range( 0f, 1f )]
		[DefaultValue( 0.05f / 4f )]    // divide by 4 since pot is 2x2?
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PotSurprisePercentChance { get; set; } = 0.05f / 4f;



		////////////////

		/*public override ModConfig Clone() {
			var clone = (AdventureModeConfig)base.Clone();
			return clone;
		}*/
	}
}
