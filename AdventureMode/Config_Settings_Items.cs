using System;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;


namespace AdventureMode {
	public partial class AMConfig : ModConfig {
		[DefaultValue( true )]
		public bool GrappleOnlyWoodAndPlatforms { get; set; } = true;

		////

		[DefaultValue( true )]
		public bool ReducedLifeCrystalStatIncrease { get; set; } = true;


		[Range( 0, 60 * 60 * 30 )]
		[DefaultValue( 60 * 60 * 2 )]
		public int MaximumDangersenseBuffDuration { get; set; } = 60 * 60 * 2;

		////

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

		[DefaultValue( true )]
		public bool NerfReaverShark { get; set; } = true;
	}
}
