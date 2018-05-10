﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Threading;
using System.Collections.Generic;
using TerminalGame.UI.Modules;
using TerminalGame.Utilities.TextHandler;
using TerminalGame.Computers;
using TerminalGame.OS;

namespace TerminalGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Menu mainMenu;
        
        private SpriteFont font, fontXL, testfont, menuFont, fontS;
        private Song bgm_game, bgm_menu;
        SoundEffect yay, click1;
        private readonly string GameTitle;

        enum GameState { Menu, Game }

        GameState gameState;

        Terminal terminal;

        Computer playerComp;

        Rectangle bgR;
        TestModule module;
        Texture2D bg;

        /// <summary>
        /// Main game constructor
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GameTitle = "TerminalGame v0.1a";
            IsFixedTimeStep = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Console.WriteLine("Initializing...");
            KeyboardInput.Initialize(this, 500f, 20);
            Window.Title = GameTitle;
            
            IsMouseVisible = true;

            //graphics.PreferredBackBufferHeight = 960;
            //graphics.PreferredBackBufferWidth = 1440;

            //Set game to fullscreen
            graphics.HardwareModeSwitch = false;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            MediaPlayer.IsRepeating = true;
            base.Initialize();
            Console.WriteLine("Done initializing");
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Console.WriteLine("Loading content...");
            base.LoadContent();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("Fonts/terminalFont");
            menuFont = Content.Load<SpriteFont>("Fonts/terminalFontL");
            fontXL = Content.Load<SpriteFont>("Fonts/terminalFontXL");
            testfont = Content.Load<SpriteFont>("Fonts/terminalFontXS");
            fontS = Content.Load<SpriteFont>("Fonts/terminalFontS");
            yay = Content.Load<SoundEffect>("Audio/Sounds/yay");
            click1 = Content.Load<SoundEffect>("Audio/Sounds/click1");
            bgm_game = Content.Load<Song>("Audio/Music/ambientbgm1_2");
            bgm_menu = Content.Load<Song>("Audio/Music/mainmenu");
            MediaPlayer.Play(bgm_menu);

            mainMenu = new Menu(GameTitle, spriteBatch, menuFont, GraphicsDevice);

            mainMenu.ButtonClicked += MainMenu_ButtonClicked;
            
            bg = Content.Load<Texture2D>("Textures/bg");
            
            Console.WriteLine("Done loading");
        }

        /// <summary>
        /// To happen when Exit has been called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected override void OnExiting(object sender, EventArgs args)
        {
            Console.WriteLine("Exiting...");
            base.OnExiting(sender, args);
        }

        /// <summary>
        /// When a button on the main menu is clicked
        /// </summary>
        /// <param name="e">Contains the text of the pressed button</param>
        private void MainMenu_ButtonClicked(UI.ButtonPressedEventArgs e)
        {
            Console.WriteLine("e: " + e.Button);
            switch(e.Button)
            {
                case "New Game":
                    {
                        MediaPlayer.Stop();
                        StartNewGame();
                        break;
                    }
                case "Load Game":
                    {
                        break;
                    }
                case "Settings":
                    {
                        break;
                    }
                case "Quit Game":
                    {
                        Exit();
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// When game window is focused
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">EventArgs</param>
        protected override void OnActivated(object sender, EventArgs args)
        {
            base.OnActivated(sender, args);
            //history.Add("Window regained focus\n");
            //UpdateOutput();
        }

        /// <summary>
        /// When game window loses focus (eg. alt+tab)
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">EventArgs</param>
        protected override void OnDeactivated(object sender, EventArgs args)
        {
            base.OnDeactivated(sender, args);
            //history.Add("Window lost focus\n");
            //UpdateOutput();
        }

        private void StartNewGame()
        {
            Console.WriteLine("Starting new game...");

            MediaPlayer.Play(bgm_game);

            playerComp = new Computer(Computer.Type.Workstation, "127.0.0.1", "localhost", "pasword");
            
            Computers.Computers.DoComputers();
            Computers.Computers.computerList.Add(playerComp);

            playerComp.GetRoot();
            playerComp.Connect(true);

            Player.GetInstance().PlayersComputer = playerComp;

            bgR = new Rectangle(new Point(0, 0), new Point(bg.Width, bg.Height));
            
            terminal = new Terminal(GraphicsDevice, new Rectangle(3, 3, 700, graphics.PreferredBackBufferHeight - 6), font)
            {
                BackgroundColor = Color.Black * 0.75f,
                BorderColor = Color.RoyalBlue,
                HeaderColor = Color.RoyalBlue,
                Title = "Terminal v0.1",
                Font = fontS,
            };

            module = new TestModule(GraphicsDevice, new Rectangle(750, 500, 400, 200), fontS, click1, yay)
            {
                BackgroundColor = Color.Pink * 0.5f,
                BorderColor = Color.White,
                HeaderColor = Color.DeepPink,
                Title = "Super Barbie Button Clicker!!!",
                Font = fontS,
            };

            Console.WriteLine("INIT: Name:" + playerComp.Name);
            Console.WriteLine("INIT: Connect: " + playerComp.IsPlayerConnected);
            Console.WriteLine("CHK: Connect: " + (Player.GetInstance().PlayersComputer != null).ToString());
            terminal.Init();
            Console.WriteLine("Game started");

            gameState = GameState.Game;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                MediaPlayer.Stop();
                if(gameState > 0)
                    terminal.ForceQuit();
                Exit();
            }
            
            KeyboardInput.Update();
            switch((int)gameState)
            {
                case 0:
                    {
                        mainMenu.Update();
                        break;
                    }
                case 1:
                    {
                        module.Update(gameTime);
                        terminal.Update(gameTime);
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(blendState: BlendState.AlphaBlend);
            base.Draw(gameTime);
            switch ((int)gameState)
            {
                case 0:
                    {
                        mainMenu.Draw(spriteBatch, Window, fontXL);
                        break;
                    }
                case 1:
                    {
                        spriteBatch.Draw(bg, bgR, Color.White);
                        module.Draw(spriteBatch);
                        terminal.Draw(spriteBatch);
                        break;
                    }
                default:
                        break;
            }
            spriteBatch.End();
        }
    }
}
