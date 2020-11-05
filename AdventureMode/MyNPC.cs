using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;
using AdventureMode.Logic;


namespace AdventureMode {
	partial class AMNPC : GlobalNPC {
		public override bool CloneNewInstances => false;
		public override bool InstancePerEntity => true;



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
			if( /*NPC.downedGoblins &&*/ pool.ContainsKey(NPCID.BoundGoblin) ) {
				NPCLogic.EditSpawnPoolForBoundGoblin( pool, spawnInfo );
			}
			if( /*NPC.downedBoss3 &&*/ pool.ContainsKey(NPCID.BoundMechanic) ) {
				NPCLogic.EditSpawnPoolForBoundMechanic( pool, spawnInfo );
			}
			if( pool.ContainsKey(NPCID.VoodooDemon) ) {
				NPCLogic.EditSpawnPoolForVoodooDemon( pool, spawnInfo );
			}
		}


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

		public override bool PreNPCLoot( NPC npc ) {
			NPCLogic.ApplyForbiddenLoot();
			return true;
		}
	}
}
