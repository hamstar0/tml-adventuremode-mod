using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AdventureMode.Items;


namespace AdventureMode.Tiles {
	public class FramingPlankTile : ModTile {
		public override void SetDefaults() {
			Main.tileSolid[ this.Type ] = true;
			Main.tileMergeDirt[ this.Type ] = true;

			//this.dustType = this.DustType<Sparkle>();
			this.dustType = DustID.t_LivingWood;
			this.drop = ModContent.ItemType<FramingPlankItem>();
			this.AddMapEntry( new Color( 133, 94, 32 ) );
		}
	}
}
