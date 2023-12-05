using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameProject0.StateManagement;

namespace GameProject0.Screens
{
    public class MoreImplementation : GameScreen
    {
        ContentManager _content;
        SpriteFont _varelaRound;

        public override void Activate()
        {
            base.Activate();

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _varelaRound = _content.Load<SpriteFont>("VarelaRound");
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.DrawString(_varelaRound, "More Implementation to Come!", new Vector2(50, 200), Color.Red);
            ScreenManager.SpriteBatch.End();
        }
    }
}