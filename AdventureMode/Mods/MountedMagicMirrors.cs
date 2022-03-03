using System;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadMountedMagicMirrors() {
			var config = MountedMagicMirrors.MMMConfig.Instance;

			config.SetOverride( nameof(config.GenerateMountedMirrorsForNewWorlds), false );
		}
	}
}
