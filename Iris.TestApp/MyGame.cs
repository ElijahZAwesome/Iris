using Iris.Graphics;

namespace Iris.TestApp
{
    public class MyGame : Game
    {
        private Font _font;

        protected override void Initialize()
        {
            GraphicsSettings.BackBufferWidth = 1024;
            GraphicsSettings.BackBufferHeight = 600;
            GraphicsSettings.ColorDepth = 24;
            GraphicsSettings.EnableVerticalSync = true;

            GraphicsSettings.CommitChanges();
        }

        protected override void LoadContent()
        {
            _font = Content.Load<Font>("c64style.ttf");
        }

        protected override void Draw(RenderContext context)
        {
            context.Clear(Color.Black);
            context.DrawString(_font, "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890", new Vector2(10, 10), Color.White);
        }

        protected override void Update(float deltaTime)
        {
        }
    }
}