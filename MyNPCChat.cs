using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Services.NPCChat;
using HamstarHelpers.Helpers.TModLoader;


namespace AdventureMode {
	partial class AdventureModeNpcChat : ILoadable {
		public readonly IDictionary<int, (string[] Greetings, string[] Added, string[] Blocked)> NPCDialogs
			= new Dictionary<int, (string[] Greetings, string[] Added, string[] Blocked)> {
				{
					NPCID.Guide, (
						Greetings: new string[] {
							"Looks like your adventure is just beginning. Please take stock of your surroundings and inventory. These may be important later. Talk with me if you need any further assistance.",
						},
						Added: new string[] {
							"Good thing the dungeon is sealed. I hear it's blighted with an undeath curse and filled with deadly fumes!",
							"The people who lived here once discovered ways to wield wearable artifacts of power, and hid their secrets around this land.",
							"Rare magic crystals can be found hidden underground. Somehow your binoculars are able to pick up their trail. I also hear they resonate with any magic energies that get near.",
							"You may find digging to be rather difficult. If you find yourself needing to squeeze into tight areas, a simple hammer of all things might be your best tool. Odd, huh?",
							"Not everything can be crafted. You'll have to learn to make do with what you can find or buy. Talk to me for more information, if in doubt.",
							"Need to add a few small patches or solid additions to a given area? You'll want to get your hands on some framing planks.",
							"Be sure to keep your eyes peeled for livable spaces to furnish. This is where our House Furnishing Kits will come in handy. They'll even provide you with new crafting stations and fast travel points!",
							"Wood is about the only non-ore material you can break freely. You'll need special framing planks to do any building, but they can be finicky. Best use house kits whenever possible.",
							"Need money? Sell your loot. Be sure to have me check your loot for available crafting recipes, first. Not everything works here the way you might assume.",
							//
							"You can use your axe to chop down trees. Just place your cursor over the tile and click!",
							"We'll need to create settlements to progress our journey. Use House Furnishing Kits to convert closed areas into livable spaces.",
							"Press Esc to access your crafting menu. When you have enough wood, find and stand near a workbench to craft things.",
							"Deploying House Furnishing Kits makes crafting stations, townsfolk and fast travel available.",
						},
						Blocked: new string[] {
							"You can use your pickaxe to dig through dirt",
							"If you want to survive",
							"When you have enough wood, create a workbench",
							"You can build a shelter by placing wood or other blocks in the world",
							"Once you have a wooden sword,",
							"To interact with backgrounds, use a hammer",
							"You can create a furnace out of torches, wood, and stone",
							"Anvils can be crafted out of iron",
							"they can be combined to create an item that will increase your magic capacity",
							"The ebonstone in the corruption can be purified",
							"You should make an attempt to max out your available life",
						}
					)
				},
				{
					NPCID.TravellingMerchant, (
						Greetings: new string[] {
							"Ever encounter that annoying Trickster fellow? I hear it likes to reward those who think they can outwit them extra quickly. If you ask me, I think it's up to something...",
							"Though the dryad gives warnings about using the Staff of Gaia, more practically I think some things of value might be lost from those it's used upon. Conversely, I hear it also gives back a portion of what it takes, in a different form.",
							"The dryad doesn't know what sort of power she carries. If only she wasn't so pressed to have to settle with mere capitalism. I don't know where she gets those orbs she's selling, but they can sure open up a world of possibilities to whomever knows how to use them.",
						},
						Added: new string[] {
							"Ever encounter that annoying Trickster fellow? I hear it likes to reward those who think they can outwit them extra quickly. If you ask me, I think it's up to something...",
							"Though the dryad gives warnings about using the Staff of Gaia, more practically I think some things of value might be lost from those it's used upon. Conversely, I hear it also gives back a portion of what it takes, in a different form.",
							"The dryad doesn't know what sort of power she carries. If only she wasn't so pressed to have to settle with mere capitalism. I don't know where she gets those orbs she's selling, but they can sure open up a world of possibilities to whomever knows how to use them.",
						},
						Blocked: new string[] {
						}
					)
				}
			};



		////////////////

		public void OnModsLoad() { }

		public void OnPostModsLoad() {
			foreach( (int npcType, var chats) in this.NPCDialogs ) {
				Func<string, string> greetingFunc = this.Greeting( npcType, chats.Greetings );
				if( greetingFunc != null ) {
					NPCChat.SetPriorityChat( npcType, greetingFunc );
				}

				foreach( string addedChat in chats.Added ) {
					NPCChat.AddChatForNPC( npcType, addedChat, 0.1f );
				}
				foreach( string blockedChat in chats.Blocked ) {
					NPCChat.AddChatRemoveFlatPatternForNPC( npcType, blockedChat );
				}
			}
		}

		public void OnModsUnload() { }


		////////////////

		private Func<string, string> Greeting( int npcType, string[] greetings ) {
			if( greetings.Length == 0 ) {
				return null;
			}

			int i = 0;

			return ( currentChat ) => {
				string npcKey = NPCID.GetUniqueKey( npcType );
				var myplayer = TmlHelpers.SafelyGetModPlayer<AdventureModePlayer>( Main.LocalPlayer );

				if( i >= greetings.Length ) {
					myplayer.IntroducedNpcUniqueKeys.Add( npcKey );
				}
				if( myplayer.IntroducedNpcUniqueKeys.Contains(npcKey) ) {
					return null;
				}

				string greeting = greetings[i];

				if( currentChat != null ) {
					i++;
				}

				return greeting;
			};
		}
	}
}
