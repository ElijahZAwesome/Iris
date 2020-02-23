using Iris.Graphics;
using SdlSharp.Graphics;

namespace Iris.TestApp
{
    public class MyGame : Game
    {
        private int HorizDirection = 1;
        private int VertDirection = 1;

        private int X { get; set; } = 100;
        private int Y { get; set; } = 100;

        protected override void Initialize(GraphicsSettings settings)
        {
            settings.WindowWidth = 800;
            settings.WindowHeight = 600;
            settings.ClearColor = Colors.Black;
            settings.EnableVerticalSync = true;
        }

        protected override void LoadContent()
        {
        }

        protected override void Draw(RenderContext context)
        {
            context.FillRectangle(X, Y, 32, 32, new Color(0, 255, 0));
        }

        protected override void Update(double deltaTime)
        {
            Window.Title = $"FPS: {FpsCounter.AverageFps:F2}";

            if (X - 1 <= 0)
                HorizDirection = 1;
            else if (X + 32 >= Window.Renderer.Viewport.Value.Size.Width)
                HorizDirection = -1;

            if (Y - 1 <= 0)
                VertDirection = 1;
            else if (Y + 32 >= Window.Renderer.Viewport.Value.Size.Height)
                VertDirection = -1;

            X += 1 * HorizDirection;
            Y += 1 * VertDirection;

        }
    }
}