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


namespace GameProject0.Screens
{
    public class LevelOneTransition : GameScreen
    {
        ContentManager _content;
        Texture2D _levelOne;
        TimeSpan _displayTime;

        private AlligatorSprite _alligator;

        private int _lives;

        Game _game;

        public LevelOneTransition(Game game, int lives)
        {
            _game = game;
            _lives = lives;
        }

        public override void Activate()
        {
            base.Activate();

            _alligator = new AlligatorSprite(new Vector2(525, 100), new Vector2(0, 0));

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            //_levelOne = _content.Load<Texture2D>("LevelOne");
            _alligator.LoadContent(_content);
            _displayTime = TimeSpan.FromSeconds(10);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            _displayTime -= gameTime.ElapsedGameTime;
            if (_displayTime <= TimeSpan.Zero)
            {
                //ExitScreen();
                ScreenManager.AddScreen(new LevelOneGamePlay(_game, _lives), null);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();
            //ScreenManager.SpriteBatch.Draw(_levelOne, new Vector2(85,0), Color.White);
            _alligator.Draw(gameTime, ScreenManager.SpriteBatch); 
            ScreenManager.SpriteBatch.DrawString(ScreenManager.TitleFont, "Lvl 1 - SCARY STICK SWAMP", new Vector2(100, 2), Color.Coral);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.TitleFont, "Lvl 1 - SCARY STICK SWAMP", new Vector2(103, 5), Color.LightGreen);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.TitleFont, "Lvl 1 - SCARY STICK SWAMP", new Vector2(106, 8), Color.CornflowerBlue);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "Objective: Survive 30 seconds on the moving platform without falling off", new Vector2(65, 300), Color.Yellow, 0f, new Vector2(0,0), scale: 0.4f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "Hint: The platform starts by moving to the right", new Vector2(185, 325), Color.CornflowerBlue, 0f, new Vector2(0, 0), scale: 0.4f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "Bonus: Jump to grab the coin in the cube", new Vector2(215, 350), Color.Coral, 0f, new Vector2(0, 0), scale: 0.4f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "LIVES REMAINING: " + _lives.ToString(), new Vector2(295, 400), Color.Red, 0f, new Vector2(0, 0), scale: 0.4f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.End();
        }
    }
}
