using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsCore.Services.Timers;
using AdventureMode.Logic;


namespace AdventureMode {
	partial class AMNPC : GlobalNPC {
		public override bool CloneNewInstances => false;
		public override bool InstancePerEntity => true;



		////////////////

		public override bool Autoload( ref string name ) {
			this.InitializeSpawnHooks();
			return base.Autoload( ref name );
		}


		////////////////

		public override void SetDefaults( NPC npc ) {
			if( npc.boss ) {
				NPCLogic.SetBossDefaults( npc );
			}
			if( Main.invasionSize > 0 && !npc.friendly ) {
				Timers.RunNow( () => NPCLogic.PostSetDefaultsForInvasion(npc) );
			}

			if( npc.townNPC && AMConfig.Instance.InvincibleTownNPCs ) {
				npc.dontTakeDamage = true;
			}

			/*////DEBUG////
			if( npc.type == NPCID.KingSlime ) {
				List<string> ctx = DebugLibraries.GetContextSlice().ToList();
				bool isEtc = ctx.Count > 7;

				LogLibraries.Log(
					"KING SLIME SPAWN DETECTED - "
					+ ctx.GetRange( 0, (isEtc ? 7 : ctx.Count) )
						.ToStringJoined("\n ")
					+ (isEtc ? "\n ..." : "")
				);
			}*/
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
			case NPCID.GoblinTinkerer:
				NPCLogic.SetupGoblinTinkererShop( shop, ref nextSlot );
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


		////////////////

		public override bool PreAI( NPC npc ) {
			// Hopefully this won't become a source of bugs
			if( npc.netID == NPCID.Guide ) {
				npc.dontTakeDamage = true;
				npc.immortal = true;
				npc.life = npc.lifeMax;
			}

			return base.PreAI( npc );
		}
	}
}
