﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TerminalGame.Utilities
{
    class Drawing
    {
        /// <summary>
        /// Will draw a blank texture. Add color in draw call with SpriteBatch.
        /// </summary>
        /// <param name="Graphics">GraphicsAdapter</param>
        /// <returns>Blank (white) Texture2D</returns>
        public static Texture2D DrawBlankTexture(GraphicsDevice Graphics)
        {
            Texture2D retval = new Texture2D(Graphics, 1, 1);
            retval.SetData(new[] { Color.White });
            return retval;
        }

        /// <summary>
        /// Draws a border around the object.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="Object">Size of border</param>
        /// <param name="Texture"></param>
        /// <param name="BorderWidth">Width of border. Negative values draws the border inside the object.</param>
        /// <param name="BorderColor">Color of the border. Use eg. Color.Red * 0.5f to make border 50% transparent</param>
        public static void DrawBorder(SpriteBatch spriteBatch, Rectangle Object, Texture2D Texture, int BorderWidth, Color BorderColor)
        {
            spriteBatch.Draw(Texture, new Rectangle(Object.Left - BorderWidth, Object.Top - BorderWidth, BorderWidth, Object.Height + 2 * BorderWidth), BorderColor); // Left
            spriteBatch.Draw(Texture, new Rectangle(Object.Right, Object.Top - BorderWidth, BorderWidth, Object.Height + 2 * BorderWidth), BorderColor); // Right
            spriteBatch.Draw(Texture, new Rectangle(Object.Left, Object.Top - BorderWidth, Object.Width, BorderWidth), BorderColor); // Top
            spriteBatch.Draw(Texture, new Rectangle(Object.Left, Object.Bottom, Object.Width, BorderWidth), BorderColor); // Bottom
        }
    }
}