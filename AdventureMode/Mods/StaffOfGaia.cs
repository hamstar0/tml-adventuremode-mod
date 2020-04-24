using System;
using HamstarHelpers.Helpers.Debug;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Nihilism;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadStaffOfGaia() {
			StaffOfGaia.StaffOfGaiaConfig.Instance.OverlayChanges( new StaffOfGaia.StaffOfGaiaConfig {
				StaffOfGaiaSoldByDryad = false,
				PlayerStartStaves = 0
			} );
		}


		public void LoadStaffOfGaiaForNihilism() {
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ModContent.ItemType<StaffOfGaia.Items.StaffOfGaiaItem>() ), true );
		}
	}
}
