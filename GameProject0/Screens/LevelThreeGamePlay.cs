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
using GameProject0.HelperClasses;
using SharpDX.Direct2D1.Effects;
using Microsoft.Xna.Framework.Audio;

namespace GameProject0.Screens
{
    public class LevelThreeGamePlay : GameScreen
    {
        ContentManager _content;

        private LevelThreeBackground _background;

        private LongPlatformSprite _longPlatform;

        private StickSprite _stickSprite;

        private DaggerSprite[] _daggers;

        private PortalSprite _portal;

        private SpriteFont _bangers;

        private KeySprite _key;

        private Texture2D _block;

        private int[] _randomDrops = { 0, 1, 2, 3, 4 };

        private bool _doneDropping = false;

        private bool _revealKey = false;

        private bool _keyCollected = false;

        private bool _startGame = false;

        private bool _strikeShake = false;

        private float _shakeDuration = 0.01f;

        private bool _justOnce = true;

        private Song _backgroundMusic;

        private SoundEffect _keyPickup;

        private bool _jumpActive = false;

        private KeyboardState currentKeyboardState;

        private KeyboardState priorKeyboardState;

        Game _game;

        private int _lives;

        private int _coinCount;

        public LevelThreeGamePlay(Game game, int lives, int coinCount)
        {
            _game = game;
            _lives = lives;
            _coinCount = coinCount;
        }

        public override void Activate()
        {
            base.Activate();

            _background = new LevelThreeBackground(new Vector2(15, 0));
            _longPlatform = new LongPlatformSprite(new Vector2(0, 450));
            _daggers = new DaggerSprite[]
            {
                new DaggerSprite(new Vector2(10, -80), new Vector2(0, RandomHelper.Next(2,5)), true),
                new DaggerSprite(new Vector2(225, -80), new Vector2(0, RandomHelper.Next(2,5)), true),
                new DaggerSprite(new Vector2(390, -80), new Vector2(0, RandomHelper.Next(2,5)), true),
                new DaggerSprite(new Vector2(550, -80), new Vector2(0, RandomHelper.Next(2,5)), true),
                new DaggerSprite(new Vector2(630, -80), new Vector2(0, RandomHelper.Next(2,5)), true),
            };
            _stickSprite = new StickSprite(new Vector2(325, 345), 3);
            _key = new KeySprite(new Vector2(500, 400));
            _portal = new PortalSprite(new Vector2(350, 20));

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _background.LoadContent(_content);
            _longPlatform.LoadContent(_content);
            foreach(var dagger in _daggers) dagger.LoadContent(_content);
            _stickSprite.LoadContent(_content);
            _key.LoadContent(_content);
            _block = _content.Load<Texture2D>("Block");
            _bangers = _content.Load<SpriteFont>("Bangers");
            _portal.LoadContent(_content);
            _keyPickup = _content.Load<SoundEffect>("KeyPickup");

            _backgroundMusic = _content.Load<Song>("Frederic Lardon feat Laura Palmée - Dans le love");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_backgroundMusic);

            for (int i = _randomDrops.Length - 1; i > 0; i--)
            {
                int j = RandomHelper.Next(0, i + 1);
                // Swap _randomDrops[i] and _randomDrops[j]
                int temp = _randomDrops[i];
                _randomDrops[i] = _randomDrops[j];
                _randomDrops[j] = temp;
            }
        }

        public override void Deactivate()
        {
            base.Deactivate();

        }

        public override async void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (_startGame)
            {
                _stickSprite.Update(gameTime);
                if (!_doneDropping)
                {
                    foreach (var drop in _randomDrops)
                    {
                        if (!_longPlatform.Bounds.CollidesWith(_daggers[drop].Bounds) && !_stickSprite.Bounds.CollidesWith(_daggers[drop].Bounds))
                        {
                            _daggers[drop].Update(gameTime);
                        }
                        else
                        {
                            if (_stickSprite.Bounds.CollidesWith(_daggers[drop].Bounds) && _daggers[drop].IsActive)
                            {
                                _daggers[drop].Direction = DirectionDagger.Down;
                                if (_lives > 1)
                                {
                                    _strikeShake = false;
                                    _lives -= 1;
                                    ExitScreen();
                                    MediaPlayer.Stop();
                                    ScreenManager.AddScreen(new LevelThreeTransition(_game, _lives, _coinCount), null);
                                    break;
                                }
                                else
                                {
                                    _strikeShake = false;
                                    ExitScreen();
                                    MediaPlayer.Stop();
                                    ScreenManager.AddScreen(new MainMenuScreen(_game, _lives + 1), null);
                                    
                                }
                            }
                            else if (_longPlatform.Bounds.CollidesWith(_daggers[drop].Bounds))
                            {
                                _daggers[drop].Direction = DirectionDagger.Down;
                                _daggers[drop].IsActive = false;
                                _strikeShake = true;
                            }

                        }
                        await Task.Delay(RandomHelper.Next(500, 4000));
                    }

                    _strikeShake = false;
                    _doneDropping = true;
                }
                else
                {
                    if(_justOnce)
                    {
                        await Task.Delay(4000);

                        _revealKey = true;
                    }
                    
                    _justOnce = false;

                    _stickSprite.AllowedUpdate(gameTime);

                    if (!_longPlatform.Bounds.CollidesWith(_stickSprite.Bounds))
                    {
                        _stickSprite.FallUpdate(gameTime);
                    }

                    if (_stickSprite.Bounds.CollidesWith(_key.Bounds) && !_keyCollected)
                    {
                        _keyCollected = true;
                        _keyPickup.Play();
                        _revealKey = false;
                    }

                    if(_stickSprite.Bounds.CollidesWith(_portal.Bounds) && _keyCollected)
                    {
                        MediaPlayer.Stop();
                        ScreenManager.AddScreen(new WinnerScreen(_game, _lives, _coinCount), null);
                        ExitScreen();
                    }

                    /*
                    if (_stickSprite.Bounds.CollidesWith(_daggers[0].Bounds) && _daggers[0].IsActive)
                    {
                        _daggers[0].Direction = DirectionDagger.Down;
                        _game.Exit();
                    }
                    else if(_stickSprite.Bounds.CollidesWith(_daggers[1].Bounds) && _daggers[1].IsActive)
                    {
                        _daggers[1].Direction = DirectionDagger.Down;
                        _game.Exit();
                    }
                    else if (_stickSprite.Bounds.CollidesWith(_daggers[2].Bounds) && _daggers[2].IsActive)
                    {
                        _daggers[2].Direction = DirectionDagger.Down;
                        _game.Exit();
                    }
                    else if (_stickSprite.Bounds.CollidesWith(_daggers[3].Bounds) && _daggers[3].IsActive)
                    {
                        _daggers[3].Direction = DirectionDagger.Down;
                        _game.Exit();
                    }
                    else if (_stickSprite.Bounds.CollidesWith(_daggers[4].Bounds) && _daggers[4].IsActive)
                    {
                        _daggers[4].Direction = DirectionDagger.Down;
                        _game.Exit();
                    }

                    if(_stickSprite.Bounds.CollidesWith(_key.Bounds))
                    {
                        _keyCollected = true;
                    }
                    */
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

            Matrix shake = Matrix.Identity;
            if (_strikeShake)
            {
                _shakeDuration += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                Matrix translation = Matrix.CreateTranslation(5 * MathF.Sin(_shakeDuration), 5 * MathF.Cos(_shakeDuration), 0);
                shake = translation;
            }

            spriteBatch.Begin(transformMatrix: shake);
            _background.Draw(gameTime, spriteBatch);
            if(_keyCollected)
            {
                _portal.Draw(gameTime, spriteBatch);
            }
            
            spriteBatch.DrawString(_bangers, "StickJump! - LvL 3", new Vector2(20, 20), Color.Orange);
            spriteBatch.DrawString(_bangers, "To Exit: Press The ESC Key", new Vector2(510, 20), Color.Red);

            if(!_startGame)
            {
                spriteBatch.DrawString(_bangers, "Press ENTER to begin!", new Vector2(260, 275), Color.Purple);
            }
            //var recP = new Rectangle((int)_longPlatform.Bounds.X, (int)_longPlatform.Bounds.Y, 971, 40);
            //spriteBatch.Draw(_block, recP, Color.White);
            _longPlatform.Draw(gameTime, spriteBatch);

            //var rec2 = new Rectangle((int)_stickSprite.Bounds.X + 25, (int)_stickSprite.Bounds.Y + 20, 40, 80);
            //spriteBatch.Draw(_block, rec2, Color.White);
            _stickSprite.Draw(gameTime, spriteBatch);
            if(_doneDropping && _revealKey)
            {
                //var rec3 = new Rectangle((int)_key.Bounds.X, (int)_key.Bounds.Y, 54, 31);
                _key.Draw(gameTime, spriteBatch);
                _jumpActive = true;
                //spriteBatch.Draw(_block, rec3, Color.Black);
            }
            
            if(_jumpActive)
            {
                spriteBatch.DrawString(_bangers, "Jump Activated!", new Vector2(285, 275), Color.Blue);
            }

            foreach (var dagger in _daggers)
            {
                //var rec = new Rectangle((int)dagger.Bounds.X + 45, (int)dagger.Bounds.Y + 45, 30, 90);
                //spriteBatch.Draw(_block, rec, Color.Red);
                dagger.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
