﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Items;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static void ApplyLoot( NPC npc ) {
			if( npc.modNPC != null ) {
				if( ModLoader.GetMod("TheTrickster") != null ) {
					NPCLogic.ApplyLoot_WeakRef_Trickster( npc );
				}
			}
		}

		private static void ApplyLoot_WeakRef_Trickster( NPC npc ) {
			if( npc.modNPC.GetType() != typeof(TheTrickster.NPCs.TricksterNPC) ) {
				return;
			}

			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}

			var tricksterNpc = npc.modNPC as TheTrickster.NPCs.TricksterNPC;
			if( tricksterNpc == null ) {
				return;
			}

			// If the trickster has made an attack, no special reward is given
			if( tricksterNpc.HasAttacked ) {
				return;
			}

			var config = AMConfig.Instance;
			int stack = config.TricksterPinkOrbDrops;

			if( stack > 0 ) {
				int itemWho = Item.NewItem( npc.getRect(), ModContent.ItemType<PinkOrbItem>(), stack );
				NetMessage.SendData( MessageID.SyncItem, -1, -1, null, itemWho );
			}
		}
	}
}
