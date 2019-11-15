using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;


namespace AdventureMode {
	class BinocularsOverlay : Overlay {
		public Vector2 TargetPosition = Vector2.Zero;



		////////////////

		public BinocularsOverlay( EffectPriority priority = EffectPriority.VeryHigh, RenderLayers layer = RenderLayers.TilesAndNPCs )
				: base( priority, layer ) { }


		////////////////

		public override void Activate( Vector2 position, params object[] args ) {
			this.TargetPosition = position;
			this.Mode = OverlayMode.FadeIn;
		}

		public override void Deactivate( params object[] args ) {
			this.Mode = OverlayMode.FadeOut;
		}

		public override bool IsVisible() {
			return !Main.LocalPlayer.dead && Main.LocalPlayer.HeldItem.type == ItemID.Binoculars;
		}


		////////////////

		public override void Update( GameTime _ ) { }


		////////////////

		public override void Draw( SpriteBatch sb ) {
		}
	}
}
