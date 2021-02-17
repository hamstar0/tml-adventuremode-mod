using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AdventureMode.Projectiles;


namespace AdventureMode.Items {
	class SeismicChargeItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Seismic Charge" );
			this.Tooltip.SetDefault( "Tossed as a bomb, exploding after a few seconds"
				+"\nExplosion produces focused tremors that break down certain materials"
				+"\nBreaks down sandstone into sand, mud into silt, ice into slush"
			);
		}

		public override void SetDefaults() {
			this.item.useStyle = ItemUseStyleID.SwingThrow;
			this.item.shootSpeed = 5f;
			this.item.shoot = ModContent.ProjectileType<SeismicChargeProjectile>();
			this.item.width = 20;
			this.item.height = 20;
			this.item.maxStack = 99;
			this.item.consumable = true;
			this.item.UseSound = SoundID.Item1;
			this.item.useAnimation = 25;
			this.item.useTime = 25;
			this.item.noUseGraphic = true;
			this.item.noMelee = true;
			this.item.damage = 0;
			this.item.value = Item.buyPrice( 0, 0, 20, 0 );
			this.item.rare = ItemRarityID.LightRed;
		}
		
		public override void AddRecipes() {
			var recipe = new SeismicChargeRecipe( this );
			recipe.AddRecipe();
		}
	}




	class SeismicChargeRecipe : ModRecipe {
		public SeismicChargeRecipe( SeismicChargeItem myitem ) : base( AMMod.Instance ) {
			this.AddTile( TileID.WorkBenches );

			this.AddIngredient( ItemID.Bomb, 12 );
			this.AddIngredient( ItemID.Obsidian, 1 );
			this.AddRecipeGroup( "AdventureMode.Orb", 1 );

			this.SetResult( myitem, 6 );
		}

		public override bool RecipeAvailable() {
			return AMConfig.Instance.SeismicChargeRecipeEnabled;
		}
	}
}
