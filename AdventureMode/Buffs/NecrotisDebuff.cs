using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Buffs {
	class NecrotisDebuff : ModBuff {
		public static bool IsBeingAfflicted( Player player ) {
			return player.ZoneDungeon && player.position.Y > ( WorldHelpers.RockLayerTopTileY << 4 );
		}

		public static float GetPercentAfflicted( Player player ) {
			int buffIdx = player.FindBuffIndex( ModContent.BuffType<NecrotisDebuff>() );
			if( buffIdx == -1 ) {
				return 0f;
			}

			int currDuration = player.buffTime[ buffIdx ];
			return (float)currDuration / 3600f;
		}


		////////////////

		public static void UpdateBuffDurationForPlayer( Player player ) {
			if( AdventureModeConfig.Instance.NecrotisAfflictTickRate < 0 ) {
				return;
			}

			var myplayer = TmlHelpers.SafelyGetModPlayer<AdventureModePlayer>( player );
			int necrotisType = ModContent.BuffType<NecrotisDebuff>();
			int necrotisIdx = player.FindBuffIndex( necrotisType );

			if( NecrotisDebuff.IsBeingAfflicted(player) ) {
				myplayer.NecrotisAmount += AdventureModeConfig.Instance.NecrotisAfflictTickRate;
				player.AddBuff( necrotisType, (int)myplayer.NecrotisAmount );
			} else if( necrotisIdx != -1 ) {
				myplayer.NecrotisAmount = (float)player.buffTime[ necrotisIdx ];
			} else {
				myplayer.NecrotisAmount = 0f;
			}
		}

		////

		public static void ApplyEffect( Player player ) {
			float percent = NecrotisDebuff.GetPercentAfflicted( player );
			float invPercent = 1f - percent;
			float scaledInvPercent = MathHelper.Clamp( invPercent * 1.35f, 0f, 1f );

			//if( percent < 0.75f ) {
			//	player.AddBuff( BuffID.Ichor, 3 );
			//}
			if( percent >= 0.5f ) {
				player.AddBuff( BuffID.Bleeding, 3 );
			}
			if( percent >= 0.8f ) {
				player.AddBuff( BuffID.Poisoned, 3 );
			}
			if( percent >= 0.99f ) {
				player.AddBuff( BuffID.CursedInferno, 3 );
			}

			player.maxRunSpeed *= scaledInvPercent;
			player.accRunSpeed = player.maxRunSpeed;
			player.moveSpeed *= scaledInvPercent;

			int maxJump = (int)( Player.jumpHeight * scaledInvPercent );
			if( player.jump > maxJump ) {
				player.jump = maxJump;
			}
		}



		////////////////

		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Necrotis" );
			this.Description.SetDefault(
				"You feel your life energy draining"
				+ "\n" + "Worsens as it increases"
				+ "\n" + "(Caps out at 1m)"
			);

			Main.debuff[this.Type] = true;
			//Main.buffNoTimeDisplay[this.Type] = true;
			//Main.buffNoSave[this.Type] = true;
		}
	}
}
