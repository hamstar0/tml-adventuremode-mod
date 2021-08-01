using System;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadSoulBarriers() {
			//var config = SoulBarriersConfig.Instance;
			var rf = new RecipeFinder();
			rf.SetResult( ModContent.ItemType<SoulBarriers.Items.PBGItem>() );

			var re = new RecipeEditor( rf.FindExactRecipe() );
			re.DeleteIngredient( ItemID.Nanites );
			re.AddIngredient( ModContent.ItemType<RuinedItems.Items.MagitechScrapItem>(), 2 );
		}
	}
}
