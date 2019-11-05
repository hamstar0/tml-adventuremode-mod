using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode {
	class AdventureModeItem : GlobalItem {
		public override bool CanUseItem( Item item, Player player ) {
			switch( item.type ) {
			case ItemID.ManaCrystal:
				Main.NewText( "yum mana" );
				break;
			case ItemID.LifeCrystal:
				Main.NewText( "yum life" );
				break;
			}
			return true;
		}

		public override bool ConsumeItem( Item item, Player player ) {
			switch( item.type ) {
			case ItemID.ManaCrystal:
				Main.NewText( "yummy mana" );
				break;
			case ItemID.LifeCrystal:
				Main.NewText( "yummy life" );
				break;
			}
			return true;
		}
	}
}
