using AdventureMode.Items;
using AdventureMode.Tiles;
using HamstarHelpers.Helpers.Recipes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode {
	public class AdventureModeMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-adventuremode-mod";


		////////////////

		public static AdventureModeMod Instance { get; private set; }

		public static AdventureModeConfig Config => ModContent.GetInstance<AdventureModeConfig>();



		////////////////

		public AdventureModeMod() {
			AdventureModeMod.Instance = this;
		}

		////////////////

		public override void Load() {
			ManaCrystalShardTile.InitializeSingleton();
			AdventureModeWorld.InitializeSingleton();
		}

		public override void Unload() {
			AdventureModeMod.Instance = null;
		}


		////////////////

		public override void PostSetupContent() {
			/*CustomHotkeys.BindActionToKey1( "Illuminate", () => {
				var manaTileSingleton = ModContent.GetInstance<ManaCrystalShardTile>();
				foreach( (int tileX, IDictionary<int, float> tileYs) in manaTileSingleton.IlluminatedCrystals.ToArray() ) {
					foreach( (int tileY, float illum) in tileYs.ToArray() ) {
						manaTileSingleton.IlluminatedCrystals[tileX][tileY] = 1f;
					}
				}
				Main.NewText("Lit!");
			} );*/
		}

		public override void PostAddRecipes() {
			if( AdventureModeMod.Config.ManaCrystalShardsPerManaCrystal > 0 ) {
				foreach( Recipe recipe in RecipeFinderHelpers.GetRecipesOfItem( ItemID.ManaCrystal ) ) {
					for( int i = 0; i < recipe.requiredItem.Length; i++ ) {
						if( recipe.requiredItem[i] != null && !recipe.requiredItem[i].IsAir ) {
							continue;
						}
						recipe.requiredItem[i].SetDefaults( ModContent.ItemType<ManaCrystalShardItem>() );
						recipe.requiredItem[i].stack = AdventureModeMod.Config.ManaCrystalShardsPerManaCrystal;
					}
				}
			}
		}
	}
}
