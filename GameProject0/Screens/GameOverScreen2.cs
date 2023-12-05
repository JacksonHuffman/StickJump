using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameProject0.StateManagement;

namespace GameProject0.Screens
{
    public class GameOverScreen2 : GameScreen
    {
        ContentManager _content;
        Texture2D _background;

        public override void Activate()
        {
            base.Activate();

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _background = _content.Load<Texture2D>("GameOverScreen2");
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.Draw(_background, new Vector2(100, 100), Color.White);
            ScreenManager.SpriteBatch.End();
        }
    }
}
