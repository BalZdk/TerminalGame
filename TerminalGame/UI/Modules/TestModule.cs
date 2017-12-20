﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TerminalGame.Utilities;

namespace TerminalGame.UI.Modules
{
    class TestModule : Module
    {
        public override SpriteFont Font { get; set; }
        public override Color BackgroundColor { get; set; }
        public override Color BorderColor { get; set; }
        public override Color HeaderColor { get; set; }
        public override bool IsActive { get; set; }
        public override string Title { get; set; }

        private Button button;
        
        private SpriteFont fnt;

        private int counter;
        private string buttonClicks;

        public TestModule(GraphicsDevice Graphics, Rectangle Container, SpriteFont font) : base(Graphics, Container)
        {
            fnt = font;
            if(BackgroundColor == null)
            {
                BackgroundColor = Color.LightPink;
            }
            if (BorderColor == null)
            {
                BackgroundColor = Color.Chartreuse;
            }
            if (HeaderColor == null)
            {
                BackgroundColor = Color.Red;
            }
            if (string.IsNullOrEmpty(Title))
            {
                Title = "!!! UNNAMED WINDOW !!!";
            }

            counter = 0;
            buttonClicks = counter + " button clicks!";

            var width = 200;
            var height = 50;
            var x = container.X + (container.Width / 2) - width / 2;
            var y = container.Y + (container.Height - height) - 15;
            button = new Button("CLICK ME!", new Rectangle(x, y, width, height), fnt, Color.White, Color.Pink, Color.HotPink, Color.Red, graphics);
            button.Click += Button_Click;
        }

        private void Button_Click(ButtonPressedEventArgs e)
        {
            counter++;
            buttonClicks = counter + " button clicks!";
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = Drawing.DrawBlankTexture(graphics);
            Drawing.DrawBorder(spriteBatch, container, texture, 1, BorderColor);
            spriteBatch.Draw(texture, container, BackgroundColor);
            spriteBatch.Draw(texture, RenderHeader(), HeaderColor);
            spriteBatch.DrawString(Font, Title, new Vector2(RenderHeader().X + 5, RenderHeader().Y + 2), Color.White);
            spriteBatch.DrawString(Font, buttonClicks, new Vector2(container.X + (container.Width / 2) - Font.MeasureString(buttonClicks).X / 2, container.Y + (container.Height / 2) - Font.MeasureString(buttonClicks).Y / 2), Color.White);
            
            button.Draw(spriteBatch);
        }

        protected override Rectangle RenderHeader()
        {
            return new Rectangle(container.X + 1, container.Y + 1, container.Width - 1, (int)Font.MeasureString(Title).Y);
        }

        public override void Update(GameTime gameTime)
        {
            button.Update();
        }
    }
}