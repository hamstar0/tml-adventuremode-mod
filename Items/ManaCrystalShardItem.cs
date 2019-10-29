﻿using AdventureMode.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Items {
	internal class ManaCrystalShardItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Mana Crystal Shard" );
			this.Tooltip.SetDefault( "Alights when magic." );
		}

		public override void SetDefaults() {
			//this.item.maxStack = 99;
			//this.item.width = 16;
			//this.item.height = 16;
			this.item.CloneDefaults( ItemID.CrystalShard );
			this.item.createTile = ModContent.TileType<ManaCrystalShardTile>();
			this.item.value = Item.buyPrice( 0, 1, 0, 0 );
			this.item.rare = 2;
		}
	}
}
