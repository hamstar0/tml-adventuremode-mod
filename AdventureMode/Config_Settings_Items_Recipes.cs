﻿using System;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;


namespace AdventureMode {
	public partial class AMConfig : ModConfig {
		[DefaultValue( true )]
		[ReloadRequired]
		public bool OverrideRecipeTileRequirements { get; set; } = true;

		////

		[DefaultValue( false )]
		[ReloadRequired]
		public bool EnableAlchemyRecipes { get; set; } = false;

		[DefaultValue( true )]
		[ReloadRequired]
		public bool EnableBossItemRecipes { get; set; } = true;

		[DefaultValue( true )]
		[ReloadRequired]
		public bool EnableTorchRecipes { get; set; } = true;

		[DefaultValue( false )]
		[ReloadRequired]
		public bool EnableBasicGrappleItemRecipes { get; set; } = false;

		////

		[DefaultValue(true)]
		public bool AddRodOfDiscordRecipe { get; set; } = true;
	}
}