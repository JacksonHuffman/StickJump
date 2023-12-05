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

namespace GameProject0.SpriteClasses
{
    public class PlatformSprite
    {

        public Vector2 Position;

        private Vector2 _velocity;

        private Texture2D _texture;

        private BoundingRectangle _bounds;

        public BoundingRectangle Bounds => _bounds;


        public PlatformSprite(Vector2 position, Vector2 velocity)
        {
            Position = position;
            _velocity = velocity;
            _bounds = new BoundingRectangle(Position, 99, 35);
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Platform");
        }

        public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            Position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * 100;

            if (Position.X < graphics.Viewport.X || Position.X + 128 > graphics.Viewport.Width)
            {
                _velocity.X *= -1;
            }
            _bounds.X = Position.X;
            _bounds.Y = Position.Y;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
