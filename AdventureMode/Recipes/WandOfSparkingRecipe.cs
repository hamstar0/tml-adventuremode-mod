using System;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Recipes {
	class WandOfSparkingRecipe : ModRecipe {
		public WandOfSparkingRecipe() : base( AMMod.Instance ) {
			this.AddRecipeGroup( "AdventureMode.Gems", 10 );
			//this.AddIngredient( ItemID.LivingWoodWand, 1 );
			this.AddIngredient( ItemID.Torch, 25 );
			this.AddIngredient( ItemID.MarshmallowonaStick, 1 );

			this.AddTile( TileID.WorkBenches );

			this.SetResult( ItemID.WandofSparking, 1 );
		}
	}
}
