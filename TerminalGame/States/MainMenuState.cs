﻿using Microsoft.Xna.Framework;
using TerminalGame.Screens;

namespace TerminalGame.States
{
    class MainMenuState : State
    {
        private static MainMenuState _instance;

        public static MainMenuState GetInstance()
        {
            if (_instance == null)
                _instance = new MainMenuState();
            return _instance;
        }

        private MainMenuState()
        {
            _name = "mainMenu";
        }

        public override void Initialize(GraphicsDeviceManager graphics, Screen screen, Game game)
        {
            base.Initialize(graphics, screen, game);
            AddState("gameLoading", GameLoadingState.GetInstance());
            AddState("gameRunning", GameRunningState.GetInstance());
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        protected override void OnStateChange(object sender, StateChangeEventArgs e)
        {
            base.OnStateChange(this, e);
            CurrentScreen.SwitchOn();
        }
    }
}
