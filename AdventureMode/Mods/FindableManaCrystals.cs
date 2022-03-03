using System;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadFindableManaCrystals() {
			var config = FindableManaCrystals.FMCConfig.Instance;

			config.SetOverride( nameof(config.IsGeothaumSurveyStationBreakable), false );
			config.SetOverride( nameof(config.EnableGeothaumSurveyStationRecipe), false );
			config.SetOverride( nameof(config.GeothaumSurveyStationDropsItem), false );
		}
	}
}
