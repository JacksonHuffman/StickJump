using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameProject0.Collisions;
using GameProject0.Screens;
using GameProject0.StateManagement;
using GameProject0.ParticleSystemFolder;

namespace GameProject0
{
    public class StickJumpGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private KeyboardState currentKeyboardState;
        private KeyboardState priorKeyboardState;

        private readonly ScreenManager _screenManager;

        public int Lives = 2;

        public StickJumpGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            var screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);

            AddInitialScreens();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        public void AddInitialScreens()
        {
            //_screenManager.AddScreen(new MainMenuBackgroundScreen(), null);
            _screenManager.AddScreen(new MainMenuScreen(this, Lives), null);
            //_screenManager.AddScreen(new WinnerScreen(this, Lives, 0), null);
            //_screenManager.AddScreen(new LevelThreeTransition(this, Lives), null);
            //_screenManager.AddScreen(new LevelTwoGamePlay(this), null);
            //_screenManager.AddScreen(new LevelThreeTransition(this), null);
            //_screenManager.AddScreen(new ControlsAndObjectives(this, Lives), null);
        }

        protected override void LoadContent()
        {
            //_spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            
            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            /*
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            */

            if (currentKeyboardState.IsKeyDown(Keys.Escape) &&
                priorKeyboardState.IsKeyUp(Keys.Escape))
            {
                this.Exit();
            }
            
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}