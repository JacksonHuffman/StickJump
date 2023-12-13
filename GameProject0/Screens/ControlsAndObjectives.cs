using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject0.SpriteClasses;
using GameProject0.StateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GameProject0.Screens
{
    public class ControlsAndObjectives : GameScreen
    {
        ContentManager _content;
        //TimeSpan _displayTime;

        Game _game;

        private int _lives;

        private KeyboardState currentKeyboardState;

        private KeyboardState priorKeyboardState;

        public ControlsAndObjectives(Game game, int lives)
        {
            _game = game;
            _lives = lives;
        }

        public override void Activate()
        {
            base.Activate();

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            //_displayTime = TimeSpan.FromSeconds(10);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Enter) && priorKeyboardState.IsKeyUp(Keys.Enter))
            {
                ScreenManager.AddScreen(new LevelOneTransition(_game, _lives), null);
            }

            /*
            _displayTime -= gameTime.ElapsedGameTime;
            if (_displayTime <= TimeSpan.Zero)
            {
                //ExitScreen();
                ScreenManager.AddScreen(new LevelOneTransition(_game), null);
            }
            */
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.DrawString(ScreenManager.TitleFont, "CONTROLS AND OBJECTIVES", new Vector2(105, 2), Color.Coral);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.TitleFont, "CONTROLS AND OBJECTIVES", new Vector2(108, 5), Color.LightGreen);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.TitleFont, "CONTROLS AND OBJECTIVES", new Vector2(111, 8), Color.CornflowerBlue);
            //ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "Objective #1: Dodge the falling daggers", new Vector2(220, 300), Color.Yellow, 0f, new Vector2(0, 0), scale: 0.4f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "W/Up Arrow - Jump", new Vector2(275, 100), Color.Coral, 0f, new Vector2(0, 0), scale: 0.5f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "A/Left Arrow - Move Left", new Vector2(255, 150), Color.LightGreen, 0f, new Vector2(0, 0), scale: 0.5f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "D/Right Arrow - Move Right", new Vector2(240, 200), Color.CornflowerBlue, 0f, new Vector2(0, 0), scale: 0.5f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "PRESS ENTER TO START!", new Vector2(160, 300), Color.OrangeRed, 0f, new Vector2(0, 0), scale: 0.8f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.EntriesFont, "Objective: Given 2 Lives, Complete 3 Mini Games to Win!", new Vector2(85, 400), Color.LightBlue, 0f, new Vector2(0, 0), scale: 0.5f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.End();
        }
    }
}
