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
        Texture2D _levelOne;
        TimeSpan _displayTime;

        Game _game;

        public LevelTwoTransition(Game game)
        {
            _game = game;   
        }

        public override void Activate()
        {
            base.Activate();

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _levelOne = _content.Load<Texture2D>("LevelTwo");
            _displayTime = TimeSpan.FromSeconds(3);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            _displayTime -= gameTime.ElapsedGameTime;
            if (_displayTime <= TimeSpan.Zero)
            {
                //ExitScreen();
                ScreenManager.AddScreen(new LevelTwoGamePlay(_game), null);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.Draw(_levelOne, new Vector2(160, 50), Color.White);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, "SUPER STICK 'SPLOSION", new Vector2(140, 2), Color.OrangeRed);
            ScreenManager.SpriteBatch.End();
        }
    }
}
