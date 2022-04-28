using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Objectives;
using Objectives.Definitions;
using AdventureModeLore.Lore.Dialogues.Events;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static (int tileX, int tileY) GetBoundGoblinOrigin() {
			var myworld = ModContent.GetInstance<AMWorld>();
			int goblinX = myworld.UndergroundDesertBounds.X + (myworld.UndergroundDesertBounds.Width / 2);	// 1/2
			int goblinY = myworld.UndergroundDesertBounds.Y + ((3 * myworld.UndergroundDesertBounds.Height) / 4);	// 3/4

			// halfway in, 3/4 down
			return (goblinX, goblinY);
		}


		////////////////

		public static bool CanBoundGoblinSpawn( NPCSpawnInfo spawnInfo, bool alsoValidateExistingNpcs ) {
			(int x, int y) goblinOrigTile = NPCLogic.GetBoundGoblinOrigin();
			var goblinOrigWldPos = new Vector2(goblinOrigTile.x, goblinOrigTile.y) * 16f;

			float distSqr = (spawnInfo.player.position - goblinOrigWldPos).LengthSquared();

			float minDist = 64f * 16f;    // 64 blocks
			float minDistSqr = minDist * minDist;

			//

			if( distSqr < minDistSqr ) {
				if( alsoValidateExistingNpcs ) {
					if( NPC.AnyNPCs(NPCID.BoundGoblin) || NPC.AnyNPCs(NPCID.GoblinTinkerer) ) {
						return false;
					}
				}
				return true;
			}

			return false;
		}

		public static bool CanBoundMechanicSpawn( NPCSpawnInfo spawnInfo, bool alsoValidateExistingNpcs ) {
			var myworld = ModContent.GetInstance<AMWorld>();
			int mechanicTileX = myworld.DungeonBottom.TileX;
			int mechanicTileY = myworld.DungeonBottom.TileY;
			var mechanicPos = new Vector2( mechanicTileX * 16, mechanicTileY * 16 );
			float distSqr = (spawnInfo.player.position - mechanicPos).LengthSquared();

			float minDist = 96 * 16;    // 96 blocks
			float minDistSqr = minDist * minDist;

			//

			if( distSqr < minDistSqr ) {
				if( alsoValidateExistingNpcs ) {
					if( NPC.AnyNPCs(NPCID.BoundMechanic) || NPC.AnyNPCs(NPCID.Mechanic) ) {
						return false;
					}
				}
				return true;
			}

			return false;
		}

		public static bool CanVoodooDemonSpawn( NPCSpawnInfo spawnInfo ) {
			if( Main.hardMode ) {
				return true;
			}

			Objective wofObj = ObjectivesAPI.GetObjective( DialogueLoreEventDefinitions.ObjectiveTitle_SummonWoF );
			return wofObj != null;
		}
	}
}
