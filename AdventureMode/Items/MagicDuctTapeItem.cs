using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Items {
	class MagicDuctTapeItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Magic Duct Tape" );
			this.Tooltip.SetDefault( "Allows jury-rigging accessory item combinations into new types" );
		}

		public override void SetDefaults() {
			this.item.width = 20;
			this.item.height = 20;
			this.item.maxStack = 99;
			this.item.material = true;
			this.item.value = Item.buyPrice( 0, 0, 10, 0 );
			this.item.rare = ItemRarityID.Blue;
		}
	}
}
