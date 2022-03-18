using System;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;


namespace AdventureMode {
	public partial class AMConfig : ModConfig {
		[Range( 0f, 1f )]
		[DefaultValue( 0.07f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PotGemPercentChance { get; set; } = 0.07f;

		[Range( 0f, 1f )]
		[DefaultValue( 0.02f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PotSurprisePercentChance { get; set; } = 0.02f;

		[Range( 0f, 1f )]
		[DefaultValue( 0.002f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PotWraithSpawnPercentChance { get; set; } = 0.002f;


		////

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

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int TricksterPinkOrbDrops { get; set; } = 1;
	}
}
