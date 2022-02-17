using System;
using System.IO;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Data {
	public partial class RaftWorldData {
		public (int TileX, int TileY) Barrel { get; internal set; }

		public (int TileX, int TileY) Mirror { get; internal set; }


		////////////////
		
		public bool IsInitialized =>
			this.Barrel != default &&
			this.Mirror != default;



		////////////////

		public void NetReceive( BinaryReader reader ) {
			try {
				int barX = reader.ReadInt32();
				int barY = reader.ReadInt32();
				this.Barrel = (barX, barY);

				int mirX = reader.ReadInt32();
				int mirY = reader.ReadInt32();
				this.Mirror = (mirX, mirY);
			} catch { }
		}

		public void NetSend( BinaryWriter writer ) {
			try {
				writer.Write( (int)this.Barrel.TileX );
				writer.Write( (int)this.Barrel.TileY );

				writer.Write( (int)this.Mirror.TileX );
				writer.Write( (int)this.Mirror.TileY );
			} catch { }
		}
	}
}
