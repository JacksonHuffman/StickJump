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
    public class PlatformSprite
    {

        private Vector2 _position;

        private Vector2 _velocity;

        private Texture2D _texture;

        private BoundingRectangle _bounds;

        public BoundingRectangle Bounds => _bounds;

        private KeyboardState currentKeyboardState;
        private KeyboardState priorKeyboardState;

        public PlatformSprite(Vector2 position, Vector2 velocity)
        {
            this._position = position;
            _velocity = velocity;
            this._bounds = new BoundingRectangle(_position, 133, 35);
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Platform");
        }

        public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            _position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * 100;

            if (_position.X < graphics.Viewport.X || _position.X + 128 > graphics.Viewport.Width)
            {
                _velocity.X *= -1;
            }
            _bounds.X = _position.X;
            _bounds.Y = _position.Y;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
