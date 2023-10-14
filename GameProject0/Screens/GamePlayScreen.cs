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
using GameProject0.ParticleSystemFolder;
using System;
using SharpDX.Direct3D11;

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

        private Game _game;

        private SwampBubbleParticleSystem _bubbles;

        private bool _loserShake = false;

        private double _countdownTimer = 30.0;

        private float _shakeDuration;

        public GamePlayScreen(Game game)
        {
            _game = game;
        }

        public override void Activate()
        {
            base.Activate();

            _platformSprite = new PlatformSprite(new Vector2((ScreenManager.GraphicsDevice.Viewport.Width - 128) / 2, (ScreenManager.GraphicsDevice.Viewport.Height - 128) / 2), new Vector2((float)1, 0));
            _boomerangSprite = new BoomerangSprite(new Vector2(5, (ScreenManager.GraphicsDevice.Viewport.Height / 2) - 190), new Vector2((float)1, 0));
            _stickSprite = new StickSprite(new Vector2((ScreenManager.GraphicsDevice.Viewport.Width - 64) / 2, (ScreenManager.GraphicsDevice.Viewport.Height - 335) / 2));
            _swampSprite = new SwampSprite(new Vector2(0, 375));
            _alligatorSprite = new AlligatorSprite(new Vector2(300, 275), new Vector2(100, 0));

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _bubbles = new SwampBubbleParticleSystem(_game, new Rectangle(0, 300, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height));
            _game.Components.Add(_bubbles);

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

            _countdownTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your update logic here
            _alligatorSprite.Update(gameTime);
            _stickSprite.Update(gameTime);
            _platformSprite.Update(gameTime, ScreenManager.GraphicsDevice);
            _boomerangSprite.Update(gameTime, ScreenManager.GraphicsDevice);
            if (!_platformSprite.Bounds.CollidesWith(_stickSprite.Bounds))
            {
                _stickSprite.FallUpdate(gameTime);
                _loserShake = true;
                _shakeDuration = 0;
            }
            else
            {
                _loserShake = false;
            }

            if (_stickSprite.Bounds.CollidesWith(_swampSprite.Bounds))
            {
                ScreenManager.AddScreen(new GameOverScreen(), null);
                _bubbles.IsBubbling = false;
                this.Deactivate();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            
            Matrix shake = Matrix.Identity;
            if(_loserShake)
            {
                _shakeDuration += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                Matrix translation = Matrix.CreateTranslation(5 * MathF.Sin(_shakeDuration), 5 * MathF.Cos(_shakeDuration), 0);
                shake = translation;
            }
            

            var spriteBatch = ScreenManager.SpriteBatch;

            // TODO: Add your drawing code here
            spriteBatch.Begin(transformMatrix: shake);
            spriteBatch.DrawString(_bangers, _countdownTimer.ToString(), new Vector2(200, 200), Color.Purple);
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
