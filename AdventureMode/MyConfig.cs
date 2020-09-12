using System;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.UI.ModConfig;


namespace AdventureMode {
	class MyFloatInputElement : FloatInputElement { }




	public partial class AdventureModeConfig : ModConfig {
		public static AdventureModeConfig Instance => ModContent.GetInstance<AdventureModeConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;


		////////////////

		public bool DebugModeInfo { get; set; } = false;

		////

		[DefaultValue( true )]
		[ReloadRequired]
		public bool RemoveRecipeTileRequirements { get; set; } = true;

		////

		[DefaultValue( true )]
		public bool GrappleOnlyWoodAndPlatforms { get; set; } = true;

		////

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

		[DefaultValue( false )]
		[ReloadRequired]
		public bool EnableAlchemyRecipes { get; set; } = false;

		[DefaultValue( true )]
		[ReloadRequired]
		public bool EnableBossItemRecipes { get; set; } = true;

		[DefaultValue( false )]
		[ReloadRequired]
		public bool EnableTorchRecipes { get; set; } = false;
		
		////

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

		[Range( -1, 99000000 )]
		[DefaultValue( 350000 )]
		public int RocketBootsCost { get; set; } = Item.buyPrice( 0, 35, 0, 0 );


		////

		[Range( 0f, 1f )]
		[DefaultValue( 0.5f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BossMaxLifePercentOnSpawn { get; set; } = 0.5f;



		////////////////

		/*public override ModConfig Clone() {
			var clone = (AdventureModeConfig)base.Clone();
			return clone;
		}*/


		////////////////

		public override void OnLoaded() {
			this.OnLoadedTiles();
		}
	}
}
