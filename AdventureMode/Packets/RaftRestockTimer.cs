using System;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Network.SimplePacket;
using AdventureMode.Logic;


namespace AdventureMode.Packets {
	class RaftRestockTimerPacket : SimplePacketPayload {
		public static void SendToClients( long ticks, int toWho=-1 ) {
			if( Main.netMode != NetmodeID.Server ) {
				throw new ModLibsException( "Not server." );
			}

			var protocol = new RaftRestockTimerPacket( ticks );

			SimplePacket.SendToClient( protocol, toWho, -1 );
		}



		////////////////

		public long Ticks;



		////////////////

		private RaftRestockTimerPacket() { }

		private RaftRestockTimerPacket( long ticks ) {
			this.Ticks = ticks;
		}

		////////////////

		public override void ReceiveOnServer( int fromWho ) {
			throw new ModLibsException( "Not implemented" );
		}

		public override void ReceiveOnClient() {
			WorldLogic.InititalizeTimerHUD( (int)this.Ticks );
		}
	}
}
