using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameProject0
{
    public class AlligatorSprite
    {
        private Vector2 _position;

        private Vector2 _velocity;

        private Texture2D _texture;

        private bool _flipped = true;

        private double _timer;

        public AlligatorSprite(Vector2 position, Vector2 velocity)
        {
            this._position = position;
            this._velocity = velocity;
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Alligator");
        }

        public void Update(GameTime gameTime)
        {
            _timer += gameTime.ElapsedGameTime.TotalSeconds;

            _position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > 5.0)
            {
                _velocity *= -1;
                _timer -= 5.0;
            }

            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = (_flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(_texture, _position, null, Color.White, 0, new Vector2(0, 0), 1, spriteEffects, 0);
        }
    }
}
