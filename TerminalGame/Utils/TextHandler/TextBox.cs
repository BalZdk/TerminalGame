﻿//From https://github.com/UnterrainerInformatik/Monogame-Textbox <3 -b

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TerminalGame.Utils.TextHandler
{
    /// <summary>
    /// Text input
    /// </summary>
    public class TextBox
    {
        /// <summary>
        /// GraphicsDevice used to render
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; set; }

        /// <summary>
        /// TextBox area
        /// </summary>
        public Rectangle Area
        {
            get { return Renderer.Area; }
            set { Renderer.Area = value; }
        }

        /// <summary>
        /// OG author did not comment anything
        /// </summary>
        public readonly Text Text;
        /// <summary>
        /// OG author did not comment anything
        /// </summary>
        public readonly TextRenderer Renderer;
        /// <summary>
        /// OG author did not comment anything
        /// </summary>
        public readonly Cursor Cursor;

        /// <summary>
        /// OG author did not comment anything
        /// </summary>
        public event EventHandler<KeyboardInput.KeyEventArgs> EnterDown;
        /// <summary>
        /// OG author did not comment anything
        /// </summary>
        public event EventHandler<KeyboardInput.KeyEventArgs> UpArrow;
        /// <summary>
        /// OG author did not comment anything
        /// </summary>
        public event EventHandler<KeyboardInput.KeyEventArgs> DnArrow;
        /// <summary>
        /// OG author did not comment anything
        /// </summary>
        public event EventHandler<KeyboardInput.KeyEventArgs> TabDown;

        private string clipboard;

        /// <summary>
        /// OG author did not comment anything
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// OG author did not comment anything
        /// </summary>
        /// <param name="area"></param>
        /// <param name="maxCharacters"></param>
        /// <param name="text"></param>
        /// <param name="graphicsDevice"></param>
        /// <param name="spriteFont"></param>
        /// <param name="cursorColor"></param>
        /// <param name="selectionColor"></param>
        /// <param name="ticksPerToggle"></param>
        public TextBox(Rectangle area, int maxCharacters, string text, GraphicsDevice graphicsDevice,
            SpriteFont spriteFont,
            Color cursorColor, Color selectionColor, int ticksPerToggle)
        {
            GraphicsDevice = graphicsDevice;

            Text = new Text(maxCharacters)
            {
                String = text
            };

            Renderer = new TextRenderer(this)
            {
                Area = area,
                Font = spriteFont,
                Color = Color.Black
            };

            Cursor = new Cursor(this, cursorColor, selectionColor, new Rectangle(0, 5, (int)spriteFont.MeasureString("_").Length()/2, (int)spriteFont.MeasureString("_").Length() / 2), ticksPerToggle);

            KeyboardInput.CharPressed += CharacterTyped;
            KeyboardInput.KeyPressed += KeyPressed;
        }

        /// <summary>
        /// OG author did not comment anything
        /// </summary>
        public void Dispose()
        {
            KeyboardInput.Dispose();
        }

        /// <summary>
        /// OG author did not comment anything
        /// </summary>
        public void Clear()
        {
            Text.RemoveCharacters(0, Text.Length);
            Cursor.TextCursor = 0;
            Cursor.SelectedChar = null;
        }

        private void KeyPressed(object sender, KeyboardInput.KeyEventArgs e, KeyboardState ks)
        {
            if (Active)
            {
                int oldPos = Cursor.TextCursor;
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        EnterDown?.Invoke(this, e);
                        break;
                    case Keys.Up:
                        UpArrow?.Invoke(this, e);
                        break;
                    case Keys.Down:
                        DnArrow?.Invoke(this, e);
                        break;
                    case Keys.Tab:
                        TabDown?.Invoke(this, e);
                        break;
                    case Keys.Left:
                        if (KeyboardInput.CtrlDown)
                        {
                            Cursor.TextCursor = IndexOfLastCharBeforeWhitespace(Cursor.TextCursor, Text.Characters);
                        }
                        else
                        {
                            Cursor.TextCursor--;
                        }
                        ShiftMod(oldPos);
                        break;
                    case Keys.Right:
                        if (KeyboardInput.CtrlDown)
                        {
                            Cursor.TextCursor = IndexOfNextCharAfterWhitespace(Cursor.TextCursor, Text.Characters);
                        }
                        else
                        {
                            Cursor.TextCursor++;
                        }
                        ShiftMod(oldPos);
                        break;
                    case Keys.Home:
                        Cursor.TextCursor = 0;
                        ShiftMod(oldPos);
                        break;
                    case Keys.End:
                        Cursor.TextCursor = Text.Length;
                        ShiftMod(oldPos);
                        break;
                    case Keys.Delete:
                        if (DelSelection() == null && Cursor.TextCursor < Text.Length)
                        {
                            Text.RemoveCharacters(Cursor.TextCursor, Cursor.TextCursor + 1);
                        }
                        break;
                    case Keys.Back:
                        if (DelSelection() == null && Cursor.TextCursor > 0)
                        {
                            Text.RemoveCharacters(Cursor.TextCursor - 1, Cursor.TextCursor);
                            Cursor.TextCursor--;
                        }
                        break;
                    case Keys.A:
                        if (KeyboardInput.CtrlDown)
                        {
                            if (Text.Length > 0)
                            {
                                Cursor.SelectedChar = 0;
                                Cursor.TextCursor = Text.Length;
                            }
                        }
                        break;
                    case Keys.C:
                        if (KeyboardInput.CtrlDown)
                        {
                            clipboard = DelSelection(true);
                        }
                        break;
                    case Keys.X:
                        if (KeyboardInput.CtrlDown)
                        {
                            if (Cursor.SelectedChar.HasValue)
                            {
                                clipboard = DelSelection();
                            }
                        }
                        break;
                    case Keys.V:
                        if (KeyboardInput.CtrlDown)
                        {
                            if (clipboard != null)
                            {
                                DelSelection();
                                foreach (char c in clipboard)
                                {
                                    if (Text.Length < Text.MaxLength)
                                    {
                                        Text.InsertCharacter(Cursor.TextCursor, c);
                                        Cursor.TextCursor++;
                                    }
                                }
                            }
                        }
                        break;
                    case Keys.D2:
                        if(KeyboardInput.CtrlDown && KeyboardInput.AltDown)
                        {
                            if (Text.Length < Text.MaxLength)
                            {
                                Text.InsertCharacter(Cursor.TextCursor, '@');
                                Cursor.TextCursor++;
                            }
                        }
                        break;
                    case Keys.D4:
                        if (KeyboardInput.CtrlDown && KeyboardInput.AltDown)
                        {
                            if (Text.Length < Text.MaxLength)
                            {
                                Text.InsertCharacter(Cursor.TextCursor, '$');
                                Cursor.TextCursor++;
                            }
                        }
                        break;
                }
            }
        }

        private void ShiftMod(int oldPos)
        {
            if (KeyboardInput.ShiftDown)
            {
                if (Cursor.SelectedChar == null)
                {
                    Cursor.SelectedChar = oldPos;
                }
            }
            else
            {
                Cursor.SelectedChar = null;
            }
        }

        private void CharacterTyped(object sender, KeyboardInput.CharacterEventArgs e, KeyboardState ks)
        {
            if (Active && !KeyboardInput.CtrlDown)
            {
                if (IsLegalCharacter(Renderer.Font, e.Character) && !e.Character.Equals('\r') &&
                    !e.Character.Equals('\n'))
                {
                    DelSelection();
                    if (Text.Length < Text.MaxLength)
                    {
                        Text.InsertCharacter(Cursor.TextCursor, e.Character);
                        Cursor.TextCursor++;
                    }
                }
            }
        }

        private string DelSelection(bool fakeForCopy = false)
        {
            if (!Cursor.SelectedChar.HasValue)
            {
                return null;
            }
            int tc = Cursor.TextCursor;
            int sc = Cursor.SelectedChar.Value;
            int min = Math.Min(sc, tc);
            int max = Math.Max(sc, tc);
            string result = Text.String.Substring(min, max - min);

            if (!fakeForCopy)
            {
                Text.Replace(Math.Min(sc, tc), Math.Max(sc, tc), string.Empty);
                if (Cursor.SelectedChar.Value < Cursor.TextCursor)
                {
                    Cursor.TextCursor -= tc - sc;
                }
                Cursor.SelectedChar = null;
            }
            return result;
        }

        /// <summary>
        /// OG author did not comment anything
        /// </summary>
        /// <param name="font"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsLegalCharacter(SpriteFont font, char c)
        {
            return font.Characters.Contains(c) || c == '\r' || c == '\n';
        }

        /// <summary>
        /// OG author did not comment anything
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="characters"></param>
        /// <returns></returns>
        public static int IndexOfNextCharAfterWhitespace(int pos, char[] characters)
        {
            char[] chars = characters;
            char c = chars[pos];
            bool whiteSpaceFound = false;
            while (true)
            {
                if (c.Equals(' '))
                {
                    whiteSpaceFound = true;
                }
                else if (whiteSpaceFound)
                {
                    return pos;
                }

                ++pos;
                if (pos >= chars.Length)
                {
                    return chars.Length;
                }
                c = chars[pos];
            }
        }

        /// <summary>
        /// OG author did not comment anything
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="characters"></param>
        /// <returns></returns>
        public static int IndexOfLastCharBeforeWhitespace(int pos, char[] characters)
        {
            char[] chars = characters;

            bool charFound = false;
            while (true)
            {
                --pos;
                if (pos <= 0)
                {
                    return 0;
                }
                var c = chars[pos];

                if (c.Equals(' '))
                {
                    if (charFound)
                    {
                        return ++pos;
                    }
                }
                else
                {
                    charFound = true;
                }
            }
        }

        /// <summary>
        /// OG author did not comment anything
        /// </summary>
        public void Update()
        {
            Renderer.Update();
            Cursor.Update();
        }

        /// <summary>
        /// OG author did not comment anything
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Renderer.Draw(spriteBatch);
            if (Active)
            {
                Cursor.Draw(spriteBatch);
            }
        }
    }
}