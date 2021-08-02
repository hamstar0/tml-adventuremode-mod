using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode {
	partial class AMMod : Mod {
		public override void Load() {
			AMMod.Instance = this;

			void loadMod( string[] modNames, Action loader ) {
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

			loadMod(
				new string[] { "Grappletech" },
				this.ModInteractions.LoadGrappletech
			);
			loadMod(
				new string[] { "GreenHell" },
				this.ModInteractions.LoadGreenHell
			);
			loadMod(
				new string[] { "GreenHell" },
				this.ModInteractions.LoadPKEMeter
			);
			loadMod(
				new string[] { "Bullwhip" },
				this.ModInteractions.LoadBullwhip
			);
			loadMod(
				new string[] { "Nihilism" },
				this.ModInteractions.LoadNihilism
			);
			loadMod(
				new string[] { "Necrotis" },
				this.ModInteractions.LoadNecrotis
			);
			loadMod(
				new string[] { "CursedBones", "Necrotis", "Orbs" },
				this.ModInteractions.LoadCursedBones
			);
			loadMod(
				new string[] { "CursedBrambles" },
				this.ModInteractions.LoadCursedBrambles
			);
			loadMod(
				new string[] { "Ergophobia", "MountedMagicMirrors" },
				this.ModInteractions.LoadErgophobiaAndMountedMagicMirrors
			);
			loadMod(
				new string[] { "TheTrickster", "LockedAbilities" },
				this.ModInteractions.LoadTricksterAndLockedAbilies
			);
			loadMod(
				new string[] { "LockedAbilities" },
				this.ModInteractions.LoadLockedAbilities
			);
			loadMod(
				new string[] { "Orbs", "Ergophobia", "FindableManaCrystals", "CursedBrambles" },
				this.ModInteractions.LoadOrbs
			);
			loadMod(
				new string[] { "SoulBarriers", "RuinedItems" },
				this.ModInteractions.LoadSoulBarriers
			);
			loadMod(
				new string[] { "TerrainRemixer" },
				this.ModInteractions.LoadTerrainRemixer
			);
			loadMod(
				new string[] { "AdventureModeLore", "Objectives" },
				this.ModInteractions.LoadLoreAndObjectives
			);
			loadMod(
				new string[] { "TheMadRanger" },
				this.ModInteractions.LoadTheMadRanger
			);
			loadMod(
				new string[] { "PowerfulMagic" },
				this.ModInteractions.LoadPowerfulMagic
			);
			loadMod(
				new string[] { "RuinedItems" },
				this.ModInteractions.LoadRuinedItems
			);
		}

		////

		public override void Unload() {
			AMMod.Instance = null;
		}
	}
}
