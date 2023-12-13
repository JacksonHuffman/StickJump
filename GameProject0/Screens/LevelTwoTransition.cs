using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject0.StateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject0.Screens
{
    public class LevelTwoTransition : GameScreen
    {
        ContentManager _content;
        Texture2D _texture;
        TimeSpan _displayTime;

        private int _lives;

        private int _coinCount;

        Game _game;

        public LevelTwoTransition(Game game, int lives, int coinCount)
        {
            _game = game;   
            _lives = lives;
            _coinCount = coinCount;
        }

        public override void Activate()
        {
            base.Activate();

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _texture = _content.Load<Texture2D>("Fire");
            //_levelOne = _content.Load<Texture2D>("LevelTwo");
            _displayTime = TimeSpan.FromSeconds(10);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            _displayTime -= gameTime.ElapsedGameTime;
            if (_displayTime <= TimeSpan.Zero)
            {
                //ExitScreen();
                ScreenManager.AddScreen(new LevelTwoGamePlay(_game, _lives, _coinCount), null);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.Draw(_texture, new Vector2(250, 100), Color.White);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.TitleFont, "Lvl 2 - SUPER STICK 'SPLOSION", new Vector2(70, 2), Color.Coral);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.TitleFont, "Lvl 2 - SUPER STICK 'SPLOSION", new Vector2(73, 5), Color.LightGreen);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.TitleFont, "Lvl 2 - SUPER STICK 'SPLOSION", new Vector2(76, 8), Color.CornflowerBlue);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "Objective: Progress to the next platform before it explodes", new Vector2(125, 350), Color.Yellow, 0f, new Vector2(0, 0), scale: 0.4f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "Bonus: Jump to grab the coin in the cube", new Vector2(208, 375), Color.Coral, 0f, new Vector2(0, 0), scale: 0.4f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "LIVES REMAINING: " + _lives.ToString(), new Vector2(295, 425), Color.Red, 0f, new Vector2(0, 0), scale: 0.4f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.End();
        }
    }
}
