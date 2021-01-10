using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Objectives;
using Objectives.Definitions;
using AdventureModeLore.Lore;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static void EditSpawnPoolForBoundGoblin( IDictionary<int, float> pool, NPCSpawnInfo spawnInfo ) {
			var myworld = ModContent.GetInstance<AMWorld>();
			int goblinX = myworld.UndergroundDesertBounds.X + (myworld.UndergroundDesertBounds.Width / 2);
			int goblinY = myworld.UndergroundDesertBounds.Y + ((2 * myworld.UndergroundDesertBounds.Height) / 3);
			var goblinPos = new Vector2( goblinX * 16, goblinY * 16 );
			float minDistSqr = 64 * 16;	// 64 blocks
			minDistSqr *= minDistSqr;

			if( (spawnInfo.player.position - goblinPos).LengthSquared() >= minDistSqr ) {
				pool.Remove( NPCID.BoundGoblin );
			} else {
				pool[ NPCID.BoundGoblin ] = 1f;
			}
		}

		public static void EditSpawnPoolForBoundMechanic( IDictionary<int, float> pool, NPCSpawnInfo spawnInfo ) {
			var myworld = ModContent.GetInstance<AMWorld>();
			int mechanicTileX = myworld.DungeonBottom.tileX;
			int mechanicTileY = myworld.DungeonBottom.tileY;
			var mechanicPos = new Vector2( mechanicTileX * 16, mechanicTileY * 16 );
			float minDistSqr = 32 * 16;	// 32 blocks
			minDistSqr *= minDistSqr;

			if( (spawnInfo.player.position - mechanicPos).LengthSquared() >= minDistSqr ) {
				pool.Remove( NPCID.BoundMechanic );
			} else {
				pool[ NPCID.BoundMechanic] = 1f;
			}
		}

		public static void EditSpawnPoolForVoodooDemon( IDictionary<int, float> pool, NPCSpawnInfo spawnInfo ) {
			if( Main.hardMode ) {
				return;
			}

			Objective wofObj = ObjectivesAPI.GetObjective( LoreEvents.ObjectiveTitle_SummonWoF );
			if( wofObj == null ) {
				pool.Remove( NPCID.VoodooDemon );
			}
		}
	}
}
