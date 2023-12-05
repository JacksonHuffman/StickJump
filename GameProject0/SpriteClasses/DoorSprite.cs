﻿using System;
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
    public class DoorSprite
    {
        private Vector2 _position;

        private Texture2D _texture;

        private BoundingRectangle _bounds;

        public BoundingRectangle Bounds => _bounds;

        public DoorSprite(Vector2 position)
        {
            _position = position;
            _bounds = new BoundingRectangle(_position, 50, 64);
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Door");
        }

        public void Draw(GameTime game, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
