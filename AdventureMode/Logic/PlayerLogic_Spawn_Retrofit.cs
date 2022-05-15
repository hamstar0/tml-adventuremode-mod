using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.Players;


namespace AdventureMode.Logic {
	static partial class PlayerLogic {
		public static bool RetrofitPlayerInventory_If( Player player ) {
			void AddItemToList( IList<Item> list, int itemType ) {
				Item item = new Item();
				item.SetDefaults( itemType, true );

				list.Add( item );
			}

			////
			
LogLibraries.Log("DEBUG OUT 3");
			int tmrType = ModContent.ItemType<TheMadRanger.Items.Weapons.TheMadRangerItem>();
			int bandolierType = ModContent.ItemType<TheMadRanger.Items.Accessories.BandolierItem>();
			int whipType = ModContent.ItemType<Bullwhip.Items.BullwhipItem>();
			int pbgType = ModContent.ItemType<SoulBarriers.Items.PBGItem>();

			int tmrIdx = Array.FindIndex( player.inventory, item => item.type == tmrType );
			int bandolierIdx = Array.FindIndex( player.inventory, item => item.type == bandolierType );
			int whipIdx = Array.FindIndex( player.inventory, item => item.type == whipType );
			int pbgIdx = Array.FindIndex( player.inventory, item => item.type == pbgType );
			int binocIdx = Array.FindIndex( player.inventory, item => item.type == ItemID.Binoculars );

			var slotExceptions = new HashSet<int>();
			if( tmrIdx != -1 ) { slotExceptions.Add( tmrIdx ); }
			if( bandolierIdx != -1 ) { slotExceptions.Add( bandolierIdx ); }
			if( whipIdx != -1 ) { slotExceptions.Add( whipIdx ); }
			if( pbgIdx != -1 ) { slotExceptions.Add( pbgIdx ); }
			if( binocIdx != -1 ) { slotExceptions.Add( binocIdx ); }

			//

			bool isVanillaFresh = PlayerLibraries.IsPlayerVanillaFresh(
				player: player,
				inventorySlotExceptions: slotExceptions
			);

			if( !isVanillaFresh ) {
				LogLibraries.Log( $"Could not 'retrofit' {player.name} for Adventure Mode." );

				return false;
			}

			//

			IList<Item> invList = player.inventory
				.Where( item => item?.active == true && item.type > 0 && item.stack > 0 )
				.ToList();

			//

			if( tmrIdx == -1 ) {
				AddItemToList( invList, tmrType );
			}
			if( bandolierIdx == -1 ) {
				AddItemToList( invList, bandolierType );
			}
			if( whipIdx == -1 ) {
				AddItemToList( invList, whipType );
			}
			if( pbgIdx == -1 ) {
				AddItemToList( invList, pbgType );
			}
			if( binocIdx == -1 ) {
				AddItemToList( invList, ItemID.Binoculars );
			}

			//

			PlayerLogic.SetupSpawnInventoryDataBeforeInWorld( invList );

			//

			int i = 0;
			foreach( Item item in invList ) {
				player.inventory[i++] = item;

				if( i >= player.inventory.Length ) {
					break;
				}
			}

			//

			PlayerLogic.SetupSpawnInventoryInWorld( player );

			//

			return true;
		}
	}
}
