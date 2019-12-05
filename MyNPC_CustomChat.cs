﻿using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Services.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;


namespace AdventureMode {
	partial class AdventureModeNPC : GlobalNPC {
		public readonly static IDictionary<int, (string[] Added, IDictionary<string, string> Blocked)> NPCDialogs
			= new Dictionary<int, (string[] Added, IDictionary<string, string> Blocked)> {
			{
				NPCID.Guide, (
					Added: new string[] {
						"Looks like your adventure is just beginning. Please take stock of your surroundings and inventory. These may be important later. Talk with me if you need any further assistance.",
						"Good thing the dungeon is sealed. I hear it's infested with an undeath curse and deadly fumes!",
						"The people who lived here once discovered ways to wield wearable artifacts of power, and hid their secrets under the ground of this land.",
						"Rare magic crystals can be found hidden deep underground. You need binoculars to pick up their trail. I hear they resonate strongly with any nearby magical energy.",
						"You may find digging to be rather difficult. If you find yourself needing to squeeze into tight areas, a simple hammer of all things might be your best tool. Odd, huh?",
					},
					Blocked: new Dictionary<string, string> {
						{
							"You can use your pickaxe to dig through dirt",
							"You can use your axe to chop down trees. Just place your cursor over the tile and click!"
						}, {
							"If you want to survive",
							"We'll need to create settlements to progress our journey. Use House Furnishing Kits to convert closed areas into livable spaces."
						}, {
							"When you have enough wood, create a workbench",
							"Press Esc to access your crafting menu. When you have enough wood, find and stand near a workbench to craft things."
						}, {
							"You can build a shelter by placing wood or other blocks in the world",
							null
						}, {
							"Once you have a wooden sword,",
							null
						}, {
							"To interact with backgrounds, use a hammer",
							null
						},
						{
							"You can create a furnace out of torches, wood, and stone",
							"Deploying House Furnishing Kits makes crafting stations, townsfolk and fast travel available."
						},
						{
							"Anvils can be crafted out of iron",
							null
						},
						{
							"they can be combined to create an item that will increase your magic capacity",
							null
						},
						{
							"The ebonstone in the corruption can be purified",
							null
						},
						{
							"You should make an attempt to max out your available life",
							null
						}
					}
				)
			}
		};



		////////////////

		public override void OnChatButtonClicked( NPC npc, bool firstButton ) {
			if( firstButton && npc.type == NPCID.Guide ) {
				Timers.SetTimer( "AdventureModeGuideHelpButton", 1, true, () => {
					this.GetChat( npc, ref Main.npcChatText );
					return false;
				} );
			}
		}


		public override void GetChat( NPC npc, ref string chat ) {
			if( !AdventureModeNPC.NPCDialogs.ContainsKey( npc.type ) ) {
				return;
			}

			var myplayer = TmlHelpers.SafelyGetModPlayer<AdventureModePlayer>( Main.LocalPlayer );
			UnifiedRandom rand = TmlHelpers.SafelyGetRand();
			string[] addedDialogs = AdventureModeNPC.NPCDialogs[ npc.type ].Added;
			IDictionary<string, string> blockedDialogs = AdventureModeNPC.NPCDialogs[ npc.type ].Blocked;

			string uniqueKey = NPCID.GetUniqueKey( npc.type );
			if( !myplayer.IntroducedNpcUniqueKeys.Contains(uniqueKey) ) {
				myplayer.IntroducedNpcUniqueKeys.Add( uniqueKey );
				chat = addedDialogs[0];
				return;
			}

			string chatCopy = chat;
			if( blockedDialogs.Keys.Any( d => chatCopy.Contains(d) ) ) {
				var blockedEntryKV = blockedDialogs.First( kv=>chatCopy.Contains(kv.Key) );
				if( blockedEntryKV.Value != null ) {
					chat = blockedEntryKV.Value;
				} else {
					int chatNum = rand.Next( addedDialogs.Length - 1 );
					chat = addedDialogs[ chatNum + 1 ];
				}
			} else if( rand.Next(4) == 0 ) {
				int chatNum = rand.Next( addedDialogs.Length - 1 );
				chat = addedDialogs[ chatNum + 1 ];
			}
		}
	}
}
