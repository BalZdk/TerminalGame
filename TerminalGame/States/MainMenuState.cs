﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TerminalGame.Utilities;

namespace TerminalGame.States
{
    class MainMenuState : State
    {
        public static MainMenuState Instance => new MainMenuState();

        private MainMenuState() { }

        public override State Next(GameState state)
        {
            switch(state)
            {
                case GameState.GameRunning:
                    return GameRunningState.Instance;
                case GameState.GameLoading:
                    return GameLoadingState.Instance;
                case GameState.SettingsMenu:
                    return SettingsMenuState.Instance;
                case GameState.LoadMenu:
                    return LoadMenuState.Instance;
                default:
                    return this;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Scenes.SceneManager.GetScene(Scenes.SceneManager.Scene.Menu).Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            Scenes.SceneManager.GetScene(Scenes.SceneManager.Scene.Menu).Update(gameTime);
        }
    }
}