﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TerminalGame.Utilities;

namespace TerminalGame.Scenes
{
    class GameScene : IScene
    {
        public GameScene()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            OS.OS.GetInstance().Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            OS.OS.GetInstance().Update(gameTime);
        }
    }
}