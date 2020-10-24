using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Logic;


namespace AdventureMode {
	partial class AdventureModeMod : Mod {
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int idx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Inventory" ) );
			if( idx != -1 ) {
				this.AddTilePlacementInterfaceLayer( layers, idx + 1 );
			}
		}


		private void AddTilePlacementInterfaceLayer( List<GameInterfaceLayer> layers, int layerIdx ) {
			bool placementUI() {
				InterfaceLogic.DrawCurrentTilePlacementOutline();
				return true;
			};

			var tradeLayer = new LegacyGameInterfaceLayer( "AdventureMode: Placement Indicators", placementUI, InterfaceScaleType.UI );
			layers.Insert( layerIdx, tradeLayer );
		}
	}
}
