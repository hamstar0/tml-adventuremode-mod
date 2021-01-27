using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace AdventureMode {
	public partial class AMConfig : ModConfig {
		[DefaultValue( true )]
		[ReloadRequired]
		public bool SetDefaultSpawnToBeach { get; set; } = true;

		///

		[DefaultValue( true )]
		public bool WorldGenRemoveMagicMirrors { get; set; } = true;

		[DefaultValue( true )]
		public bool WorldGenRemoveDarkHeartPieces { get; set; } = true;

		[DefaultValue( 0.1f )]
		[CustomModConfigItem( typeof(MyFloatInputElement) )]
		public float WorldGenAddedMountedMagicMirrorChance { get; set; } = 0.1f;
	}
}
