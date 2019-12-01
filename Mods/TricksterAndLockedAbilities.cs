using System;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Configs;
using TheTrickster;
using LockedAbilities.Items.Consumable;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadTricksterAndLockedAbilies() {
			ModConfigStack.SetStackedConfigChangesOnly( new TheTricksterConfig {
				DropsOnDefeat = new ItemDefinition( ModContent.ItemType<DarkHeartPieceItem>() )
			} );
		}
	}
}
