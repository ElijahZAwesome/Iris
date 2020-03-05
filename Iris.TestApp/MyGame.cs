using Iris.Graphics;

namespace Iris.TestApp
{
    public class MyGame : Game
    {
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
        }

        protected override void Update(float deltaTime)
        {

        }
    }
}