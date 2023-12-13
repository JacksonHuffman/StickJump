using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject0.StateManagement;
using GameProject0.SpriteClasses;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject0.Screens
{
    public class MainMenuBackgroundScreen : GameScreen
    {
        private ContentManager _content;
        private Texture2D _backgroundTexture;

        public MainMenuBackgroundScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            ScreenState = ScreenState.Active;
        }

        public override void Activate()
        {
            base.Activate();

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _backgroundTexture = _content.Load<Texture2D>("MainMenuBackground");
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            var spriteBatch = ScreenManager.SpriteBatch;
            var viewport = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen = new Rectangle(0,0, viewport.Width, viewport.Height);

            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexture, fullscreen, Color.White);

            spriteBatch.End();
        }
    }
}
