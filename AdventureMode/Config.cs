using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.UI.ModConfig;
using System.Linq;

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
			clone.RaftBarrelContents = this.RaftBarrelContents.ToList();
			clone.RaftBarrelContents = this.RaftBarrelContents.ToList();

			//RaftBarrelContents RaftBarrelRestockSelection ShopPriceScales ShopWhitelists TilePlaceWhitelist
			return base.Clone();
		}
	}
}
