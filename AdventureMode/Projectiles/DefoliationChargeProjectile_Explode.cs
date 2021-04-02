using System;
using Terraria;
using Terraria.ID;


namespace AdventureMode.Projectiles {
	public partial class DefoliationChargeProjectile : BaseExplosiveChargeProjectile {
		public override ushort? GetReplacementTileType( int tileType ) {
			switch( tileType ) {
			case TileID.VineFlowers:
			case TileID.Vines:
			case TileID.CrimsonVines:
			case TileID.HallowedVines:
			case TileID.JungleVines:
			case TileID.FleshWeeds:
			case TileID.CorruptPlants:
			case TileID.PlantDetritus:
			case TileID.HallowedPlants:
			case TileID.HallowedPlants2:
			case TileID.LongMoss:
			case TileID.JunglePlants:
			case TileID.JunglePlants2:
			case TileID.Plants:
			case TileID.Plants2:
			case TileID.ImmatureHerbs:
			case TileID.MatureHerbs:
			case TileID.BloomingHerbs:
			case TileID.Trees:
			case TileID.MushroomTrees:
			case TileID.PalmTree:
				return null;
			case TileID.Grass:
			case TileID.CorruptGrass:
			case TileID.FleshGrass:
			case TileID.HallowedGrass:
				return TileID.Dirt;
			case TileID.JungleGrass:
			case TileID.MushroomGrass:
				return TileID.Mud;
			case TileID.Mud:
				return TileID.Silt;
			default:
				return null;
			}
		}
	}
}
