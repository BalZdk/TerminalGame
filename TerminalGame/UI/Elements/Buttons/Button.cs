﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TerminalGame.UI.Elements.Buttons
{
    class Button : UIElement
    {
        protected readonly string TEXT;
        protected SpriteFont _font;
        protected ButtonPressedEventArgs _buttonPressed;

        public delegate void ButtonPressedEventHandler(ButtonPressedEventArgs e);
        public event ButtonPressedEventHandler ButtonPressed;

        public Button(Game game, string text, Point location, Point size) : base(game, location, size)
        {
            _borderColor = Color.White * _opacity;
            TEXT = text;
            _font = Utils.FontManager.GetFont("FontL");
            _buttonPressed = new ButtonPressedEventArgs();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Visible && !_fadingDown && !_fadingUp)
                return;

            if (_mouseDown)
            {
                _backgroundColor = Color.Green * _opacity;
                _borderColor = Color.LimeGreen * _opacity;
            }
            else if (_isHovering)
            {
                _backgroundColor = Color.DarkGray * _opacity;
                _borderColor = Color.Green * _opacity;
            }
            else
            {
                _backgroundColor = Color.Gray * _opacity;
                _borderColor = Color.LimeGreen * _opacity;
            }

            _spriteBatch.Begin();
            _spriteBatch.Draw(Utils.Globals.DummyTexture(), Rectangle,
                              _backgroundColor * _opacity);

            _spriteBatch.DrawString(_font, TEXT, new Vector2(Rectangle.Center.X - _font.MeasureString(TEXT).X / 2, Rectangle.Center.Y - _font.MeasureString(TEXT).Y / 2), Color.White * _opacity);

            Utils.Globals.DrawOuterBorder(_spriteBatch, Rectangle, Utils.Globals.DummyTexture(), 1,
                                          _borderColor * _opacity);
            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_backgroundColor == null)
                _backgroundColor = Color.DarkGray;
        }

        protected override void OnClick(MouseEventArgs e)
        {
            base.OnClick(e);
            ButtonPressed?.Invoke(_buttonPressed);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
        }

        protected override void OnMouseHover(MouseEventArgs e)
        {
            base.OnMouseHover(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
        }
    }

    /// <summary>
    /// Generic button event args
    /// </summary>
    public class ButtonPressedEventArgs : EventArgs
    {
        // TODO: Specify ButtonPressedEventArgs content
    }
}