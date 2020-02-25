using System.Collections.Generic;
using Iris.Graphics;

namespace Iris.TestApp
{
    public class MyGame : Game
    {
        private int _horizDirection = 1;
        private int _vertDirection = 1;

        private float X { get; set; } = 100;
        private float Y { get; set; } = 100;

        private int _spriteCount = 100;
        private readonly List<Sprite> _sprites = new List<Sprite>();

        private PixelShader _shader;

        protected override void Initialize()
        {
            GraphicsSettings.BackBufferWidth = 800;
            GraphicsSettings.BackBufferHeight = 600;
            GraphicsSettings.ClearColor = Color.CornflowerBlue;
            GraphicsSettings.FramerateLimit = 500;
            GraphicsSettings.EnableVerticalSync = true;
        }

        protected override void LoadContent()
        {
            _shader = Content.Load<PixelShader>("shader.glsl");
            
            for (var i = 0; i < _spriteCount; i++)
            {
                _sprites.Add(Content.Load<Sprite>("wot2.png"));
            }
        }

        protected override void Draw(RenderContext context)
        {
            context.UseShader(_shader);
            context.Clear(Color.Azure);
            
            foreach(var sprite in _sprites)
            {
                context.Draw(sprite);
            }
        }

        protected override void Update(float deltaTime)
        {
            WindowProperties.Title = $"FPS: {FpsCounter.FramesPerSecond:F2} | Delta {deltaTime:F6} | {_spriteCount} Sprites";

            foreach (var sprite in _sprites)
            {
                if (X - 1 <= 0)
                    _horizDirection = 1;
                else if (X + sprite.ActualWidth >= GraphicsSettings.BackBufferWidth)
                    _horizDirection = -1;

                if (Y - 1 <= 0)
                    _vertDirection = 1;
                else if (Y + sprite.ActualHeight >= GraphicsSettings.BackBufferHeight)
                    _vertDirection = -1;

                X += 2 * _horizDirection * deltaTime;
                Y += 2 * _vertDirection * deltaTime;

                sprite.Position = new Vector2(X, Y);
            }
        }
    }
}