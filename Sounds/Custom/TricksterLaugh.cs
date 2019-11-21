using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;


namespace TheTrickster.Sounds.Custom {
	public class TricksterLaugh : ModSound {
		public override SoundEffectInstance PlaySound( ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type ) {
			soundInstance.Volume = volume; //* 0.55f;
			soundInstance.Pan = pan;
			return soundInstance;
		}
	}
}
