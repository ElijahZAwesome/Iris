using SfmlShader = SFML.Graphics.Shader;

namespace Iris.Graphics
{
    public class PixelShader
    {
        internal SfmlShader SfmlShader { get; }

        public PixelShader(string filePath)
        {
            SfmlShader = new SfmlShader(null, null, filePath);
        }

        public void SetUniform(string name, Vector2 value)
            => SfmlShader.SetUniform(name, value.ToGlslVector());

        public void SetUniform(string name, Vector3 value)
            => SfmlShader.SetUniform(name, value.ToGlslVector());

        public void SetUniform(string name, Vector4 value)
            => SfmlShader.SetUniform(name, value.ToGlslVector());

        public void SetUniform(string name, Color value)
            => SetUniform(name, value.ToVector4());

        public void SetUniform(string name, bool value)
            => SfmlShader.SetUniform(name, value);

        public void SetUniform(string name, float value)
            => SfmlShader.SetUniform(name, value);
    }
}
