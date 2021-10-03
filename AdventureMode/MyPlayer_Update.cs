using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using AdventureMode.Logic;
using AdventureMode.Items;


namespace AdventureMode {
	partial class AMPlayer : ModPlayer {
		public override void PreUpdate() {
//int guide = NPC.FindFirstNPC( NPCID.Guide );
//if( guide != -1 ) {
//	DebugLibraries.Print( "guide", Main.npc[guide].position.ToString() );
//}
			Vector2? resurfPos = ResurfacePotionItem.GetResurfacePointIf( this.player );
			if( resurfPos.HasValue ) {
				this.ResurfacePoint = resurfPos.Value;
			}
		}

		public override void PreUpdateBuffs() {
			PlayerLogic.UpdateBuffs( this.player );
		}


		////////////////

		public override void UpdateDead() {
			PlayerLogic.UpdateDeadDuringBoss( this );
		}


		////////////////
		
		public override bool PreItemCheck() {
			Item heldItem = this.player.HeldItem;
			if( heldItem?.IsAir != true ) {
				switch( heldItem.type ) {
				case ItemID.RodofDiscord:
					return PlayerLogic.UpdateRodOfDiscordUse( this );
				default:
					if( heldItem.type == ModContent.ItemType<ResurfacePotionItem>() ) {
						ResurfacePotionItem.CheckItemForPlayer( this.player, heldItem );
					}
					break;
				}
			}

			return true;
		}
	}
}
