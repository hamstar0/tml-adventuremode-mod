using System;
using Terraria;
using Terraria.ID;


namespace AdventureMode.Projectiles {
	public partial class SeismicChargeProjectile : BaseExplosiveChargeProjectile {
		public override ushort? GetReplacementTileType( int tileType ) {
			switch( tileType ) {
			// Sandstone
			case TileID.Sandstone:
				return TileID.Sand;
			case TileID.CorruptSandstone:
				return TileID.Ebonsand;
			case TileID.CrimsonSandstone:
				return TileID.Crimsand;
			case TileID.HallowSandstone:
				return TileID.Pearlsand;
			// Hardened sand
			case TileID.HardenedSand:
				return TileID.Sandstone;
			case TileID.CorruptHardenedSand:
				return TileID.CorruptSandstone;
			case TileID.CrimsonHardenedSand:
				return TileID.CrimsonSandstone;
			case TileID.HallowHardenedSand:
				return TileID.HallowSandstone;
			// Bricks
			case TileID.CrimtaneBrick:
			case TileID.EbonstoneBrick:
			case TileID.ObsidianBrick:
			case TileID.HellstoneBrick:
				return TileID.Silt;
			default:
				return null;
			}
		}
	}
}
