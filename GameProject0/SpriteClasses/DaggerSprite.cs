using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameProject0.Collisions;

namespace GameProject0.SpriteClasses
{
    public enum DirectionDagger
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
    }

    public class DaggerSprite
    {
        private Vector2 _position;

        private Vector2 _velocity;

        private Texture2D _texture;

        private Texture2D _textureRight;

        private Texture2D _textureLeft;

        private Texture2D _textureDown;

        public DirectionDagger Direction = DirectionDagger.Up;

        private BoundingRectangle _bounds;

        public BoundingRectangle Bounds => _bounds;

        private float _rotation;

        public bool IsActive;

        private float _time = 0f;

        public DaggerSprite(Vector2 position, Vector2 velocity, bool isActive)
        {
            _position = position;
            _velocity = velocity;
            _bounds = new BoundingRectangle(_position + new Vector2(45, 45), 30, 120);
            IsActive = isActive;
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("FallingDagger");
            _textureRight = content.Load<Texture2D>("FallingDaggerRight");
            _textureLeft = content.Load<Texture2D>("FallingDaggerLeft");
            _textureDown = content.Load<Texture2D>("FallingDaggerDown");
        }
        
        public void Update(GameTime gameTime)
        {
            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_time > 0.05)
            {
                switch (Direction)
                {
                    case DirectionDagger.Right:
                        Direction = DirectionDagger.Down;
                        break;
                    case DirectionDagger.Down:
                        Direction = DirectionDagger.Left;
                        break;
                    case DirectionDagger.Left:
                        Direction = DirectionDagger.Up;
                        break;
                    case DirectionDagger.Up:
                        Direction = DirectionDagger.Right;
                        break;
                }
                _time -= 0.05f;
            }

            _position += _velocity;

            _bounds.X = _position.X;
            _bounds.Y = _position.Y;
        }

        public void Draw(GameTime game, SpriteBatch spriteBatch)
        {
            if (Direction == DirectionDagger.Right)
            {
                spriteBatch.Draw(_textureRight, _position, Color.White);
            }
            else if (Direction == DirectionDagger.Down)
            {
                spriteBatch.Draw(_textureDown, _position, Color.White);
            }
            else if (Direction == DirectionDagger.Left)
            {
                spriteBatch.Draw(_textureLeft, _position, Color.White);
            }
            else
            {
                spriteBatch.Draw(_texture, _position, Color.White);
            }
            //spriteBatch.Draw(_texture, _position, null, Color.White, 0f, new Vector2(0,0), scale: 1f, SpriteEffects.None, 0f);
        }
    }
}
