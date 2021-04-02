using System;
using Terraria;
using Terraria.ID;


namespace AdventureMode.Projectiles {
	public partial class ThermalChargeProjectile : BaseExplosiveChargeProjectile {
		public override ushort? GetReplacementTileType( int tileType ) {
			switch( tileType ) {
			// Ice
			case TileID.IceBlock:
			case TileID.CorruptIce:
			case TileID.FleshIce:
			case TileID.HallowedIce:
				return TileID.Slush;
			default:
				return null;
			}
		}
	}
}
