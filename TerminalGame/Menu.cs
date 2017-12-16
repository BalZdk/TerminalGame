﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using TerminalGame.Utilities;
using TerminalGame.Utilities.TextHandler;
using TerminalGame.UI;
using System;

namespace TerminalGame
{
    class Menu
    {
        public delegate void ButtonPressedEventHandler(ButtonPressedEventArgs e);
        List<Button> buttons;
        public event ButtonPressedEventHandler ButtonClicked;

        private string gameTitle;
        public Menu(string GameTitle, SpriteBatch spriteBatch, SpriteFont font, GraphicsDevice graphics)
        {
            gameTitle = GameTitle;

            float leftMargin = 50f;
            float topMargin = 250f;
            float spacing = 20f;
            int width = 500;
            int height = 50;
            int counter = 0;

            Button newGame = new Button("New Game", width, height, font, graphics)
            {
            };
            Button loadGame = new Button("Load Game", width, height, font, graphics)
            {
            };
            Button settings = new Button("Settings", width, height, font, graphics)
            {
            };
            Button quit = new Button("Quit Game", width, height, font, graphics)
            {
            };
            buttons = new List<Button>()
            {
                newGame,
                loadGame,
                settings,
                quit,
            };
            foreach(Button b in buttons)
            {
                b.Position = new Vector2(leftMargin, (topMargin + counter * height + counter * spacing));
                counter++;
            }
            newGame.Click += NewGame_Click;
            loadGame.Click += LoadGame_Click;
            settings.Click += Settings_Click;
            quit.Click += Quit_Click;
        }

        private void Quit_Click(ButtonPressedEventArgs e)
        {
            ButtonClicked?.Invoke(e);
        }

        private void Settings_Click(ButtonPressedEventArgs e)
        {
            ButtonClicked?.Invoke(e);
        }

        private void LoadGame_Click(ButtonPressedEventArgs e)
        {
            ButtonClicked?.Invoke(e);
        }

        private void NewGame_Click(ButtonPressedEventArgs e)
        {
            ButtonClicked?.Invoke(e);
        }

        public void Update()
        {
            foreach (Button b in buttons)
            {
                b.Update();
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameWindow Window, SpriteFont Font)
        {
            Vector2 textMiddlePoint = Font.MeasureString(gameTitle) / 2;
            Vector2 position = new Vector2((Window.ClientBounds.Width - textMiddlePoint.X - 15), textMiddlePoint.Y + 15);
            Vector2 position2 = new Vector2((Window.ClientBounds.Width - textMiddlePoint.X - 15) + TestClass.ShakeStuff(3), textMiddlePoint.Y + 15 + TestClass.ShakeStuff(3));
            spriteBatch.DrawString(Font, gameTitle, position2, Color.Green, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(Font, gameTitle, position, Color.LightGray, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);

            foreach (Button b in buttons)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}
