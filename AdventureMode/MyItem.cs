﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Items.Attributes;
using ModLibsInterMod.Libraries.Mods.APIMirrors.ModHelpersAPIMirrors;
using AdventureMode.Logic;


namespace AdventureMode {
	partial class AMItem : GlobalItem {
		public override bool OnPickup( Item item, Player player ) {
			if( ItemAttributeLibraries.IsGrapple(item) ) {
				InboxAPIMirrorsLibraries.SetMessage(
					"AdventureModeGrappleChanges",
					"New to Adventure Mode: Grappling hooks must now be used on only wood objects.",
					false
				);
			}
			return base.OnPickup( item, player );
		}


		////////////////

		public override void OnConsumeItem( Item item, Player player ) {
			switch( item.type ) {
			//case ItemID.ManaCrystal:
			//	if( AdventureModeConfig.Instance.ReducedManaCrystalStatIncrease ) {
			//		player.statManaMax -= 15;
			//	}
			//	this.ModifyPopupText();
			//	break;	//<- Implemented via FindableManaCrystals mod
			case ItemID.LifeCrystal:
				ItemLogic.OnLifeCrystalConsume( player );
				break;
			}
		}
	}
}
