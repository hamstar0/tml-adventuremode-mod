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
		[DefaultValue( 0 )]
		public int GrappleChainAmmoRate { get; set; } = 0;

		[DefaultValue( true )]
		public bool ReducedManaCrystalStatIncrease { get; set; } = true;

		[DefaultValue( true )]
		public bool ReducedLifeCrystalStatIncrease { get; set; } = true;

		[Range( 0, 60 * 60 * 30 )]
		[DefaultValue( 60 * 60 * 2 )]
		public int MaximumDangersenseBuffDuration { get; set; } = 60 * 60 * 2;

		//[Range( -1, 60 * 60 * 60 * 2 )]
		//[DefaultValue( 60 * 60 * 3 )]
		//public int NecrotisMaxTickDuration { get; set; } = 60 * 60 * 3;

		[Range( 0f, 1f )]
		[DefaultValue( 1f / 24f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float NecrotisAfflictTickRate { get; set; } = 1f / 24f;

		//[Range( 1, 60 * 60 )]
		//[DefaultValue( 8 )]
		//public int NecrotisRecoverTickRate { get; set; } = 8;

		////

		[DefaultValue( true )]
		[ReloadRequired]
		public bool EnableMechBossItemRecipes { get; set; } = true;

		[DefaultValue( true )]
		[ReloadRequired]
		public bool RespawnBlockedDuringBosses { get; set; } = true;

		////

		[DefaultValue(true)]
		public bool AddRodOfDiscordRecipe { get; set; } = true;

		[Range( 60, 60 * 60 * 60 * 24 )]
		[DefaultValue( 60 * 10 )]
		public int AddedRodOfDiscordChaosStateTime { get; set; } = 60 * 10;

		/*[Range( 0f, 100f )]
		[DefaultValue( 2f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RodOfDiscordPainIncreaseMultiplier { get; set; } = 2f;*/

		[DefaultValue( true )]
		[ReloadRequired]
		public bool RodOfDiscordChaosStateBlocksBlink { get; set; } = true;

		////

		[Range( 0f, 1f )]
		[DefaultValue( 0.02f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PotSurprisePercentChance { get; set; } = 0.02f;

		////

		[DefaultValue( true )]
		public bool NerfReaverShark { get; set; } = true;



		////////////////

		/*public override ModConfig Clone() {
			var clone = (AdventureModeConfig)base.Clone();
			return clone;
		}*/
	}
}
