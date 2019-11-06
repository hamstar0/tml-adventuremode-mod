using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.EntityGroups;
using HamstarHelpers.Services.EntityGroups.Definitions;
using Nihilism;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace AdventureMode {
	partial class AdventureModeMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-adventuremode-mod";


		////////////////

		public static AdventureModeMod Instance { get; private set; }

		public static AdventureModeConfig Config => ModContent.GetInstance<AdventureModeConfig>();



		////////////////

		public AdventureModeMod() {
			AdventureModeMod.Instance = this;
		}

		////////////////

		public override void Load() {
			EntityGroups.Enable();
			NihilismAPI.InstancedFiltersOn();

			NihilismAPI.OnSyncOrWorldLoad( ( isSync ) => {
				if( isSync ) { return; }
				this.ApplyNihilismFilters();
			}, 0f );
		}

		public override void Unload() {
			AdventureModeMod.Instance = null;
		}


		////////////////

		public override void PostSetupContent() {
			/*CustomHotkeys.BindActionToKey1( "Illuminate", () => {
				var manaTileSingleton = ModContent.GetInstance<ManaCrystalShardTile>();
				foreach( (int tileX, IDictionary<int, float> tileYs) in manaTileSingleton.IlluminatedCrystals.ToArray() ) {
					foreach( (int tileY, float illum) in tileYs.ToArray() ) {
						manaTileSingleton.IlluminatedCrystals[tileX][tileY] = 1f;
					}
				}
				Main.NewText("Lit!");
			} );*/
		}


		////////////////

		private void ApplyNihilismFilters() {
			NihilismAPI.ClearFiltersForCurrentWorld( true );

			NihilismAPI.SetRecipeBlacklistGroupEntry( "Any Item", true );
			NihilismAPI.SetRecipeWhitelistGroupEntry( ItemGroupIDs.AnyOreEquipment, true );
			NihilismAPI.SetRecipeWhitelistGroupEntry( ItemGroupIDs.AnyNonOreCraftedEquipment, true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.ManaCrystal), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.WoodPlatform), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.BoosterTrack), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.MechanicalWorm), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.MechanicalSkull), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.MechanicalEye), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.CelestialSigil), true );

			/*NihilismAPI.SetItemBlacklistGroupEntry( "Any Placeable", true );
			NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.WoodPlatform), true );
			NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.Rope), true );
			NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.SilkRope), true );
			NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.VineRope), true );
			NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.WebRope), true );
			NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.Chain), true );
			NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.MinecartTrack), true );

			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.TinkerersWorkshop), true );
			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.MythrilAnvil), true );
			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.OrichalcumAnvil), true );
			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.AdamantiteForge), true );
			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.TitaniumForge), true );
			NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.LunarCraftingStation), true );*/

			NihilismAPI.NihilateCurrentWorld( true );
		}
	}
}
