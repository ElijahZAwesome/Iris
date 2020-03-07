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

            context.FillRectangle(
                new Vector2(60, 60),
                new Vector2(120, 120),
                Color.Red
            );

            context.DrawRectangle(
                new Vector2(60, 60),
                new Vector2(120, 120),
                Color.White, 2f
            );

            _font.CharacterSize = 32;
            context.DrawString(
                _font,
                "this is a test string",
                new Vector2(60, 60),
                Color.HotPink
            );
            var bounds = _font.Measure("this is a test string");

            _font.CharacterSize = 16;
            context.DrawString(
                _font,
                "LOREM IPSUM dolor",
                new Vector2(120, 120),
                Color.HotPink
            );
            var bounds2 = _font.Measure("LOREM IPSUM dolor");

            context.DrawRectangle(new Vector2(60 + bounds.Left, 60 + bounds.Top), new Vector2(bounds.Width, bounds.Height), Color.Green);
            context.DrawRectangle(new Vector2(120 + bounds2.Left, 120 + bounds2.Top), new Vector2(bounds2.Width, bounds2.Height), Color.Cyan);
        }

        protected override void Update(float deltaTime)
        {

        }
    }
}