using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ModLibsCore.Classes.UI.ModConfig;


namespace AdventureMode {
	class MyFloatInputElement : FloatInputElement { }




	public partial class AMConfig : ModConfig {
		public static AMConfig Instance => ModContent.GetInstance<AMConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		public override void OnLoaded() {
			this.OnLoadedTiles();
		}


		public override ModConfig Clone() {
			var clone = base.Clone() as AMConfig;
			clone.RaftBarrelContents = this.RaftBarrelContents
				.Select( iqd => new ItemQuantityDefinition(iqd) )
				.ToList();
			clone.RaftBarrelRestockSelection = this.RaftBarrelRestockSelection
				.Select( iqd => new ItemQuantityDefinition( iqd ) )
				.ToList();
			clone.ShopPriceScales = this.ShopPriceScales.ToDictionary(
				kv => new ItemDefinition( kv.Key.mod, kv.Key.name ),
				kv => new ScaleSetting( kv.Value.Scale )
			);
			clone.ShopWhitelists = this.ShopWhitelists.ToDictionary(
				kv => new NPCDefinition( kv.Key.mod, kv.Key.name ),
				kv => kv.Value
					.Select( i => new ItemDefinition( i.mod, i.name ) )
					.ToList()
			);

			return clone;
		}
	}
}
