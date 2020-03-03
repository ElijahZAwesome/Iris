using Iris.Exceptions;
using SfmlShader = SFML.Graphics.Shader;

namespace Iris.Graphics
{
    public class PixelShader
    {
        internal SfmlShader SfmlShader { get; }

        internal PixelShader(string filePath)
        {
            if (!SfmlShader.IsAvailable)
            {
                throw new ShaderNotSupportedException("Pixel shaders are not supported on this platform.");
            }
            
            SfmlShader = new SfmlShader(null, null, filePath);
        }

        public void Set(string name, Vector2 value)
            => SfmlShader.SetUniform(name, value);

        public void Set(string name, Vector3 value)
            => SfmlShader.SetUniform(name, value);

        public void Set(string name, Vector4 value)
            => SfmlShader.SetUniform(name, value);

        public void Set(string name, Color value)
            => Set(name, value.ToVector4());

        public void Set(string name, bool value)
            => SfmlShader.SetUniform(name, value);

        public void Set(string name, float value)
            => SfmlShader.SetUniform(name, value);

        public void Set(string name, Texture texture)
            => SfmlShader.SetUniform(name, texture.SfmlTexture);
    }
}
