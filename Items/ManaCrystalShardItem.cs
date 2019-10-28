using AdventureMode.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Items {
	internal class ManaCrystalShardItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault("Mana Crystal Shard");
		}

		public override void SetDefaults() {
			this.item.CloneDefaults( ItemID.CrystalShard );
			this.item.createTile = ModContent.TileType<ManaCrystalShardTile>();
		}
	}
}
