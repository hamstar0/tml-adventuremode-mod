﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode {
	class AdventureModeItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			if( item.type != ItemID.WoodPlatform ) {
				return;
			}

			var tip = new TooltipLine( this.mod, "AdventureModePlatform", "Only placeable in short ledges attached to something solid" );
			tooltips.Add( tip );
		}


		////////////////

		public override void OnConsumeItem( Item item, Player player ) {
			switch( item.type ) {
			case ItemID.ManaCrystal:
				player.statManaMax -= 15;
				break;
			case ItemID.LifeCrystal:
				player.statLifeMax -= 15;
				break;
			}
		}
	}
}
