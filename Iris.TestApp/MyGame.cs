using Iris.Graphics;
using Iris.Input;
using System;

namespace Iris.TestApp
{
    public class MyGame : Game
    {
        private Texture _texture;
        private Sprite _sprite1;
        private Sprite _sprite2;

        private Font _font;
        private Vector2 _measure;
        private Vector2 _textPos;
        private const string Text = "YOU COULD CALL THIS ART\n     I SUPPOSE...";

        private float _x;
        private float _y;
        private float _offset = 0;
        private float _timer = 0;
        private byte _alpha = 255;

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
            _font.CharacterSize = 32;
            _measure = _font.Measure(Text);
            _textPos = new Vector2(
                (GraphicsSettings.BackBufferWidth / 2) - (_measure.X / 2),
                (GraphicsSettings.BackBufferHeight / 2) - (_measure.Y / 2)
            );
            _texture = new Texture(GraphicsSettings.BackBufferWidth / 2, GraphicsSettings.BackBufferHeight / 2);

            _sprite1 = new Sprite(_texture)
            {
                Scale = new Vector2(2.0f, 2.0f),
            };

            _sprite2 = new Sprite(_texture)
            {
                Scale = new Vector2(2.0f, 2.0f),
            };
        }

        protected override void Draw(RenderContext context)
        {
            context.Clear(Color.Black);

            context.DrawString(_font, Text, _textPos,
                new Color(
                    (byte)(255 * (_offset / GraphicsSettings.BackBufferHeight)),
                    0, 
                    0, 
                    _alpha
                )
            );

            context.Draw(_sprite1);
            context.Draw(_sprite2);
        }

        protected override void Update(float deltaTime)
        {
            if(_timer > 100)
            {
                _alpha = (byte)(_alpha == 255 ? 0 : 255);
                _timer = 0;
            }

            _timer += 80 * deltaTime;

            if (Keyboard.IsKeyDown(KeyCode.Right))
                _x += 8 * deltaTime;
            else if (Keyboard.IsKeyDown(KeyCode.Left))
                _x -= 8 * deltaTime;
            else if (Keyboard.IsKeyDown(KeyCode.Up))
            {
                _y -= 8 * deltaTime;
                _offset += 32 * deltaTime;
            }
            else if (Keyboard.IsKeyDown(KeyCode.Down))
            {
                _y += 8 * deltaTime;
                _offset -= 32 * deltaTime;
            }

            _sprite1.Position = new Vector2(0, (-GraphicsSettings.BackBufferHeight / 2) - _offset);
            _sprite2.Position = new Vector2(0, (GraphicsSettings.BackBufferHeight / 2) + _offset);

            for (var x = 0; x < _texture.Width; x++)
            {
                for (var y = 0; y < _texture.Height; y++)
                {
                    var noise = (float)Math.Abs(MathF.SimplexNoise(_x + x, _y));
                    noise = MathF.Clamp(noise, 0f, 1f);

                    _sprite1.Texture.SetPixel(
                        (uint)x,
                        (uint)y,
                        new Color(
                            (byte)(255 * noise),
                            (byte)(0 * noise),
                            (byte)(0 * noise),
                            255
                        )
                    );
                }
            }

            _sprite1.UpdateTexture();
            _sprite2.UpdateTexture();
        }
    }
}