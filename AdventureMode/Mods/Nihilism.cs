using System;
using Terraria.ID;
using Terraria.ModLoader.Config;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.TModLoader;
using ModLibsCore.Services.Hooks.LoadHooks;
using ModLibsEntityGroups.Services.EntityGroups.Definitions;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadNihilism() {
			Nihilism.NihilismAPI.InstancedFiltersOn();
			Nihilism.NihilismAPI.OnSyncOrWorldLoad( ( isSync ) => {
				if( isSync && LoadLibraries.IsWorldBeingPlayed() ) {
					return;
				}

				LoadHooks.AddPostWorldLoadOnceHook( () => {
					this.ApplyNihilismFilters();
				} );
			}, 0f );
		}


		////////////////

		private void ApplyNihilismFilters() {
			Nihilism.NihilismAPI.ClearFiltersForCurrentWorld( true );

			//

			this.ApplyNihilismNPCFilters();
			this.ApplyNihilismItemFilters();

			//

			Nihilism.NihilismAPI.NihilateCurrentWorld( true );
		}


		////////////////

		private void ApplyNihilismNPCFilters() {
			Nihilism.NihilismAPI.SetNpcBlacklistEntry( new NPCDefinition( NPCID.Angler ), true );
			Nihilism.NihilismAPI.SetNpcBlacklistEntry( new NPCDefinition( NPCID.SleepingAngler ), true );
			Nihilism.NihilismAPI.SetNpcBlacklistEntry( new NPCDefinition( NPCID.Painter ), true );
		}


		////////////////

		private void ApplyNihilismItemFilters() {
			Nihilism.NihilismAPI.SetItemBlacklistEntry( new ItemDefinition(ItemID.BlinkrootSeeds), true );
			Nihilism.NihilismAPI.SetItemBlacklistEntry( new ItemDefinition(ItemID.DaybloomSeeds), true );
			Nihilism.NihilismAPI.SetItemBlacklistEntry( new ItemDefinition(ItemID.DeathweedSeeds), true );
			Nihilism.NihilismAPI.SetItemBlacklistEntry( new ItemDefinition(ItemID.FireblossomSeeds), true );
			Nihilism.NihilismAPI.SetItemBlacklistEntry( new ItemDefinition(ItemID.MoonglowSeeds), true );
			Nihilism.NihilismAPI.SetItemBlacklistEntry( new ItemDefinition(ItemID.ShiverthornSeeds), true );
			Nihilism.NihilismAPI.SetItemBlacklistEntry( new ItemDefinition(ItemID.WaterleafSeeds), true );
			Nihilism.NihilismAPI.SetItemBlacklistGroupEntry( ItemGroupIDs.AnyFishingPole, true );
		}
	}
}
