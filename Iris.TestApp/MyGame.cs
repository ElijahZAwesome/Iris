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

        private int Velocity { get; set; } = 4;

        private Sprite Sprite { get; set; }

        protected override void Initialize(GraphicsSettings settings)
        {
            settings.WindowWidth = 1024;
            settings.WindowHeight = 600;
            settings.ClearColor = Colors.CornflowerBlue;
            settings.EnableVerticalSync = true;
        }

        protected override void LoadContent()
        {
            Sprite = Content.Load<Sprite>("wot2.png");
            Sprite.Scale = 0.4f;
        }

        protected override void Draw(RenderContext context)
        {
            context.Draw(Sprite);
        }

        protected override void Update(double deltaTime)
        {
            Window.Title = $"FPS: {FpsCounter.AverageFps:F2}";

            if (X - 1 <= 0)
                HorizDirection = 1;
            else if (X + Sprite.ActualWidth >= Window.Renderer.Viewport.Value.Size.Width)
                HorizDirection = -1;

            if (Y - 1 <= 0)
                VertDirection = 1;
            else if (Y + Sprite.ActualHeight >= Window.Renderer.Viewport.Value.Size.Height)
                VertDirection = -1;

            X += Velocity * HorizDirection;
            Y += Velocity * VertDirection;

            Sprite.X = X;
            Sprite.Y = Y;
        }
    }
}