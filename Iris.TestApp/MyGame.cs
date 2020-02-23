using Iris.Graphics;
using SdlSharp.Graphics;

namespace Iris.TestApp
{
    public class MyGame : Game
    {
        private Sprite _sprite;
        private Sprite _sprite2;
        private Sprite _sprite3;

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
            _sprite = new Sprite("E:\\dvd.png") { Color = Colors.Lime, Scale = 0.5f, ScalingMode = SpriteScalingMode.NearestNeighbor };
            _sprite2 = new Sprite("E:\\dvd.png") { Color = Colors.Lime, Scale = 0.5f, ScalingMode = SpriteScalingMode.Linear };
            _sprite3 = new Sprite("E:\\dvd.png") { Color = Colors.Lime, Scale = 0.5f, ScalingMode = SpriteScalingMode.Anisotropic };
        }

        protected override void Draw(RenderContext context)
        {
            //context.FillRectangle(X, Y, Width, Height, new Color(0, 255, 0));
            _sprite.Draw(context);
            _sprite2.Draw(context);
            _sprite3.Draw(context);
        }

        protected override void Update(double deltaTime)
        {
            Window.Title = $"FPS: {FpsCounter.AverageFps:F2}";

            if (X - 1 <= 0)
                HorizDirection = 1;
            else if (X + _sprite.ActualWidth >= Window.Renderer.Viewport.Value.Size.Width)
                HorizDirection = -1;

            if (Y - 1 <= 0)
                VertDirection = 1;
            else if (Y + _sprite.ActualHeight >= Window.Renderer.Viewport.Value.Size.Height)
                VertDirection = -1;

            X += 0 * HorizDirection;
            Y += 0 * VertDirection;

            _sprite.X = X;
            _sprite.Y = Y;
            _sprite2.Y = Y;
            _sprite3.Y = Y;

            _sprite2.X = _sprite.X + _sprite.ActualWidth + 32;
            _sprite3.X = _sprite2.X + _sprite2.ActualWidth + 32;
        }
    }
}