using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameProject0.StateManagement;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Media;

namespace GameProject0.Screens
{
    public class GamePlayScreen : GameScreen
    {
        ContentManager _content;

        private PlatformSprite _platformSprite;

        private StickSprite _stickSprite;

        private BoomerangSprite _boomerangSprite;

        private AlligatorSprite _alligatorSprite;

        private SwampSprite _swampSprite;

        private SpriteFont _bangers;

        private Song _backgroundMusic;

        public override void Activate()
        {
            base.Activate();

            _platformSprite = new PlatformSprite(new Vector2((ScreenManager.GraphicsDevice.Viewport.Width - 128) / 2, (ScreenManager.GraphicsDevice.Viewport.Height - 128) / 2), new Vector2((float)1, 0));
            _boomerangSprite = new BoomerangSprite(new Vector2(5, (ScreenManager.GraphicsDevice.Viewport.Height / 2) - 190), new Vector2((float)1, 0));
            _stickSprite = new StickSprite(new Vector2((ScreenManager.GraphicsDevice.Viewport.Width - 64) / 2, (ScreenManager.GraphicsDevice.Viewport.Height - 335) / 2));
            _swampSprite = new SwampSprite(new Vector2(0, 375));
            _alligatorSprite = new AlligatorSprite(new Vector2(0, 275), new Vector2(100, 0));

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _stickSprite.LoadContent(_content);
            _platformSprite.LoadContent(_content);
            _boomerangSprite.LoadContent(_content);
            _alligatorSprite.LoadContent(_content);
            _bangers = _content.Load<SpriteFont>("bangers");
            _swampSprite.LoadContent(_content);
            _backgroundMusic = _content.Load<Song>("Portron Portron Lopez - Hiver Fou");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_backgroundMusic);
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // TODO: Add your update logic here
            _alligatorSprite.Update(gameTime);
            _stickSprite.Update(gameTime);
            _platformSprite.Update(gameTime, ScreenManager.GraphicsDevice);
            _boomerangSprite.Update(gameTime, ScreenManager.GraphicsDevice);
            if (!_platformSprite.Bounds.CollidesWith(_stickSprite.Bounds))
            {
                _stickSprite.FallUpdate(gameTime);
            }

            if (_stickSprite.Bounds.CollidesWith(_swampSprite.Bounds))
            {
                ScreenManager.AddScreen(new GameOverScreen(), null);
                this.Deactivate();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            var spriteBatch = ScreenManager.SpriteBatch;

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            _platformSprite.Draw(gameTime, spriteBatch);
            _boomerangSprite.Draw(gameTime, spriteBatch);
            _stickSprite.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(_bangers, "StickJump!", new Vector2(20, 20), Color.Orange);
            spriteBatch.DrawString(_bangers, "To Exit: Press The H Key!", new Vector2(520, 20), Color.Red);
            _swampSprite.Draw(gameTime, spriteBatch);
            _alligatorSprite.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
