using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Automation;
using GameProject0.SpriteClasses;
using GameProject0.StateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject0.Screens
{
    public class WinnerScreen : GameScreen
    {
        ContentManager _content;

        private int _lives;

        private int _coins;

        Game _game;

        private KeyboardState currentKeyboardState;

        private KeyboardState priorKeyboardState;

        public WinnerScreen(Game game, int lives, int coins)
        {
            _game = game;
            _lives = lives;
            _coins = coins;
        }

        public override void Activate()
        {
            base.Activate();

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Enter) && priorKeyboardState.IsKeyUp(Keys.Enter))
            {
                ScreenManager.AddScreen(new MainMenuScreen(_game, 2), null);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.DrawString(ScreenManager.TitleFont, "WINNER", new Vector2(235, 2), Color.Coral, 0f, new Vector2(0,0), scale: 2, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.TitleFont, "WINNER", new Vector2(238, 5), Color.LightGreen, 0f, new Vector2(0, 0), scale: 2, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.TitleFont, "WINNER", new Vector2(241, 8), Color.CornflowerBlue, 0f, new Vector2(0, 0), scale: 2, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "Coins Collected: " + _coins.ToString(), new Vector2(180, 250), Color.LightBlue);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "Lives Lost: " + (2 - _lives).ToString(), new Vector2(250, 200), Color.Red);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "ENTER to MainMenu", new Vector2(155, 400), Color.Yellow);
            ScreenManager.SpriteBatch.End();
        }
    }
}
