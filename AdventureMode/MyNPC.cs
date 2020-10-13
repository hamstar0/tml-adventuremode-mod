using System;
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
	}
}
