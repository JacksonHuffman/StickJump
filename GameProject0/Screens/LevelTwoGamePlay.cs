using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameProject0.StateManagement;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameProject0.ParticleSystemFolder;
using System;
using GameProject0.SpriteClasses;
using Microsoft.Xna.Framework.Audio;
using GameProject0.Collisions;

namespace GameProject0.Screens
{
    public class LevelTwoGamePlay : GameScreen
    {
        ContentManager _content;

        private PlatformSprite _platformSpriteOne;

        private PlatformSprite _platformSpriteTwo;

        private PlatformSprite _platformSpriteThree;

        private DoorSprite _doorSprite;

        private StickSprite _stickSprite;

        private LavaSprite _lavaSprite;

        private LevelTwoBackground _twoBackground;

        private SpriteFont _bangers;

        private BoundingRectangle _coinCubeRec = new BoundingRectangle(new Vector2(390, 20), 10, 10);

        private ExplosionParticleSystem _explosion;

        private Game _game;

        private double _platOneCount = 3.0;

        private double _platTwoCount = 2.0;

        private double _platThreeCount = 1.5;

        private bool _collideOne = false;

        private bool _collideTwo = false;

        private bool _collideThree = false;

        private bool _displayPlatOneTime = true;

        private bool _displayPlatTwoTime = true;

        private bool _displayPlatThreeTime = true;

        private int _currentPlatform = 1;

        private bool _startGame = false;

        private KeyboardState currentKeyboardState;

        private KeyboardState priorKeyboardState;

        CoinCube _coinCube;

        private Song _backgroundMusic;

        private SoundEffect _explosionSound;

        private int _lives;

        private bool _coinCollected = false;

        private SoundEffect _coinCollect;

        private int _coinCount;

        public LevelTwoGamePlay(Game game, int lives, int coinCount)
        {
            _game = game;
            _lives = lives;
            _coinCount = coinCount;
        }

        public override void Activate()
        {
            base.Activate();

            _lavaSprite = new LavaSprite(new Vector2(0, 400));
            _platformSpriteOne = new PlatformSprite(new Vector2(50, 250), Vector2.Zero);
            _platformSpriteTwo = new PlatformSprite(new Vector2(315, 175), Vector2.Zero);
            _platformSpriteThree = new PlatformSprite(new Vector2(565, 95), Vector2.Zero);
            _doorSprite = new DoorSprite(new Vector2(635, 20));
            _stickSprite = new StickSprite(new Vector2(50, 0), 2);
            _explosion = new ExplosionParticleSystem(_game, 1);
            _twoBackground = new LevelTwoBackground(new Vector2(0, 0));

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _lavaSprite.LoadContent(_content);
            _platformSpriteOne.LoadContent(_content);
            _platformSpriteTwo.LoadContent(_content);
            _platformSpriteThree.LoadContent(_content);
            _doorSprite.LoadContent(_content);
            _stickSprite.LoadContent(_content);
            _twoBackground.LoadContent(_content);
            _bangers = _content.Load<SpriteFont>("bangers");
            _explosionSound = _content.Load<SoundEffect>("ExplosionSound");
            _backgroundMusic = _content.Load<Song>("HoliznaCC0 - Sky Fish");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_backgroundMusic);
            _game.Components.Add(_explosion);
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
                if (_currentPlatform == 1)
                {
                    _stickSprite.Update(gameTime);

                    if (!_platformSpriteOne.Bounds.CollidesWith(_stickSprite.Bounds))
                    {
                        _stickSprite.FallUpdate(gameTime);
                    }
                    else
                    {
                        _stickSprite.AllowedUpdate(gameTime);
                        _collideOne = true;
                    }

                    if (_collideOne)
                    {
                        _platOneCount -= gameTime.ElapsedGameTime.TotalSeconds;

                        if (_platOneCount <= 0.0 || _platformSpriteTwo.Bounds.CollidesWith(_stickSprite.Bounds))
                        {
                            _displayPlatOneTime = false;
                            _explosion.PlaceExplosion(_platformSpriteOne.Position + new Vector2(50, 0));
                            _explosionSound.Play();
                            _stickSprite.FallUpdate(gameTime);
                            _currentPlatform = 2;
                        }
                    }
                }
                else if (_currentPlatform == 2)
                {
                    _stickSprite.Update(gameTime);

                    if (!_platformSpriteTwo.Bounds.CollidesWith(_stickSprite.Bounds))
                    {
                        _stickSprite.FallUpdate(gameTime);
                    }
                    else
                    {
                        _stickSprite.AllowedUpdate(gameTime);
                        _collideTwo = true;
                    }

                    if (_collideTwo)
                    {
                        _platTwoCount -= gameTime.ElapsedGameTime.TotalSeconds;

                        if (_platTwoCount <= 0.0)
                        {
                            _displayPlatTwoTime = false;
                            _explosion.PlaceExplosion(_platformSpriteTwo.Position + new Vector2(50, 0));
                            _explosionSound.Play();
                            _stickSprite.FallUpdate(gameTime);
                            _currentPlatform = 3;
                        }
                    }

                    if(_stickSprite.Bounds.CollidesWith(_coinCubeRec) && !_coinCollected)
                    {
                        _coinCollected = true;
                        _coinCount++;
                        _coinCollect.Play();

                    }
                }
                else if (_currentPlatform == 3)
                {
                    _stickSprite.Update(gameTime);

                    if (!_platformSpriteThree.Bounds.CollidesWith(_stickSprite.Bounds))
                    {
                        _stickSprite.FallUpdate(gameTime);
                    }
                    else
                    {
                        _stickSprite.AllowedUpdate(gameTime);
                        _collideThree = true;
                    }

                    if (_collideThree)
                    {
                        _platThreeCount -= gameTime.ElapsedGameTime.TotalSeconds;

                        if (_platThreeCount <= 0.0)
                        {
                            _displayPlatThreeTime = false;
                            _explosion.PlaceExplosion(_platformSpriteThree.Position + new Vector2(50, 0));
                            _explosionSound.Play();
                            _stickSprite.FallUpdate(gameTime);
                        }
                    }
                }

                if (_lavaSprite.Bounds.CollidesWith(_stickSprite.Bounds))
                {
                    if (_lives > 1)
                    {
                        _lives -= 1;
                        ScreenManager.AddScreen(new LevelTwoTransition(_game, _lives, _coinCount), null);
                    }
                    else
                    {
                        ScreenManager.AddScreen(new MainMenuScreen(_game, _lives + 1), null);
                    }
                    //ScreenManager.AddScreen(new GameOverScreen2(), null);
                    MediaPlayer.Stop();
                    ExitScreen();
                }

                if (_doorSprite.Bounds.CollidesWith((_stickSprite.Bounds)))
                {
                    ScreenManager.AddScreen(new LevelThreeTransition(_game, _lives, _coinCount), null);
                    MediaPlayer.Stop();
                    ExitScreen();
                }
            }

            if (currentKeyboardState.IsKeyDown(Keys.Enter) && priorKeyboardState.IsKeyUp(Keys.Enter))
            {
                _startGame = true;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            //_twoBackground.Draw(gameTime, spriteBatch);
            _lavaSprite.Draw(gameTime, spriteBatch);
            _doorSprite.Draw(gameTime, spriteBatch);
            _stickSprite.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(_bangers, "StickJump! - LvL 2", new Vector2(20, 20), Color.Orange);
            spriteBatch.DrawString(_bangers, "To Exit: Press The ESC Key", new Vector2(510, 350), Color.Red);

            if(!_startGame)
            {
                spriteBatch.DrawString(_bangers, "Press ENTER to start!", new Vector2(250, 130), Color.Purple);
            }

            if (_displayPlatOneTime)
            {
                _platformSpriteOne.Draw(gameTime, spriteBatch);
                _platOneCount = Math.Round(_platOneCount, 2);
                spriteBatch.DrawString(_bangers, _platOneCount.ToString(), new Vector2(60, 255), Color.OrangeRed);
            }

            if(_displayPlatTwoTime)
            {
                _platformSpriteTwo.Draw(gameTime, spriteBatch);
                _platTwoCount = Math.Round(_platTwoCount, 2);
                spriteBatch.DrawString(_bangers, _platTwoCount.ToString(), new Vector2(325, 180), Color.OrangeRed);
            }

            if(_displayPlatThreeTime)
            {
                _platformSpriteThree.Draw(gameTime, spriteBatch);
                _platThreeCount = Math.Round(_platThreeCount, 2);
                spriteBatch.DrawString(_bangers, _platThreeCount.ToString(), new Vector2(575, 100), Color.OrangeRed);
            }

            if (!_coinCollected)
            {
                _coinCube.Draw();
            }
            spriteBatch.End();
        }
    }
}
