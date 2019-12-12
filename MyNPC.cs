using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Services.AnimatedColor;
using HouseKits.Items;


namespace AdventureMode {
	partial class AdventureModeNPC : GlobalNPC {
		public static Item[] FilterShop( Item[] shop, IList<ItemDefinition> whitelist, ref int nextSlot ) {
			Item[] newShop = new Item[ shop.Length ];

			Item item;
			int j = 0;
			for( int i=0; i<shop.Length; i++ ) {
				item = shop[i];
				if( item == null || item.IsAir ) {
					continue;
				}

				if( whitelist.Any(itemDef => itemDef.Type == item.type) ) {
					newShop[j++] = item;
				} else {
					nextSlot--;
				}
			}

			for( ; j<shop.Length; j++ ) {
				newShop[j] = new Item();
			}

			return newShop;
		}



		////////////////

		public override bool CloneNewInstances => false;
		public override bool InstancePerEntity => true;



		////////////////

		public override void SetupShop( int type, Chest shop, ref int nextSlot ) {
			var npcDef = new NPCDefinition( type );

			if( AdventureModeConfig.Instance.ShopWhitelists.ContainsKey(npcDef) ) {
				var shopList = new List<Item>( shop.item );

				Item[] newShop = AdventureModeNPC.FilterShop(
					shop.item,
					AdventureModeConfig.Instance.ShopWhitelists[npcDef],
					ref nextSlot
				);
				
				for( int i=0; i<newShop.Length; i++ ) {
					shop.item[i] = shopList[i];
				}
			}

			switch( type ) {
			case NPCID.Merchant:
				var frameKit = new Item();
				var furnKit = new Item();

				frameKit.SetDefaults( ModContent.ItemType<HouseFramingKitItem>() );
				furnKit.SetDefaults( ModContent.ItemType<HouseFurnishingKitItem>() );

				shop.item[nextSlot++] = frameKit;
				shop.item[nextSlot++] = furnKit;
				break;
			}
		}


		////////////////

		public override void PostDraw( NPC npc, SpriteBatch sb, Color drawColor ) {
			if( !npc.townNPC || !AdventureModeNPC.NPCDialogs.ContainsKey(npc.type) ) {
				return;
			}

			var myplayer = TmlHelpers.SafelyGetModPlayer<AdventureModePlayer>( Main.LocalPlayer );
			if( myplayer?.IntroducedNpcUniqueKeys.Contains(NPCID.GetUniqueKey(npc.type)) ?? false ) {
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
