using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Items;
using HamstarHelpers.Helpers.Players;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode {
	class AdventureModeProjectile : GlobalProjectile {
		public override bool? CanUseGrapple( int projType, Player player ) {
			if( AdventureModeConfig.Instance.GrappleChainAmmoRate <= 0 ) {
				return null;
			}

			//int ammo = ItemFinderHelpers.CountTotalOfEach( player.inventory, new HashSet<int> { ItemID.Chain } );
			int idx = ItemFinderHelpers.FindIndexOfFirstOfItemInCollection( player.inventory, new HashSet<int> { ItemID.Chain } );
			if( idx == -1 ) { return false; }

			return null;
		}


		public override void UseGrapple( Player player, ref int type ) {
			if( AdventureModeConfig.Instance.GrappleChainAmmoRate <= 0 ) {
				return;
			}

			int idx = ItemFinderHelpers.FindIndexOfFirstOfItemInCollection( player.inventory, new HashSet<int> { ItemID.Chain } );
			if( idx == -1 ) {
				if( AdventureModeConfig.Instance.DebugModeInfo ) {
					LogHelpers.LogAndPrintOnce( "No chains available for grappling.", Color.Red );
				}
				return;
			}

			PlayerItemHelpers.RemoveInventoryItemQuantity( player, ItemID.Chain, AdventureModeConfig.Instance.GrappleChainAmmoRate );
		}
	}
}
