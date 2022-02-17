using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Data {
	public partial class RaftModData : ILoadable {
		public const string RaftRestockTimerName = "AdventureModeRaftRestock";


		////////////////

		public static RaftModData Instance => ModContent.GetInstance<RaftModData>();



		////////////////

		internal int RaftRestockTimerSnapshot = 0;



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() { }
	}
}
