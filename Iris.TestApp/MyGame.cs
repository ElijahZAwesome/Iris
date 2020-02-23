using Iris.Graphics;
using SdlSharp.Graphics;
using SdlSharp.Input;
using Keyboard = Iris.Input.Keyboard;

namespace Iris.TestApp
{
    public class MyGame : Game
    {
        private float X { get; set; } = 100;
        private float Y { get; set; } = 100;
        private int Width { get; } = 32;
        private int Height { get; } = 32;
        
        protected override void Initialize(GraphicsSettings settings)
        {
            settings.WindowWidth = 1024;
            settings.WindowHeight = 600;
            settings.EnableVerticalSync = false;
        }

        protected override void Draw(RenderContext context)
        {
            context.FillRectangle(X, Y, Width, Height, new Color(0, 255, 0));
        }

        protected override void Update(double deltaTime)
        {
            Window.Title = $"FPS: {FpsCounter.AverageFps:F2}";

            if (Keyboard.IsKeyDown(Keycode.Left))
                X -= 0.25f * (float)deltaTime;

            if (Keyboard.IsKeyDown(Keycode.Right))
                X += 0.25f * (float)deltaTime;

            if (Keyboard.IsKeyDown(Keycode.Up))
                Y -= 0.25f * (float)deltaTime;

            if (Keyboard.IsKeyDown(Keycode.Down))
                Y += 0.25f * (float)deltaTime;
        }
    }
}