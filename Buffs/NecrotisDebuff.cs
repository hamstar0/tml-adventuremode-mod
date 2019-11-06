using HamstarHelpers.Helpers.World;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Buffs {
	class NecrotisDebuff : ModBuff {
		public const int Duration = 60 * 60 * 15;    // 15 minutes



		////////////////

		public static void UpdateForPlayer( Player player ) {
			int necrotisType = ModContent.BuffType<NecrotisDebuff>();
			int necrotisIdx = player.FindBuffIndex( necrotisType );

			if( player.ZoneDungeon && WorldHelpers.UnderworldLayerTopTileY < player.position.Y ) {
				if( necrotisIdx == -1 ) {
					player.AddBuff( necrotisType, NecrotisDebuff.Duration );
				}
			} else {
				if( necrotisIdx != -1 ) {
					player.buffTime[necrotisIdx] += 4;
					if( player.buffTime[necrotisIdx] >= NecrotisDebuff.Duration ) {
						player.DelBuff( necrotisType );
					}
				}
			}
		}

		////

		public static void ApplyEffect( Player player, float percent ) {
			float scaledPercent = Math.Min( 1f, percent + 0.25f );

			if( percent < 0.75f ) {
				player.AddBuff( BuffID.Ichor, 3 );
			}
			if( percent < 0.5f ) {
				player.AddBuff( BuffID.Bleeding, 3 );
			}
			if( percent < 0.2f ) {
				player.AddBuff( BuffID.Poisoned, 3 );
			}
			if( percent < 0.01f ) {
				player.AddBuff( BuffID.CursedInferno, 3 );
			}

			player.maxRunSpeed *= scaledPercent;
			player.accRunSpeed = player.maxRunSpeed;
			player.moveSpeed *= scaledPercent;

			int maxJump = (int)( Player.jumpHeight * scaledPercent );
			if( player.jump > maxJump ) {
				player.jump = maxJump;
			}
		}



		////////////////

		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Necrotis" );
			this.Description.SetDefault( "You feel your life force draining"
				+ "\n" + "Gets worse the longer it lasts" );

			Main.debuff[this.Type] = true;
			//Main.buffNoTimeDisplay[this.Type] = true;
			//Main.buffNoSave[this.Type] = true;
		}


		////////////////

		public override void Update( Player player, ref int buffIndex ) {
			var myplayer = player.GetModPlayer<AdventureModePlayer>();
			myplayer.NecrotisPercent = (float)player.buffTime[buffIndex] / (float)NecrotisDebuff.Duration;

			if( player.buffTime[buffIndex] > 0 && player.buffTime[buffIndex] < 60 ) {
				player.buffTime[buffIndex] = 60;
			}
		}
	}
}
