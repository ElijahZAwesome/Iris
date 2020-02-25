using Iris.Graphics;

namespace Iris.TestApp
{
    public class MyGame : Game
    {
        private OffscreenBuffer _offbuf;
        private PixelShader _shader;
        private Sprite _sprite;
        private Spritesheet _spritesheet;

        protected override void Initialize()
        {
            GraphicsSettings.BackBufferWidth = 1366;
            GraphicsSettings.BackBufferHeight = 768;
            GraphicsSettings.ClearColor = Color.CornflowerBlue;
            GraphicsSettings.FramerateLimit = 500;
            GraphicsSettings.EnableVerticalSync = true;
        }

        protected override void LoadContent()
        {
            _offbuf = new OffscreenBuffer(GraphicsSettings.BackBufferWidth, GraphicsSettings.BackBufferHeight);

            _shader = Content.Load<PixelShader>("shader.glsl");
            _shader.Set("screenSize", new Vector2(GraphicsSettings.BackBufferWidth, GraphicsSettings.BackBufferHeight));
            _shader.Set("scanlineDensity", 2f);
            _shader.Set("blurDistance", .375f);

            _spritesheet = Content.Load<Spritesheet>("terrain.png");
            _spritesheet.CellHeight = 16;
            _spritesheet.CellWidth = 16;
        }

        protected override void Draw(RenderContext context)
        {
            context.UseOffscreenBuffer(_offbuf);
            context.Clear(Color.CornflowerBlue);
            for (var i = 0; i < _spritesheet.CellCount; i++)
            {
                context.Draw(_spritesheet, i, _spritesheet.GetGranularXY(i) * 32, new Vector2(2, 2), Color.White);
            }
            context.UseOffscreenBuffer(null);

            //context.UsePixelShader(_shader);
            context.Draw(_offbuf);
            //context.UsePixelShader(null);
        }

        protected override void Update(float deltaTime)
        {
            WindowProperties.Title = $"FPS: {FpsCounter.FramesPerSecond:F2} | Delta {deltaTime:F6}";
        }
    }
}