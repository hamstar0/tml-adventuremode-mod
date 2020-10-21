﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Logic;
using HamstarHelpers.Services.Timers;


namespace AdventureMode {
	partial class AdventureModeNPC : GlobalNPC {
		public override bool CloneNewInstances => false;
		public override bool InstancePerEntity => true;



		////////////////

		public override void SetupShop( int type, Chest shop, ref int nextSlot ) {
			NPCLogic.FilterShops( type, shop.item, ref nextSlot );

			switch( type ) {
			case NPCID.Merchant:
				NPCLogic.SetupMerchantShop( shop, ref nextSlot );
				break;
			}
		}


		////////////////

		public override void SetDefaults( NPC npc ) {
			if( npc.boss ) {
				NPCLogic.SetBossDefaults( npc );
			}
			if( Main.invasionSize > 0 && !npc.friendly ) {
				Timers.RunNow( () => NPCLogic.SetInvasionDefaults(npc) );
			}
		}


		////////////////

		public override void EditSpawnPool( IDictionary<int, float> pool, NPCSpawnInfo spawnInfo ) {
			if( NPC.downedGoblins && pool.ContainsKey(NPCID.BoundGoblin) ) {
				NPCLogic.EditSpawnPoolForBoundGoblin( pool, spawnInfo );
			}
		}


		////////////////

		public override bool PreNPCLoot( NPC npc ) {
			NPCLogic.ApplyForbiddenLoot();
			return true;
		}
	}
}
