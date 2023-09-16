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

        private float _rotation;

        public Vector2 Origin;

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
            
            _animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (_animationTimer > 0.1)
            {
                switch (Direction)
                {
                    case Direction.Right:
                        Direction = Direction.Down;
                        break;
                    case Direction.Down:
                        Direction = Direction.Left;
                        break;
                    case Direction.Left:
                        Direction = Direction.Up;
                        break;
                    case Direction.Up:
                        Direction = Direction.Right;
                        break;
                }
                _animationTimer -= 0.1;
            }
            

            _position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * 100;

            if (_position.X < graphics.Viewport.X - 60 || _position.X + 85 > graphics.Viewport.Width)
            {
                _velocity.X *= -1;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(Direction == Direction.Right)
            {
                spriteBatch.Draw(_textureRight, _position, Color.White);
            }
            else if(Direction == Direction.Down)
            {
                spriteBatch.Draw(_textureDown, _position, Color.White);
            }
            else if(Direction == Direction.Left)
            {
                spriteBatch.Draw(_textureLeft, _position, Color.White);
            }
            else
            {
                spriteBatch.Draw(_textureUp, _position, Color.White);
            }
            //spriteBatch.Draw(_textureRight, _position, Color.White);
        }
    }
}
