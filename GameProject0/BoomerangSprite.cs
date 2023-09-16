using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject0
{
    public enum Direction
    {
        Right = 0,
        Down = 1,
        Left = 2,
        Up = 3,
    }

    public class BoomerangSprite
    {
        private Texture2D _textureRight;

        private Texture2D _textureDown;

        private Texture2D _textureLeft;

        private Texture2D _textureUp;

        public Direction Direction;

        private Vector2 _position;

        private Vector2 _velocity;

        private double _animationTimer;

        private short _animationFrame;

        public BoomerangSprite(Vector2 position, Vector2 velocity)
        {
            _position = position;
            _velocity = velocity;
        }

        public void LoadContent(ContentManager content)
        {
            _textureRight = content.Load<Texture2D>("BoomRight");
            _textureDown = content.Load<Texture2D>("BoomDown");
            _textureLeft = content.Load<Texture2D>("BoomLeft");
            _textureUp = content.Load<Texture2D>("BoomUp");
        }

        public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            _position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * 100;

            if (_position.X < graphics.Viewport.X - 60 || _position.X + 85 > graphics.Viewport.Width)
            {
                _velocity.X *= -1;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureRight, _position, Color.White);
        }
    }
}
