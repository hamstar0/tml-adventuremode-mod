using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;


namespace AdventureMode.Logic {
	static partial class PlayerLogic {
		public static void SetupStartInventory( AMPlayer myplayer, IList<Item> items, bool mediumcoreDeath ) {
			void addItem( int itemType, int stack ) {
				var item = new Item();
				item.SetDefaults( itemType );
				item.stack = stack;
				items.Add( item );
			}

			if( !mediumcoreDeath ) {
				myplayer.IsAdventurer = true;

				addItem( ItemID.WoodenHammer, 1 );
				if( !AMConfig.Instance.EnableTorchRecipes ) {
					addItem( ItemID.Torch, 10 );
				}
				addItem( ItemID.RopeCoil, 20 );
			}
		}
	}
}
