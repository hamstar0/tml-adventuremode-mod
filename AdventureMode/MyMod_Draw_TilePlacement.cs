using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Draw;
using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Services.AnimatedColor;
using AdventureMode.Tiles;
using AdventureMode.Items;


namespace AdventureMode {
	partial class AdventureModeMod : Mod {
		private (int X, int Y) PlacementOutlineTile = (0, 0);
		private int PlacementOutlineLinger = 0;



		////////////////

		private void DrawCurrentTilePlacementOutline() {
			int mouseWorldX = (int)Main.screenPosition.X + Main.mouseX;
			int mouseWorldY = (int)Main.screenPosition.Y + Main.mouseY;

			if( !PlayerInteractionHelpers.IsWithinTilePlacementReach(mouseWorldX, mouseWorldY) ) {
				return;
			}

			int mouseTileX = mouseWorldX >> 4;
			int mouseTileY = mouseWorldY >> 4;
			float outlineIntensity = 1f;
			
			if( this.PlacementOutlineTile.X == mouseTileX && this.PlacementOutlineTile.Y == mouseTileY ) {
				outlineIntensity = 1f - ((float)this.PlacementOutlineLinger / 60f);
				if( outlineIntensity < 0.1f ) {
					outlineIntensity = 0.1f;
				}
				this.PlacementOutlineLinger++;
			} else {
				this.PlacementOutlineTile = (mouseTileX, mouseTileY);
				this.PlacementOutlineLinger = 0;
			}

			Item heldItem = Main.LocalPlayer.HeldItem;

			switch( heldItem.type ) {
			case ItemID.BlueBrickPlatform:
			case ItemID.BonePlatform:
			case ItemID.BorealWoodPlatform:
			case ItemID.CactusPlatform:
			case ItemID.CrystalPlatform:
			case ItemID.DynastyPlatform:
			case ItemID.EbonwoodPlatform:
			case ItemID.FleshPlatform:
			case ItemID.FrozenPlatform:
			case ItemID.GlassPlatform:
			case ItemID.GoldenPlatform:
			case ItemID.GranitePlatform:
			case ItemID.GreenBrickPlatform:
			case ItemID.HoneyPlatform:
			case ItemID.LihzahrdPlatform:
			case ItemID.LivingWoodPlatform:
			case ItemID.MarblePlatform:
			case ItemID.MartianPlatform:
			case ItemID.MeteoritePlatform:
			case ItemID.MushroomPlatform:
			case ItemID.ObsidianPlatform:
			case ItemID.PalmWoodPlatform:
			case ItemID.PearlwoodPlatform:
			case ItemID.PinkBrickPlatform:
			case ItemID.PumpkinPlatform:
			case ItemID.RichMahoganyPlatform:
			case ItemID.ShadewoodPlatform:
			case ItemID.SkywarePlatform:
			case ItemID.SlimePlatform:
			case ItemID.SpookyPlatform:
			case ItemID.SteampunkPlatform:
			case ItemID.TeamBlockBluePlatform:
			case ItemID.TeamBlockGreenPlatform:
			case ItemID.TeamBlockPinkPlatform:
			case ItemID.TeamBlockRedPlatform:
			case ItemID.TeamBlockWhitePlatform:
			case ItemID.TeamBlockYellowPlatform:
			case ItemID.WoodPlatform:
			case ItemID.BlinkrootPlanterBox:
			case ItemID.CorruptPlanterBox:
			case ItemID.CrimsonPlanterBox:
			case ItemID.DayBloomPlanterBox:
			case ItemID.FireBlossomPlanterBox:
			case ItemID.MoonglowPlanterBox:
			case ItemID.ShiverthornPlanterBox:
			case ItemID.WaterleafPlanterBox:
				this.DrawPlatformTilePlacementOutline( outlineIntensity );
				break;
			case ItemID.Rope:
			case ItemID.SilkRope:
			case ItemID.VineRope:
			case ItemID.WebRope:
				this.DrawRopeTilePlacementOutline( outlineIntensity );
				break;
			default:
				if( heldItem.type == ModContent.ItemType<FramingPlankItem>() ) {
					this.DrawPlankTilePlacementOutline( outlineIntensity );
				}
				break;
			}
		}


		////////////////

		private void DrawPlatformTilePlacementOutline( float outlineIntensity ) {
			int maxLength = AdventureModeConfig.Instance.MaxPlatformBridgeLength;
			int tileX = ( (int)Main.screenPosition.X + Main.mouseX ) >> 4;
			int tileY = ( (int)Main.screenPosition.Y + Main.mouseY ) >> 4;

			//

			bool isAnchor( int x, int y ) {
				Tile tile = Main.tile[x, y];
				return tile.active() && Main.tileSolid[tile.type] && !Main.tileSolidTop[tile.type];
			}

			int traceRight() {
				for( int i = 0; i < maxLength; i++ ) {
					if( isAnchor( tileX + i, tileY ) ) {
						return i;
					}
				}
				return maxLength;
			}
			int traceLeft() {
				for( int i = 0; i < maxLength; i++ ) {
					if( isAnchor( tileX - i, tileY ) ) {
						return i;
					}
				}
				return maxLength;
			}

			//

			if( !isAnchor( tileX, tileY ) ) {
				if( isAnchor( tileX - 1, tileY ) ) {
					this.DrawTilePlacementOutline( outlineIntensity, new Rectangle( tileX, tileY, traceRight(), 1 ) );
				} else if( isAnchor( tileX + 1, tileY ) ) {
					int width = traceLeft();
					this.DrawTilePlacementOutline( outlineIntensity, new Rectangle( ( tileX - width ) + 1, tileY, width, 1 ) );
				} else {
					if( isAnchor( tileX, tileY - 1 ) || isAnchor( tileX, tileY + 1 ) ) {
						this.DrawTilePlacementOutline( outlineIntensity, new Rectangle( tileX, tileY, 1, 1 ), false );
					}
				}
			}
		}

		private void DrawRopeTilePlacementOutline( float outlineIntensity ) { }

		private void DrawPlankTilePlacementOutline( float outlineIntensity ) {
			int tileX = ( (int)Main.screenPosition.X + Main.mouseX ) >> 4;
			int tileY = ( (int)Main.screenPosition.Y + Main.mouseY ) >> 4;
			int plankTileType = ModContent.TileType<FramingPlankTile>();

			//

			bool isAnchor( int x, int y ) {
				Tile tile = Main.tile[x, y];
				bool isMyAnchor = tile.active()
					&& Main.tileSolid[tile.type]
					&& !Main.tileSolidTop[tile.type]
					&& tile.type != plankTileType;

				return isMyAnchor;
			}

			int trace( int dirX, int dirY ) {
				int max = dirY != 0
					? AdventureModeConfig.Instance.MaxFramingPlankVerticalLength
					: AdventureModeConfig.Instance.MaxFramingPlankHorizontalLength;

				for( int i = 0; i < max; i++ ) {
					if( isAnchor( tileX + ( i * dirX ), tileY + ( i * dirY ) ) ) {
						return i;
					}
				}
				return max;
			}

			//

			if( !isAnchor( tileX, tileY ) ) {
				if( isAnchor( tileX - 1, tileY ) ) {
					this.DrawTilePlacementOutline( outlineIntensity, new Rectangle( tileX, tileY, trace( 1, 0 ), 1 ) );
				} else if( isAnchor( tileX + 1, tileY ) ) {
					int width = trace( -1, 0 );
					this.DrawTilePlacementOutline( outlineIntensity, new Rectangle( ( tileX - width ) + 1, tileY, width, 1 ) );
				}

				if( isAnchor( tileX, tileY - 1 ) ) {
					this.DrawTilePlacementOutline( outlineIntensity, new Rectangle( tileX, tileY, 1, trace( 0, 1 ) ) );
				} else if( isAnchor( tileX, tileY + 1 ) ) {
					int height = trace( 0, -1 );
					this.DrawTilePlacementOutline( outlineIntensity, new Rectangle( tileX, ( tileY - height ) + 1, 1, height ) );
				}
			}
		}


		////////////////

		private void DrawTilePlacementOutline( float outlineIntensity, Rectangle tileRect, bool isValid = true ) {
			var scrRect = new Rectangle {
				X = ( tileRect.X << 4 ) - (int)Main.screenPosition.X,
				Y = ( tileRect.Y << 4 ) - (int)Main.screenPosition.Y,
				Width = tileRect.Width << 4,
				Height = tileRect.Height << 4
			};

			Color bgColor = Color.White * 0.15f * outlineIntensity;
			if( !isValid ) {
				bgColor.G = bgColor.B = 0;
			}

			Color edgeColor = AnimatedColors.Air.CurrentColor * 0.25f * outlineIntensity;
			if( !isValid ) {
				edgeColor.G = edgeColor.B = 0;
			}

			DrawHelpers.DrawBorderedRect( Main.spriteBatch, bgColor, edgeColor, scrRect, 2 );
		}
	}
}
