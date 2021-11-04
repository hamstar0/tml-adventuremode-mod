using System;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;


namespace AdventureMode {
	public partial class AMConfig : ModConfig {
		[ReloadRequired]
		public bool RespawnBlockedDuringBosses { get; set; } = false;


		////

		[Range( 0f, 1f )]
		[DefaultValue( 0.65f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BossMaxLifePercentOnSpawn { get; set; } = 0.65f;


		////

		[DefaultValue( true )]
		public bool InvincibleTownNPCs { get; set; } = true;
	}
}
