using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameProject0.StateManagement;
using System;
using GameProject0.SpriteClasses;

namespace GameProject0.Screens
{
    public class LevelThreeTransition : GameScreen
    {
        ContentManager _content;
        TimeSpan _displayTime;

        private DaggerSprite _dagger;

        private int _lives;

        private int _coinCount;

        Game _game;

        public LevelThreeTransition(Game game, int lives, int coinCount)
        {
            _game = game;
            _lives = lives;
            _coinCount = coinCount;
        }

        public override void Activate()
        {
            base.Activate();

            _dagger = new DaggerSprite(new Vector2(330, 120), new Vector2(0,0), true);

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _dagger.LoadContent(_content);
            _displayTime = TimeSpan.FromSeconds(10);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            _displayTime -= gameTime.ElapsedGameTime;
            if (_displayTime <= TimeSpan.Zero)
            {
                //ExitScreen();
                ScreenManager.AddScreen(new LevelThreeGamePlay(_game, _lives, _coinCount), null);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            ScreenManager.SpriteBatch.Begin();
            _dagger.Draw(gameTime, ScreenManager.SpriteBatch);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.TitleFont, "Lvl 3 - SPEEDY STICK SLICE", new Vector2(100, 2), Color.Coral);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.TitleFont, "Lvl 3 - SPEEDY STICK SLICE", new Vector2(103, 5), Color.LightGreen);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.TitleFont, "Lvl 3 - SPEEDY STICK SLICE", new Vector2(106, 8), Color.CornflowerBlue);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "Objective #1: Dodge the falling daggers", new Vector2(220, 300), Color.Yellow, 0f, new Vector2(0, 0), scale: 0.4f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "Objective #2: Collect the key", new Vector2(275, 325), Color.CornflowerBlue, 0f, new Vector2(0, 0), scale: 0.4f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "Objective #3: Jump to the portal", new Vector2(255, 350), Color.Coral, 0f, new Vector2(0, 0), scale: 0.4f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "LIVES REMAINING: " + _lives.ToString(), new Vector2(295, 400), Color.Red, 0f, new Vector2(0, 0), scale: 0.4f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.End();
        }
    }
}