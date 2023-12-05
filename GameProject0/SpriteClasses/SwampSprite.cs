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
    public class SwampSprite
    {
        private Vector2 _position;

        private Texture2D _texture;

        private BoundingRectangle _bounds;

        public BoundingRectangle Bounds => _bounds;

        public SwampSprite(Vector2 position)
        {
            _position = position;
            _bounds = new BoundingRectangle(_position, 1245, 175);
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Swamp");
        }

        public void Draw(GameTime game, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
