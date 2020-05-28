using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode {
	partial class AdventureModeItem : GlobalItem {
		public override void OnConsumeItem( Item item, Player player ) {
			switch( item.type ) {
			/*case ItemID.ManaCrystal:
				if( AdventureModeConfig.Instance.ReducedManaCrystalStatIncrease ) {
					player.statManaMax -= 15;
				}
				this.ModifyPopupText();
				break;*/	//<- Implemented via FindableManaCrystals mod
			case ItemID.LifeCrystal:
				if( AdventureModeConfig.Instance.ReducedLifeCrystalStatIncrease ) {
					player.statLifeMax -= 10;
					this.ModifyPopupText();
				}
				break;
			}
		}
	}
}
