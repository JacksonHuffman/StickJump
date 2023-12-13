using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameProject0.StateManagement;
using Microsoft.Xna.Framework.Input;
//using System.Windows.Forms;
using Microsoft.Xna.Framework.Media;
using GameProject0.ParticleSystemFolder;
using System;
using GameProject0.SpriteClasses;
using GameProject0.Collisions;
using Microsoft.Xna.Framework.Audio;

namespace GameProject0.Screens
{
    public class LevelOneGamePlay : GameScreen
    {
        ContentManager _content;

        private PlatformSprite _platformSprite;

        private StickSprite _stickSprite;

        private BoomerangSprite _boomerangSprite;

        private AlligatorSprite _alligatorSprite;

        private SwampSprite _swampSprite;

        private LeftCastle _leftCastle;

        private RightCastle _rightCastle;

        private SpriteFont _bangers;

        private Song _backgroundMusic;

        private SoundEffect _coinCollect;

        private BoundingRectangle _coinCubeRec = new BoundingRectangle(new Vector2(390, 20), 10, 10);

        private Texture2D _block;

        private bool _coinCollected = false;

        private Game _game;

        private SwampBubbleParticleSystem _bubbles;

        private bool _loserShake = false;

        private double _countdownTimer = 30.0;

        private float _shakeDuration;

        CoinCube _coinCube;

        private TimeSpan _winnerTime;

        private bool _lost = false;

        private bool _startGame = false;

        private int _lives;

        private int _coinCount = 0;

        private KeyboardState currentKeyboardState;

        private KeyboardState priorKeyboardState;

        public LevelOneGamePlay(Game game, int lives)
        {
            _game = game;
            _lives = lives;
        }

        public override void Activate()
        {
            base.Activate();

            _platformSprite = new PlatformSprite(new Vector2((ScreenManager.GraphicsDevice.Viewport.Width - 128) / 2, (ScreenManager.GraphicsDevice.Viewport.Height - 128) / 2), new Vector2((float)1, 0));
            _boomerangSprite = new BoomerangSprite(new Vector2(5, (ScreenManager.GraphicsDevice.Viewport.Height / 2) - 190), new Vector2((float)1, 0));
            _stickSprite = new StickSprite(new Vector2((ScreenManager.GraphicsDevice.Viewport.Width - 64) / 2, (ScreenManager.GraphicsDevice.Viewport.Height - 335) / 2), 1);
            _swampSprite = new SwampSprite(new Vector2(0, 375));
            _alligatorSprite = new AlligatorSprite(new Vector2(300, 275), new Vector2(100, 0));
            _leftCastle = new LeftCastle(new Vector2(-37, 115));
            _rightCastle = new RightCastle(new Vector2(550, 115));

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _bubbles = new SwampBubbleParticleSystem(_game, new Rectangle(0, 300, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height));
            _game.Components.Add(_bubbles);

            _stickSprite.LoadContent(_content);
            _platformSprite.LoadContent(_content);
            _boomerangSprite.LoadContent(_content);
            _alligatorSprite.LoadContent(_content);
            _bangers = _content.Load<SpriteFont>("bangers");
            _swampSprite.LoadContent(_content);
            _leftCastle.LoadContent(_content);
            _rightCastle.LoadContent(_content);
            _backgroundMusic = _content.Load<Song>("Portron Portron Lopez - Hiver Fou");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_backgroundMusic);
            _winnerTime = TimeSpan.FromSeconds(30);
            _block = _content.Load<Texture2D>("Block");
            _coinCollect = _content.Load<SoundEffect>("CoinCollect");

            _coinCube = new CoinCube(_game);
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            _coinCube.Update(gameTime);

            if (_startGame)
            {
                _countdownTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                _winnerTime -= gameTime.ElapsedGameTime;

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
                    _stickSprite.AllowedUpdate(gameTime);
                    _loserShake = false;
                }

                if (_stickSprite.Bounds.CollidesWith(_swampSprite.Bounds))
                {
                    _lost = true;
                    if(_lives > 1)
                    {
                        _lives -= 1;
                        ScreenManager.AddScreen(new LevelOneTransition(_game, _lives), null);
                    }
                    else
                    {
                        ScreenManager.AddScreen(new MainMenuScreen(_game, _lives + 1), null);
                    }
                    //ScreenManager.AddScreen(new LevelOneTransition(_game, _lives), null);
                    _bubbles.IsBubbling = false;
                    MediaPlayer.Stop();
                    ExitScreen();
                }

                if (_winnerTime <= TimeSpan.Zero && !_lost)
                {
                    ScreenManager.AddScreen(new LevelTwoTransition(_game, _lives, _coinCount), null);
                    _bubbles.IsBubbling = false;
                    MediaPlayer.Stop();
                    ExitScreen();
                }

                if(_stickSprite.Bounds.CollidesWith(_coinCubeRec) && !_coinCollected)
                {
                    _coinCollected = true;
                    _coinCount++;
                    _coinCollect.Play();
                }
            }

            if(currentKeyboardState.IsKeyDown(Keys.Enter) && priorKeyboardState.IsKeyUp(Keys.Enter))
            {
                _startGame = true;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);
            
            /*
            Matrix shake = Matrix.Identity;
            if(_loserShake)
            {
                _shakeDuration += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                Matrix translation = Matrix.CreateTranslation(5 * MathF.Sin(_shakeDuration), 5 * MathF.Cos(_shakeDuration), 0);
                shake = translation;
            }
            */
            
            var spriteBatch = ScreenManager.SpriteBatch;

            // TODO: Add your drawing code here
            spriteBatch.Begin(/*transformMatrix: shake*/);
            _leftCastle.Draw(gameTime, spriteBatch);
            _rightCastle.Draw(gameTime, spriteBatch);
            if(_startGame)
            {
                spriteBatch.DrawString(_bangers, _countdownTimer.ToString(), new Vector2(275, 250), Color.Purple);
            }
            else
            {
                spriteBatch.DrawString(_bangers, "Press ENTER to begin!", new Vector2(275, 250), Color.Purple);
            }
            _platformSprite.Draw(gameTime, spriteBatch);
            //_boomerangSprite.Draw(gameTime, spriteBatch);
            _stickSprite.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(_bangers, "StickJump! - LvL 1", new Vector2(20, 20), Color.Orange);
            spriteBatch.DrawString(_bangers, "To Exit: Press The ESC Key", new Vector2(510, 20), Color.Red);
            _swampSprite.Draw(gameTime, spriteBatch);
            _alligatorSprite.Draw(gameTime, spriteBatch);

            if(!_coinCollected)
            {
                //var rec = new Rectangle((int)_coinCubeRec.X, (int)_coinCubeRec.Y, 10, 10);
                //spriteBatch.Draw(_block, rec, Color.White);
                _coinCube.Draw();
            }
            
            spriteBatch.End();
        }
    }
}
