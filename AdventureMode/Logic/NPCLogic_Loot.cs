using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Items;
using TheTrickster.NPCs;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static void ApplyLoot( NPC npc ) {
			if( npc.modNPC != null && npc.modNPC.GetType() == typeof(TricksterNPC) ) {
				NPCLogic.ApplyLootForTrickster( npc );
			}
		}

		private static void ApplyLootForTrickster( NPC npc ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}

			var tricksterNpc = npc.modNPC as TricksterNPC;
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
