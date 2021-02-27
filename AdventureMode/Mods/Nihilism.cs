using System;
using Terraria.ID;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Services.EntityGroups;
using HamstarHelpers.Services.EntityGroups.Definitions;
using HamstarHelpers.Services.Hooks.LoadHooks;
using Nihilism;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadNihilism() {
			EntityGroups.Enable();

			NihilismAPI.InstancedFiltersOn();
			NihilismAPI.OnSyncOrWorldLoad( ( isSync ) => {
				if( isSync && LoadHelpers.IsWorldBeingPlayed() ) {
					return;
				}

				LoadHooks.AddPostWorldLoadOnceHook( () => {
					this.ApplyNihilismFilters();
				} );
			}, 0f );
		}


		////////////////

		private void ApplyNihilismFilters() {
			NihilismAPI.ClearFiltersForCurrentWorld( true );

			//

			this.ApplyNihilismNPCFilters();
			this.ApplyNihilismItemFilters();

			//

			NihilismAPI.NihilateCurrentWorld( true );
		}


		////////////////

		private void ApplyNihilismNPCFilters() {
			NihilismAPI.SetNpcBlacklistEntry( new NPCDefinition( NPCID.Angler ), true );
			NihilismAPI.SetNpcBlacklistEntry( new NPCDefinition( NPCID.SleepingAngler ), true );
			NihilismAPI.SetNpcBlacklistEntry( new NPCDefinition( NPCID.Painter ), true );
		}


		////////////////

		private void ApplyNihilismItemFilters() {
			NihilismAPI.SetItemBlacklistEntry( new ItemDefinition(ItemID.BlinkrootSeeds), true );
			NihilismAPI.SetItemBlacklistEntry( new ItemDefinition(ItemID.DaybloomSeeds), true );
			NihilismAPI.SetItemBlacklistEntry( new ItemDefinition(ItemID.DeathweedSeeds), true );
			NihilismAPI.SetItemBlacklistEntry( new ItemDefinition(ItemID.FireblossomSeeds), true );
			NihilismAPI.SetItemBlacklistEntry( new ItemDefinition(ItemID.MoonglowSeeds), true );
			NihilismAPI.SetItemBlacklistEntry( new ItemDefinition(ItemID.ShiverthornSeeds), true );
			NihilismAPI.SetItemBlacklistEntry( new ItemDefinition(ItemID.WaterleafSeeds), true );
			NihilismAPI.SetItemBlacklistGroupEntry( ItemGroupIDs.AnyFishingPole, true );
		}
	}
}
