using System;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Recipes {
	class BottleRecipe : ModRecipe {
		public BottleRecipe() : base( AMMod.Instance ) {
			this.AddRecipeGroup( "AdventureMode.Sand", 2 );

			this.SetResult( ItemID.Bottle, 1 );
		}
	}
}
