using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject0.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject0.SpriteClasses
{
    public class StickSprite
    {
        private Vector2 _position;

        private Texture2D _texture;

        private Vector2 _velocity = new Vector2(0, 0);

        private BoundingRectangle _bounds;

        private SoundEffect _stickJump;

        private int _level;

        public BoundingRectangle Bounds => _bounds;

        private KeyboardState currentKeyboardState;
        private KeyboardState priorKeyboardState;

        public StickSprite(Vector2 position, int level)
        {
            _position = position;
            _bounds = new BoundingRectangle(_position, 50, 100);
            _level = level;
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Character");
            _stickJump = content.Load<SoundEffect>("StickJump");
        }

        public void Update(GameTime gameTime)
        {
            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
            {
                _position += new Vector2(-5, 0);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
            {
                _position += new Vector2(5, 0);
            }

            if(1 == _level)
            {
                if (currentKeyboardState.IsKeyDown(Keys.Space) && priorKeyboardState.IsKeyUp(Keys.Space) || currentKeyboardState.IsKeyDown(Keys.Up) && priorKeyboardState.IsKeyUp(Keys.Up))
                {
                    _position += new Vector2(0, -100);
                    _stickJump.Play();
                }
            }
           
            _bounds.X = _position.X;
            _bounds.Y = _position.Y;
        }

        public void AllowedUpdate(GameTime gameTime)
        {
            if (currentKeyboardState.IsKeyDown(Keys.Space) && priorKeyboardState.IsKeyUp(Keys.Space) || currentKeyboardState.IsKeyDown(Keys.Up) && priorKeyboardState.IsKeyUp(Keys.Up))
            {
                _position += new Vector2(0, -130);
            }
        }

        public void FallUpdate(GameTime gameTime)
        {
            if(1 == _level)
            {
                float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
                Vector2 acceleration = new Vector2(0, 500);

                _velocity += acceleration * t;
                _position += acceleration * t;
            }
            else if (2 == _level)
            {
                float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
                Vector2 acceleration = new Vector2(0, 50);

                _velocity += acceleration * t;
                _position += acceleration * t;
            }
            

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.Purple);
        }
    }
}
