using System;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;


namespace AdventureMode {
	public partial class AMConfig : ModConfig {
		[DefaultValue( true )]
		[ReloadRequired]
		public bool RespawnBlockedDuringBosses { get; set; } = true;


		////

		[Range( 0f, 1f )]
		[DefaultValue( 0.5f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BossMaxLifePercentOnSpawn { get; set; } = 0.5f;


		////

		[DefaultValue( true )]
		public bool InvincibleTownNPCs { get; set; } = true;
	}
}
