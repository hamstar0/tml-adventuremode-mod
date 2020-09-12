using System;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.Debug;
using TheTrickster;
using LockedAbilities.Items.Consumable;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadTricksterAndLockedAbilies() {
			var config = TheTricksterConfig.Instance;
			config.SetOverride(
				nameof( TheTricksterConfig.DropsOnDefeat ),
				new ItemDefinition( ModContent.ItemType<DarkHeartPieceItem>() )
			);
		}
	}
}
