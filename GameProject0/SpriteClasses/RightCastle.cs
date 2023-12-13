using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject0.SpriteClasses
{
    public class RightCastle
    {
        private Vector2 _position;

        private Texture2D _texture;

        public RightCastle(Vector2 position)
        {
            _position = position;
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("RightCastle");
        }

        public void Draw(GameTime game, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, null, Color.White, 0f, new Vector2(0, 0), scale: 0.4f, SpriteEffects.None, 0f);
        }
    }
}
