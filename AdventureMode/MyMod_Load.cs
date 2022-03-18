using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode {
	partial class AMMod : Mod {
		public override void Load() {
			AMMod.Instance = this;

			//

			void LoadMod( string[] modNames, Action loader ) {
				foreach( string modName in modNames ) {
					if( ModLoader.GetMod(modName) == null ) {
						LogLibraries.Warn( "Error loading missing mod "+ modName );
						return;
					}
				}

				try {
					loader();
				} catch( Exception e ) {
					LogLibraries.Warn( "Error loading "+string.Join(", ", modNames)+" - "+e.ToString() );
				}
			}

			//

			LoadMod(
				new string[] { "Grappletech" },
				this.ModInteractions.LoadGrappletech
			);
			LoadMod(
				new string[] { "GreenHell" },
				this.ModInteractions.LoadGreenHell
			);
			LoadMod(
				new string[] { "PKEMeter" },
				this.ModInteractions.LoadPKEMeter
			);
			LoadMod(
				new string[] { "PotLuck" },
				this.ModInteractions.LoadPotLuck
			);
			LoadMod(
				new string[] { "BossReigns", "PKEMeter" },
				this.ModInteractions.LoadBossReignsAndPKEMeter
			);
			LoadMod(
				new string[] { "Bullwhip" },
				this.ModInteractions.LoadBullwhip
			);
			LoadMod(
				new string[] { "FindableManaCrystals" },
				this.ModInteractions.LoadFindableManaCrystals
			);
			LoadMod(
				new string[] { "MountedMagicMirrors" },
				this.ModInteractions.LoadMountedMagicMirrors
			);
			LoadMod(
				new string[] { "Nihilism" },
				this.ModInteractions.LoadNihilism
			);
			LoadMod(
				new string[] { "Necrotis" },
				this.ModInteractions.LoadNecrotis
			);
			LoadMod(
				new string[] { "CursedBones", "Necrotis", "Orbs" },
				this.ModInteractions.LoadCursedBones
			);
			LoadMod(
				new string[] { "CursedBrambles" },
				this.ModInteractions.LoadCursedBrambles
			);
			LoadMod(
				new string[] { "Ergophobia", "MountedMagicMirrors" },
				this.ModInteractions.LoadErgophobiaAndMountedMagicMirrors
			);
			LoadMod(
				new string[] { "Ergophobia", "QuickRope" },
				this.ModInteractions.LoadErgophobiaAndQuickRope
			);
			LoadMod(
				new string[] { "TheTrickster", "LockedAbilities" },
				this.ModInteractions.LoadTricksterAndLockedAbilies
			);
			LoadMod(
				new string[] { "LockedAbilities" },
				this.ModInteractions.LoadLockedAbilities
			);
			LoadMod(
				new string[] { "Orbs", "Ergophobia", "FindableManaCrystals", "CursedBrambles" },
				this.ModInteractions.LoadOrbs
			);
			LoadMod(
				new string[] { "TerrainRemixer" },
				this.ModInteractions.LoadTerrainRemixer
			);
			LoadMod(
				new string[] { "AdventureModeLore", "Objectives" },
				this.ModInteractions.LoadLoreAndObjectives
			);
			LoadMod(
				new string[] { "TheMadRanger" },
				this.ModInteractions.LoadTheMadRanger
			);
			LoadMod(
				new string[] { "PowerfulMagic" },
				this.ModInteractions.LoadPowerfulMagic
			);
			LoadMod(
				new string[] { "RuinedItems" },
				this.ModInteractions.LoadRuinedItems
			);
			LoadMod(
				new string[] { "SoulBarriers" },
				this.ModInteractions.LoadSoulBarriers
			);
			LoadMod(
				new string[] { "WorldGates", "SpiritWalking" },
				this.ModInteractions.LoadWorldGatesAndSpiritWalking
			);
		}

		////

		public override void Unload() {
			AMMod.Instance = null;
		}
	}
}
