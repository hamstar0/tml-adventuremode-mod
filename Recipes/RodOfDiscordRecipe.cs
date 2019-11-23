using System;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Recipes {
	class RodOfDiscordRecipe : ModRecipe {
		public RodOfDiscordRecipe( bool isAlt ) : base( AdventureModeMod.Instance ) {
			this.AddIngredient( ItemID.SoulofLight, 10 );
			this.AddIngredient( ItemID.SoulofNight, 10 );
			this.AddIngredient( ItemID.TeleportationPotion, 10 );
			if( !isAlt ) {
				this.AddIngredient( ItemID.DiamondStaff, 1 );
			} else {
				this.AddIngredient( ItemID.RubyStaff, 1 );
				this.AddIngredient( ItemID.Diamond, 4 );
			}
			this.AddTile( TileID.MythrilAnvil );
			this.SetResult( ItemID.RodofDiscord, 1 );
		}


		public override bool RecipeAvailable() {
			return AdventureModeConfig.Instance.AddRodOfDiscordRecipe;
		}
	}
}
