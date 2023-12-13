using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject0.StateManagement;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject0.SpriteClasses
{
    public class LevelThreeBackground
    {
        private Vector2 _position;

        private Texture2D _texture;

        public LevelThreeBackground(Vector2 position)
        {
            _position = position;
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("LevelThreeBackground");
        }

        public void Draw(GameTime game, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, null, Color.White, 0f, new Vector2(0, 0), scale: 0.4f, SpriteEffects.None, 0f);
        }
    }
}
