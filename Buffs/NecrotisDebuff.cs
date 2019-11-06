using HamstarHelpers.Helpers.World;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Buffs {
	class NecrotisDebuff : ModBuff {
		public const int Duration = 60 * 60 * 5;    // 5 minutes



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
					player.buffTime[necrotisIdx] += 2;
					if( player.buffTime[necrotisIdx] >= NecrotisDebuff.Duration ) {
						player.DelBuff( necrotisType );
					}
				}
			}
		}

		public static void ApplyEffect( Player player, float percent, ref float runSpeed, ref float jumpHeight ) {
			float scaledPercent = Math.Min( 1f, percent * 1.5f );

			runSpeed *= scaledPercent;
			jumpHeight *= scaledPercent;

			if( percent < 0.75f ) {
				player.AddBuff( BuffID.Ichor, 3 );
			}
			if( percent < 0.5f ) {
				player.AddBuff( BuffID.Bleeding, 3 );
			}
			if( percent < 0.1f ) {
				player.AddBuff( BuffID.Poisoned, 3 );
			}
			if( percent < 0.01f ) {
				player.AddBuff( BuffID.CursedInferno, 3 );
			}
		}



		////////////////

		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Necrotis" );
			this.Description.SetDefault( "You feel your life force draining..." + "\n" + "Gets worse the longer it lasts" );

			Main.debuff[this.Type] = true;
			//Main.buffNoTimeDisplay[this.Type] = true;
			Main.buffNoSave[this.Type] = true;
		}

		public override void ModifyBuffTip( ref string tip, ref int rare ) {
			tip = "This is no place for the living";
			rare = 2;
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
