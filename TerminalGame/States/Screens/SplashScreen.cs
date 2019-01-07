﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TerminalGame.Utils;

namespace TerminalGame.States.Screens
{
    class SplashScreen : Screen
    {
        private readonly string TITLE, VER;
        private readonly SpriteFont TITLE_FONT, VER_FONT;

        public SplashScreen(Game game) : base(game)
        {
            TITLE = "TerminalGame";
            var tg = game as TerminalGame;
            VER = String.Format("v{0}.{1}", tg.version.Major, tg.version.Minor);
            TITLE_FONT = FontManager.GetFont("FontXL");
            VER_FONT = FontManager.GetFont("FontM");
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, blendState: BlendState.AlphaBlend);
            base.Draw(gameTime);
            _spriteBatch.DrawString(TITLE_FONT, TITLE, new Vector2(Globals.GameWidth / 2 - TITLE_FONT.MeasureString(TITLE).X / 2, Globals.GameHeight / 2 - TITLE_FONT.MeasureString(TITLE).Y / 2), Color.White);
            _spriteBatch.DrawString(VER_FONT, VER, new Vector2(Globals.GameWidth / 2 + TITLE_FONT.MeasureString(TITLE).X / 2, Globals.GameHeight / 2), Color.Green);
            _spriteBatch.End();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            base.Initialize(graphics);
        }

        public override void SwitchOn()
        {
            base.SwitchOn();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }
    }
}
