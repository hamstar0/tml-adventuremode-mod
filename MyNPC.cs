using HamstarHelpers.Services.AnimatedColor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace AdventureMode {
	partial class AdventureModeNPC : GlobalNPC {
		public static void FilterShop( IList<Item> shop, IList<ItemDefinition> whitelist, ref int nextSlot ) {
			foreach( Item item in shop ) {
				if( whitelist.Any( itemDef => itemDef.Type == item.type ) ) {
					continue;
				}

				shop.Remove( item );
				nextSlot--;
			}
		}



		////////////////

		public override bool CloneNewInstances => false;
		public override bool InstancePerEntity => true;



		////////////////

		public override void SetupShop( int type, Chest shop, ref int nextSlot ) {
			var npcDef = new NPCDefinition( type );

			if( AdventureModeConfig.Instance.ShopWhitelists.ContainsKey(npcDef) ) {
				var shopList = new List<Item>( shop.item );

				AdventureModeNPC.FilterShop( shopList, AdventureModeConfig.Instance.ShopWhitelists[npcDef], ref nextSlot );
				shop.item = shopList.ToArray();
			}
		}


		////////////////

		public override void PostDraw( NPC npc, SpriteBatch sb, Color drawColor ) {
			if( !npc.townNPC || !AdventureModeNPC.NPCDialogs.ContainsKey(npc.type) ) {
				return;
			}

			var myworld = ModContent.GetInstance<AdventureModeWorld>();
			if( myworld == null ) {
				return;
			}

			if( myworld.IntroducedNpcUniqueKeys.Contains(NPCID.GetUniqueKey(npc.type)) ) {
				return;
			}

			Vector2 scrPos = npc.Center - Main.screenPosition;
			scrPos.X -= 4;
			scrPos.Y -= (npc.height / 2) + 56;

			sb.DrawString(
				spriteFont: Main.fontMouseText,
				text: "!",
				position: scrPos,
				color: AnimatedColors.Alert.CurrentColor,
				rotation: 0f,
				origin: default(Vector2),
				scale: 2f,
				effects: SpriteEffects.None,
				layerDepth: 1f
			);
		}
	}
}
