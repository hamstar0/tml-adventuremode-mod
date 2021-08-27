using System;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;


namespace AdventureMode {
	public partial class AMConfig : ModConfig {
		[DefaultValue( true )]
		[ReloadRequired]
		public bool OverrideRecipeTileRequirements { get; set; } = true;


		////

		[DefaultValue( true )]
		public bool OreRefundRecipesEnabled { get; set; } = true;


		////

		[DefaultValue( false )]
		[ReloadRequired]
		public bool EnableAlchemyRecipes { get; set; } = false;

		[DefaultValue( true )]
		[ReloadRequired]
		public bool EnableBossItemRecipes { get; set; } = true;

		[DefaultValue( true )]
		[ReloadRequired]
		public bool EnableBiomedBossItemRecipes { get; set; } = true;

		[DefaultValue( true )]
		[ReloadRequired]
		public bool EnableTorchRecipes { get; set; } = true;

		[DefaultValue( true )]
		[ReloadRequired]
		public bool EnableBasicGrappleItemRecipes { get; set; } = true;

		//

		[DefaultValue( true )]
		[ReloadRequired]
		public bool SeismicChargeRecipeEnabled { get; set; } = true;

		////

		[DefaultValue(true)]
		public bool AddRodOfDiscordRecipe { get; set; } = true;

		////

		[DefaultValue(1)]
		public int StrangePlantsAddedPerBossSummonItemRecipe { get; set; } = 1;
	}
}
