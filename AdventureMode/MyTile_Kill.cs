using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using AdventureMode.Logic;


namespace AdventureMode {
	class AdventureModeExtendedTileHooks : ILoadable {
		void ILoadable.OnModsLoad() {
		}

		void ILoadable.OnModsUnload() {
		}

		void ILoadable.OnPostModsLoad() {
			TileLogic.InitializeTileKillBehaviors();
		}
	}




	partial class AMTile : GlobalTile {
		public override bool Drop( int i, int j, int type ) {
			if( !AMConfig.Instance.EnableAlchemyRecipes ) {
				switch( type ) {
				case TileID.ImmatureHerbs:
				case TileID.MatureHerbs:
				case TileID.BloomingHerbs:
					return false;
				}
			}

			return base.Drop( i, j, type );
		}
	}
}