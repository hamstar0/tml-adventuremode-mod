using HamstarHelpers.Helpers.World;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;


namespace AdventureMode {
	class AdventureModeWorld : ModWorld {
		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			int shards;

			switch( WorldHelpers.GetSize() ) {
			default:
			case WorldSize.SubSmall:
				shards = AdventureModeMod.Config.TinyWorldManaCrystalShards;
				break;
			case WorldSize.Small:
				shards = AdventureModeMod.Config.SmallWorldManaCrystalShards;
				break;
			case WorldSize.Medium:
				shards = AdventureModeMod.Config.MediumWorldManaCrystalShards;
				break;
			case WorldSize.Large:
				shards = AdventureModeMod.Config.LargeWorldManaCrystalShards;
				break;
			case WorldSize.SuperLarge:
				shards = AdventureModeMod.Config.HugeWorldManaCrystalShards;
				break;
			}

			tasks.Add( new AdventureModeGenPass(shards) );
		}
	}
}
