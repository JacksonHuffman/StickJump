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
    public class LongPlatformSprite
    {

        public Vector2 _position;

        private Texture2D _texture;

        private BoundingRectangle _bounds;

        public BoundingRectangle Bounds => _bounds;


        public LongPlatformSprite(Vector2 position)
        {
            _position = position;
            _bounds = new BoundingRectangle(_position, 971, 0);
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("LevelFourPlatform");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
