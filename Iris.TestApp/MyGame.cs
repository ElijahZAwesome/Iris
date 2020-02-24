using Iris.Graphics;

namespace Iris.TestApp
{
    public class MyGame : Game
    {
        private int _horizDirection = 1;
        private int _vertDirection = 1;

        private float X { get; set; } = 100;
        private float Y { get; set; } = 100;

        private Sprite _sprite;

        protected override void Initialize(GraphicsSettings settings)
        {
            settings.WindowWidth = 1024;
            settings.WindowHeight = 600;
            settings.ClearColor = Color.CornflowerBlue;
            settings.FramerateLimit = 0;
            settings.EnableVerticalSync = true;
        }

        protected override void LoadContent()
        {
            _sprite = Content.Load<Sprite>("wot2.png");
        }

        protected override void Draw(RenderContext context)
        {
            context.Draw(_sprite);
        }

        protected override void Update(float deltaTime)
        {
            Window.SetTitle($"FPS: {FpsCounter.FramesPerSecond:F2} | Delta {deltaTime:F6}");

            if (X - 1 <= 0)
                _horizDirection = 1;
            else if (X + _sprite.ActualWidth >= Window.Size.X)
                _horizDirection = -1;

            if (Y - 1 <= 0)
                _vertDirection = 1;
            else if (Y + _sprite.ActualHeight >= Window.Size.Y)
                _vertDirection = -1;

            X += 140 * _horizDirection * deltaTime;
            Y += 140 * _vertDirection * deltaTime;
            
            _sprite.Position = new Vector2(X, Y);
        }
    }
}