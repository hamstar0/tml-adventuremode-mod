using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.NPCs {
	partial class TricksterNPC : ModNPC {
		private bool IsEncountered = false;


		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Trickster" );
			Main.npcFrameCount[ this.npc.type ] = 10;
		}

		public override void SetDefaults() {
			this.npc.lifeMax = 10;
			this.npc.defense = 9999;
			this.npc.width = 18;
			this.npc.height = 40;
			this.npc.damage = 14;
			this.npc.HitSound = SoundID.NPCHit1;
			this.npc.DeathSound = SoundID.NPCDeath2;
			this.npc.value = 60f;
			this.npc.knockBackResist = 1f;
			this.npc.aiStyle = -1;//8
			this.npc.lavaImmune = true;
			this.npc.buffImmune[BuffID.OnFire] = true;
			this.npc.buffImmune[BuffID.CursedInferno] = true;
			this.animationType = NPCID.FireImp;
			this.banner = Item.NPCtoBanner( NPCID.FireImp );
			this.bannerItem = Item.BannerToItem( this.banner );
		}

		////////////////

		public override float SpawnChance( NPCSpawnInfo spawnInfo ) {
Main.NewText("Spawned trickster at "+spawnInfo.spawnTileX+","+spawnInfo.spawnTileY);
			return 10f;//0.05f;
		}


		////////////////

		public override void AI() {
			if( !this.IsEncountered ) {
				this.Encounter();
			}

			this.RunAI();
			base.AI();
		}


		////////////////

		public void Encounter() {
			Vector2 scrMid = Main.screenPosition;
			scrMid.X += Main.screenWidth / 2;
			scrMid.Y += Main.screenHeight / 2;
			float distSqr = Vector2.DistanceSquared( scrMid, this.npc.Center );

			if( distSqr < 409600 ) {
				this.IsEncountered = true;

				Vector2 diff = this.npc.Center - scrMid;
				Vector2 pos = scrMid + ( diff * 0.5f );

				int soundSlot = this.mod.GetSoundSlot( SoundType.Custom, "Sounds/Custom/TricksterLaugh" );
				Main.PlaySound( (int)SoundType.Custom, (int)pos.X, (int)pos.Y, soundSlot );
			}
		}
	}
}
