using HamstarHelpers.Services.Configs;
using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace AdventureMode {
	public partial class AdventureModeConfig : StackableModConfig {
		[DefaultValue( true )]
		//[ReloadRequired]
		public bool SetDefaultSpawnToBeach { get; set; } = true;

		///

		[DefaultValue( true )]
		public bool WorldGenRemoveMagicMirrors { get; set; } = true;

		[DefaultValue( true )]
		public bool WorldGenRemoveDarkHeartPieces { get; set; } = true;

		[CustomModConfigItem( typeof(MyFloatInputElement) )]
		public float WorldGenAddedMountedMagicMirrorChance { get; set; } = 0.05f;
	}
}
