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

			if( npc.townNPC && AMConfig.Instance.InvincibleTownNPCs ) {
				npc.dontTakeDamage = true;
			}

			////DEBUG////
			if( npc.type == NPCID.KingSlime ) {
				LogHelpers.Log( "KING SLIME SPAWN DETECTED - "+DebugHelpers.GetCurrentContext() );
			}
		}


		////////////////

		public override void EditSpawnPool( IDictionary<int, float> pool, NPCSpawnInfo spawnInfo ) {
			if( pool.ContainsKey(NPCID.BoundGoblin) ) {	//NPC.downedGoblins
				NPCLogic.EditSpawnPoolForBoundGoblin( pool, spawnInfo );
			}
			if( pool.ContainsKey(NPCID.BoundMechanic) ) {	//NPC.downedBoss3
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
			case NPCID.Mechanic:
				NPCLogic.SetupMechanicShop( shop, ref nextSlot );
				break;
			case NPCID.TravellingMerchant:
				NPCLogic.SetupTravellingMerchantShop( shop, ref nextSlot );
				break;
			}
		}


		////////////////

		public override bool PreNPCLoot( NPC npc ) {
			NPCLogic.ApplyForbiddenLoot();
			return true;
		}

		public override void NPCLoot( NPC npc ) {
			NPCLogic.ApplyLoot( npc );
		}
	}
}
