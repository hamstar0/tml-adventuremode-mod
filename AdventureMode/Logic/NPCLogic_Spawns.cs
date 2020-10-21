using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static void EditSpawnPoolForBoundGoblin( IDictionary<int, float> pool, NPCSpawnInfo spawnInfo ) {
			var myworld = ModContent.GetInstance<AdventureModeWorld>();
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
	}
}
