using System;
using Terraria;
using Terraria.ModLoader;


namespace AdventureMode.Buffs {
	class NecrotisDebuff : ModBuff {
		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Impact Trauma" );
			this.Description.SetDefault( "You've been hit hard by something" );

			Main.debuff[this.Type] = true;
			Main.pvpBuff[this.Type] = true;
		}

		public override void ModifyBuffTip( ref string tip, ref int rare ) {
			base.ModifyBuffTip( ref tip, ref rare );
		}
	}
}
