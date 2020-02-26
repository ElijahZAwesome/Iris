using Iris.Graphics;
using Iris.Input;

namespace Iris.TestApp
{
    public class MyGame : Game
    {
        private OffscreenBuffer _offbuf;
        private PixelShader _shader;
        private Spritesheet _spritesheet;
        private Font _font;

        private string _text;

        protected override void Initialize()
        {
            GraphicsSettings.BackBufferWidth = 1366;
            GraphicsSettings.BackBufferHeight = 768;
            GraphicsSettings.EnableVerticalSync = true;

            GraphicsSettings.CommitChanges();
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

            _font = Content.Load<Font>("c64style.ttf");
            _font.CharacterSize = 16;
        }

        protected override void Draw(RenderContext context)
        {
            context.UseOffscreenBuffer(_offbuf);
            context.Clear(Color.CornflowerBlue);

            /*for (var i = 0; i < _spritesheet.CellCount; i++)
            {
                context.Draw(_spritesheet, i, _spritesheet.GetGranularXY(i) * _x, new Vector2(4, 4), Color.White);
            }*/

            context.DrawString(_font, _text, new Vector2(64, 64), .0f, Color.Black);

            context.UseOffscreenBuffer(null);

            context.UsePixelShader(_shader);
            context.Draw(_offbuf);
            context.UsePixelShader(null);
        }

        protected override void KeyPressed(KeyCode keyCode, KeyModifiers modifiers)
        {
            if (keyCode == KeyCode.Enter)
                _text += "\n";
        }

        protected override void TextInput(char character)
        {
            if(char.IsLetterOrDigit(character) || char.IsSymbol(character) || char.IsPunctuation(character))
                _text += character;
        }

        protected override void Update(float deltaTime)
        {
            WindowProperties.Title = $"FPS: {FpsCounter.FramesPerSecond:F2} | Delta {deltaTime:F6}";
        }
    }
}