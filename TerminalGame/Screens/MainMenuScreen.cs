﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using TerminalGame.States;
using TerminalGame.UI.Elements.Buttons;
using TerminalGame.Utils;

namespace TerminalGame.Screens
{
    class MainMenuScreen : Screen
    {
        private readonly List<Button> BUTTONS;
        private readonly string _gameTitle, _binary, _version;
        private int _binaryXpos;
        private SpriteFont _titleFont, _versionFont;
        private Texture2D _smoke;
        private Rectangle _smokeRect1, _smokeRect2;

        public MainMenuScreen(Game game) : base(game)
        {
            Button continueGame = new Button(game, "Continue", new Point(100, 200), new Point(400, 50), !Game.IsGameRunning);
            Button newGame = new Button(game, "New Game", new Point(100, 275), new Point(400, 50), !Game.IsGameRunning);
            Button loadGame = new Button(game, "Load Game", new Point(100, 350), new Point(400, 50), !Game.IsGameRunning);
            Button settings = new Button(game, "Settings", new Point(100, 425), new Point(400, 50), !Game.IsGameRunning);
            Button quit = new Button(game, "Quit", new Point(100, 500), new Point(400, 50), !Game.IsGameRunning);

            continueGame.ButtonPressed += ContinueGame_Clicked;
            newGame.ButtonPressed += NewGame_Clicked;
            loadGame.ButtonPressed += LoadGame_Clicked;
            settings.ButtonPressed += Settings_Clicked;
            quit.ButtonPressed += Quit_Clicked;

            BUTTONS = new List<Button>
            {
                continueGame,
                newGame,
                loadGame,
                settings,
                quit
            };

            continueGame.Enabled = false;

            _gameTitle = Game.Title;
            _version = Game.Version;
            _binary = "0100110101000101010100110101001101000001010001110100010100100000010000100100010101000111010010010100111001010011001110100010000001001001011001100010000001100011011101010111001001101001011011110111001101101001011101000111100100100000011001110110111101110100001000000111010001101000011001010010000001100010011001010111010001110100011001010111001000100000011011110110011000100000011110010110111101110101001011000010000001100001011011100110010000100000011110010110111101110101001000000110000101100011011101000111010101100001011011000110110001111001001000000111001101110000011001010110111001110100001000000111010001101001011011010110010100100000011001000110010101100011011011110110010001101001011011100110011100100000011101000110100001101001011100110010000001110011011101000111001001100101011000010110110100100000011011110110011000100000011000100110100101110100011100110010110000100000011110010110111101110101001000000110110101101001011001110110100001110100001000000110010001101111001000000111011101100101011011000110110000100000011010010110111000100000011101000110100001101001011100110010000001100111011000010110110101100101001011000010000001101001011001100010000001101001011101000010000001100101011101100110010101110010001000000110011101100101011101000111001100100000011001000110111101101110011001010010000000101101001000000110100101110100001001110111001100100000011000110111010101110010011100100110010101101110011101000110110001111001001000000110100101101110001000000110000101101100011100000110100001100001001000000011000000101110001100110010000000101000011110010110111101110101001000000110001101100001011011100010000001110011011001010110010100100000011000010010000001110011011000110111001001100101011001010110111001110011011010000110111101110100001000000110111101100110001000000111011101101000011000010111010000100000011101000110100001100101001000000110011101100001011011010110010100100000011011000110111101101111011010110110010101100100001000000110110001101001011010110110010100100000011000010111010000100000011101000110100001101001011100110010000001110011011101000110000101100111011001010010000001101000011001010111001001100101001110100010000001101000011101000111010001110000011100110011101000101111001011110110100100101110011010010110110101100111011101010111001000101110011000110110111101101101001011110011000101000010010101100111011001010100001110010100100100101110011100000110111001100111001010010010111000100000010011010110000101111001011000100110010100100000011110010110111101110101001001110111011001100101001000000110000101101100011100100110010101100001011001000111100100100000011000110110111101101101011100000110110001100101011101000110010101100100001000000111010001101000011001010010000001100111011000010110110101100101001000000010100001101001011001100010000001110100011010000110000101110100001001110111001100100000011001010111011001100101011011100010000001110000011011110111001101110011011010010110001001101100011001010011101100100000011101110110100001101111001000000110101101101110011011110111011101110011001011100010000001001001011001100010000001110011011011110010110000100000011101000110100001100001011011100110101100100000011110010110111101110101001000000111001101101111001000000110110101110101011000110110100000100000011001100110111101110010001000000111000001101100011000010111100101101001011011100110011100100000011011010111100100100000011001110110000101101101011001010010000100100000010010010010000001101000011011110111000001100101001000000111100101101111011101010010000001100101011011100110101001101111011110010110010101100100001000000110100101110100001011100010000001001001001001110110110100100000011100110111010001101001011011000110110000100000011100110110100101110100011101000110100101101110011001110010000001101000011001010111001001100101001011000010000001100100011100100110010101100001011001000110100101101110011001110010000001110100011010000110010100100000011100000111001001101111011000110110010101110011011100110010000001101111011001100010000001101101011000010110101101101001011011100110011100100000011101000110100001100101001000000111001101100001011101100110010100101111011011000110111101100001011001000010000001100110011101010110111001100011011101000110100101101111011011100110000101101100011010010111010001111001001011100010111000101110001000000100100100100000011001100111010101100011011010110110100101101110011001110010000001101000011000010111010001100101001000000111000001100001011100100111001101101001011011100110011100100000010110000100110101001100001000000110011001101001011011000110010101110011001000000010110100100000011000010110111001100100001000000111100101101111011101010010000001101000011000010111011001100101001000000111010001101111001000000111001001100101011001000110111100100000011010010111010000100000011011000110100101101011011001010010000000110001001101110010000001110100011010010110110101100101011100110010000001100010011001010110001101100001011101010111001101100101001000000110111001100101011101110010000001100110011001010110000101110100011101010111001001100101011100110010000001100001011011100110010000100000011100110111010001110101011001100110011000100000011100110111010101100100011001000110010101101110011011000111100100100000011011100110010101100101011001000111001100100000011101000110100001100101011010010111001000100000011100110111010001100001011101000110010100100000011100110110000101110110011001010110010000100000011000010111001100100000011101110110010101101100011011000010111000101110001011100010000001100110011101010110001101101011001000000110110101100101001011100010000001000001011011100111100101110111011000010111100100101100001000000110000101110011001000000100100100100000011101110111001001101001011101000110010100100000011101000110100001101001011100110010110000100000011101000110100001101001011100110010000001110011011101000111001001100101011000010110110100100000011010010111001100100000011100110111010101110000011100000110111101110011011001010110010000100000011101000110111100100000011100110110001101110010011011110110110001101100001000000110001001111001001000000110000101110100001000000111010001101000011001010010000001100010011011110111010001110100011011110110110100100000011011110110011000100000011101000110100001100101001000000110110101100001011010010110111000100000011011010110010101101110011101010010110000100000011010010111010000100000011011010110100101100111011010000111010000100000011011100110111101110100001000000110010101110110011001010110111000100000011000100110010100100000011010010110111000100000011101000110100001100101001000000110011101100001011011010110010100100000011101110110100001100101011011100010000001111001011011110111010100100000011100100110010101100001011001000010000001110100011010000110100101110011001011100010000001000100011010010110010000100000011110010110111101110101001000000110100001100001011101100110010100100000011101000110111100100000011001000110100101100111001000000110100101110100001000000110111101110101011101000010000001101111011001100010000001110011011011110110110101100101001000000110010001100101011011110110001001100110011101010111001101100011011000010111010001100101011001000010110000100000011001000110010101100011011011110110110101110000011010010110110001100101011001000010000001000011001000110010000001100110011010010110110001100101001111110010000001001111011010000110100000101100001000000100100100100000011011010110100101100111011010000111010000100000011011010110000101101011011001010010000001101101011011110111001001100101001000000110111101100110001000000111010001101000011001010111001101100101001000000110110001101001011101000111010001101100011001010010000001100010011011000110111101100111001011010111001101110100011110010110110001100101001000000110010101100001011100110111010001100101011100100010110101100101011001110110011100100000011101000110100001101001011011100110011101110011001011100010111000101110001000000111010001101000011000010111010000100000011000110110111101110101011011000110010000100000011000100110010100100000011000010010000001100110011101010110111000100000011101000110100001101001011011100110011100100000011001100110111101110010001000000111001101101111011011010110010101101111011011100110010100100000011101000110111100100000011001000110100101100111001000000110000101110010011011110111010101101110011001000010000001100110011011110111001000100000001110100101000000100000010001010100111101000110";
            _binaryXpos = 0 - (int)FontManager.GetFont("FontS").MeasureString(_binary).X + Game.Window.ClientBounds.Width;

            _titleFont = FontManager.GetFont("FontXL");
            _versionFont = FontManager.GetFont("FontM");

            _smokeRect1 = new Rectangle(0, 0, Globals.GameWidth * 2, (int)(Globals.GameHeight * 1.25f));
            _smokeRect2 = new Rectangle(-Globals.GameWidth, (int)(Globals.GameHeight * 0.1), Globals.GameWidth * 2, (int)(Globals.GameHeight * 1.25f));
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _smoke = Content.Load<Texture2D>("Graphics/Textures/Backgrounds/smoke");
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            base.Initialize(graphics);
        }

        public override void SwitchOn()
        {
            base.SwitchOn();
            MusicManager.GetInstance().Start("mainMenuBgm");
        }

        public override void Draw(GameTime gameTime)
        {
            if (_smoke == null)
                return;

            _spriteBatch.Begin(SpriteSortMode.Immediate, blendState: BlendState.AlphaBlend);

            base.Draw(gameTime);

            _spriteBatch.Draw(_smoke, _smokeRect1, Color.White * 0.1f);
            _spriteBatch.Draw(_smoke, _smokeRect2, Color.White * 0.2f);

            _spriteBatch.DrawString(FontManager.GetFont("FontS"), _binary, 
                new Vector2(_binaryXpos, Game.Window.ClientBounds.Height - FontManager.GetFont("FontS").MeasureString("A").Y), Color.Green * 0.25f);
            Vector2 textMiddlePoint = _titleFont.MeasureString(_gameTitle) / 2;
            Vector2 position = new Vector2((Game.Window.ClientBounds.Width - textMiddlePoint.X - 15), textMiddlePoint.Y + 15);
            Vector2 position2 = new Vector2((Game.Window.ClientBounds.Width - textMiddlePoint.X - 15) + Generators.ShakeStuff(3), 
                textMiddlePoint.Y + 15 + Generators.ShakeStuff(3));
            Vector2 position3 = new Vector2((Game.Window.ClientBounds.Width - _versionFont.MeasureString(_version).X / 2 - 20), 
                position.Y + _titleFont.MeasureString(_gameTitle).Y / 2 - 15);
            _spriteBatch.DrawString(_titleFont, _gameTitle, position2, Color.Green, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);
            _spriteBatch.DrawString(_titleFont, _gameTitle, position, Color.LightGray, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);
            _spriteBatch.DrawString(_versionFont, _version, position3, Color.Green, 0, 
                _versionFont.MeasureString(_version) / 2, 1.0f, SpriteEffects.None, 0.5f);
            foreach (var b in BUTTONS)
                b.Draw(gameTime);
            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_smokeRect1.Location.X > -Globals.GameWidth)
                _smokeRect1.Offset(-2, 0);
            else
                _smokeRect1.Offset(Globals.GameWidth, 0);
            if (_smokeRect2.Location.X < 0)
                _smokeRect2.Offset(1, 0);
            else
                _smokeRect2.Offset(-Globals.GameWidth, 0);
            foreach (var b in BUTTONS)
                b.Update(gameTime);

            if (gameTime.TotalGameTime.Milliseconds % 100 == 0)
            {
                if (_binaryXpos > -10)
                {
                    _binaryXpos = 0 - (int)FontManager.GetFont("FontS").MeasureString(_binary).X + Game.Window.ClientBounds.Width + 8;
                }
                else
                {
                    _binaryXpos += (int)FontManager.GetFont("FontS").MeasureString("A").X;
                }
            }
        }

        private void ContinueGame_Clicked(ButtonPressedEventArgs e)
        {
            Console.WriteLine("Continue clicked");
        }

        private void NewGame_Clicked(ButtonPressedEventArgs e)
        {
            Console.WriteLine("New Game clicked");
            StateMachine.GetInstance().ChangeState("gameLoading", new GameLoadingScreen(Game));
        }

        private void LoadGame_Clicked(ButtonPressedEventArgs e)
        {
            Console.WriteLine("Load Game clicked");
        }

        private void Settings_Clicked(ButtonPressedEventArgs e)
        {
            Console.WriteLine("Settings clicked");
        }

        private void Quit_Clicked(ButtonPressedEventArgs e)
        {
            Console.WriteLine("Quit clicked");
            Game.Exit();
        }
    }
}
