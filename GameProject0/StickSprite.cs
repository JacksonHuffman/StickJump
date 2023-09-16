using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject0.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject0
{
    public class StickSprite
    {
        private Vector2 _position;

        private Texture2D _texture;

        private Vector2 _velocity = new Vector2(0,0);

        private BoundingRectangle _bounds;

        public BoundingRectangle Bounds => _bounds;

        private KeyboardState currentKeyboardState;
        private KeyboardState priorKeyboardState;

        public StickSprite(Vector2 position)
        {
            this._position = position;
            this._bounds = new BoundingRectangle(_position, 91, 125);
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Character");
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

            if ((currentKeyboardState.IsKeyDown(Keys.Space) && priorKeyboardState.IsKeyUp(Keys.Space)) || (currentKeyboardState.IsKeyDown(Keys.Up) && priorKeyboardState.IsKeyUp(Keys.Up)))
            {
                _position += new Vector2(0, -100);
            }

            _bounds.X = _position.X;
            _bounds.Y = _position.Y;
        }

        public void FallUpdate(GameTime gameTime)
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 acceleration = new Vector2(0, 500);

            _velocity += acceleration * t;
            _position += _velocity * t;
           
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
