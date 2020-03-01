using Iris.Graphics;
using Iris.Input;
using System;

namespace Iris.TestApp
{
    public class MyGame : Game
    {
        private Texture _texture;
        private Sprite _sprite;

        private float _x;
        private float _y;

        protected override void Initialize()
        {
            GraphicsSettings.BackBufferWidth = 1024;
            GraphicsSettings.BackBufferHeight = 600;
            GraphicsSettings.ColorDepth = 24;
            GraphicsSettings.EnableVerticalSync = true;

            GraphicsSettings.CommitChanges();
            MathF.SimplexSeed(12523187562);
        }

        protected override void LoadContent()
        {
            _texture = new Texture(GraphicsSettings.BackBufferWidth / 8, GraphicsSettings.BackBufferHeight / 8);
            _sprite = new Sprite(_texture)
            {
                Scale = new Vector2(8.0f, 8.0f)
            };
        }

        protected override void Draw(RenderContext context)
        {
            context.Clear(Color.CornflowerBlue);
            context.Draw(_sprite);
        }

        protected override void KeyPressed(KeyCode keyCode, KeyModifiers modifiers)
        {
            _sprite.UpdateTexture();
        }

        protected override void Update(float deltaTime)
        {
            if (Keyboard.IsKeyDown(KeyCode.Right))
                _x += 1 * deltaTime;
            else if (Keyboard.IsKeyDown(KeyCode.Left))
                _x -= 1 * deltaTime;
            else if (Keyboard.IsKeyDown(KeyCode.Up))
                _y -= 1 * deltaTime;
            else if (Keyboard.IsKeyDown(KeyCode.Down))
                _y += 1 * deltaTime;

            for (var x = 0; x < _texture.Width; x++)
            {
                for (var y = 0; y < _texture.Height; y++)
                {
                    var noise = (float)Math.Abs(MathF.SimplexNoise(_x + x, _y + y));
                    noise = MathF.Clamp(noise, 0f, 1f);

                    _sprite.Texture.SetPixel(
                        (uint)x,
                        (uint)y,
                        new Color(
                            (byte)(255 * noise),
                            (byte)(255 * noise),
                            (byte)(255 * noise),
                            255
                        )
                    );
                }
            }
        }
    }
}