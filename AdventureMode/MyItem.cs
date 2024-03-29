﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Items.Attributes;
using Messages;
using AdventureMode.Logic;


namespace AdventureMode {
	partial class AMItem : GlobalItem {
		public override bool OnPickup( Item item, Player player ) {
			if( ItemAttributeLibraries.IsGrapple(item) ) {
				string id = "AdventureModeGrappleChanges";

				MessagesAPI.AddMessage(
					title: "Grappling hook changes",
					description: "New to Adventure Mode: Grappling hooks must now be used on only wood objects.",
					modOfOrigin: AMMod.Instance,
					alertPlayer: MessagesAPI.IsUnread( id ),
					isImportant: false,
					id: id,
					parentMessage: MessagesAPI.ModInfoCategoryMsg
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


		////////////////

		public override void PostUpdate( Item item ) {
			var config = AMConfig.Instance;

			if( config.ForceRemoveHerbSeeds ) {
				switch( item.type ) {
				case ItemID.DaybloomSeeds:
				case ItemID.BlinkrootSeeds:
				case ItemID.DeathweedSeeds:
				case ItemID.FireblossomSeeds:
				case ItemID.MoonglowSeeds:
				case ItemID.ShiverthornSeeds:
				case ItemID.WaterleafSeeds:
					item.active = false;
					break;
				}
			}
		}

		public override void UpdateInventory( Item item, Player player ) {
			var config = AMConfig.Instance;

			if( config.ForceRemoveHerbSeeds ) {
				switch( item.type ) {
				case ItemID.DaybloomSeeds:
				case ItemID.BlinkrootSeeds:
				case ItemID.DeathweedSeeds:
				case ItemID.FireblossomSeeds:
				case ItemID.MoonglowSeeds:
				case ItemID.ShiverthornSeeds:
				case ItemID.WaterleafSeeds:
					item.active = false;
					break;
				}
			}
		}
	}
}
