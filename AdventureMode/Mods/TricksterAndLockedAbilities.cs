using System;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadTricksterAndLockedAbilies() {
			var config = TheTrickster.TheTricksterConfig.Instance;
			
			config.SetOverride(
				nameof(config.DropsOnDefeat),
				new ItemDefinition( ModContent.ItemType<LockedAbilities.Items.Consumable.DarkHeartPieceItem>() )
			);
		}
	}
}
