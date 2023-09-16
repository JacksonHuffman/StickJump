using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameProject0.Collisions;

namespace GameProject0
{
    public class StickJumpGame : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private PlatformSprite _platformSprite;

        private StickSprite _stickSprite;

        private BoomerangSprite _boomerangSprite;

        private Texture2D _blockTexture;
        private Vector2 _blockPosition;

        private Texture2D _fireTexture;
        private Vector2 _firePosition;

        private SpriteFont _bangers;

        private KeyboardState currentKeyboardState;
        private KeyboardState priorKeyboardState;

        public StickJumpGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _platformSprite = new PlatformSprite(new Vector2((GraphicsDevice.Viewport.Width - 128) / 2, (GraphicsDevice.Viewport.Height - 128) / 2), new Vector2((float)1, 0));
            _boomerangSprite = new BoomerangSprite(new Vector2(5, (GraphicsDevice.Viewport.Height / 2) - 190), new Vector2((float)1,0));
            _firePosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 128, (GraphicsDevice.Viewport.Height / 2) + 32);
            _stickSprite = new StickSprite(new Vector2((GraphicsDevice.Viewport.Width - 64) / 2, (GraphicsDevice.Viewport.Height - 335) / 2));
            _blockPosition = new Vector2(20, 275);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //_platformTexture = Content.Load<Texture2D>("Platform");
            _fireTexture = Content.Load<Texture2D>("Fire");
            _stickSprite.LoadContent(Content);
            _platformSprite.LoadContent(Content);
            _boomerangSprite.LoadContent(Content);
            _bangers = Content.Load<SpriteFont>("bangers");
            _blockTexture = Content.Load<Texture2D>("Block");
        }

        protected override void Update(GameTime gameTime)
        {
            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _stickSprite.Update(gameTime);
            _platformSprite.Update(gameTime, GraphicsDevice);
            _boomerangSprite.Update(gameTime, GraphicsDevice);
            if(!_platformSprite.Bounds.CollidesWith(_stickSprite.Bounds))
            {
                _stickSprite.FallUpdate(gameTime);
            }

            if (currentKeyboardState.IsKeyDown(Keys.H) &&
                priorKeyboardState.IsKeyUp(Keys.H))
            {
                this.Exit();
            }
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _platformSprite.Draw(gameTime, _spriteBatch);
            _boomerangSprite.Draw(gameTime, _spriteBatch);
            _spriteBatch.Draw(_fireTexture, _firePosition, Color.White);
            _stickSprite.Draw(gameTime, _spriteBatch);
            _spriteBatch.DrawString(_bangers, "StickJump!", new Vector2(20, 20), Color.Orange);
            _spriteBatch.DrawString(_bangers, "To Exit: Press The H Key!", new Vector2(520, 20), Color.Red);
            _spriteBatch.Draw(_blockTexture, _blockPosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}