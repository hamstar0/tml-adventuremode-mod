using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AdventureMode.Projectiles;
using AdventureMode.Recipes;


namespace AdventureMode.Items {
	public class ThermalChargeItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Thermal Charge" );
			this.Tooltip.SetDefault( "Tossed as a bomb; explodes after a few seconds"
				+"\nExplosion produces intense heat that degrades meltable materials"
				+"\nMelts ice into slush"
			);
		}

		public override void SetDefaults() {
			this.item.useStyle = ItemUseStyleID.SwingThrow;
			this.item.shootSpeed = 5f;
			this.item.shoot = ModContent.ProjectileType<ThermalChargeProjectile>();
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
			this.item.value = Item.buyPrice( 0, 0, 30, 0 );
			this.item.rare = ItemRarityID.Pink;
		}
		
		public override void AddRecipes() {
			var recipe = new ThermalChargeRecipe( this );
			recipe.AddRecipe();
		}
	}
}
