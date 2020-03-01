using Iris.Internal;
using SfmlFont = SFML.Graphics.Font;
using SfmlText = SFML.Graphics.Text;

namespace Iris.Graphics
{
    public class Font
    {
        internal SfmlFont SfmlFont { get; }

        public uint CharacterSize { get; set; } = 12;
        public float CharacterSpacing { get; set; } = 1;
        public float LineSpacing { get; set; }
        public uint OutlineThickness { get; set; }

        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public bool Underline { get; set; }

        public float LineHeight => CharacterSize + LineSpacing;

        internal Font(string filePath)
        {
            SfmlFont = new SfmlFont(filePath);
        }

        public Vector2 Measure(string text)
        {
            var rect = ConstructText(text)
                        .GetLocalBounds()
                        .ToIrisRectangle();

            return new Vector2(rect.Width, rect.Height);
        }

        internal SfmlText ConstructText(string text)
        {
            var txt = new SfmlText(text, SfmlFont, CharacterSize)
            {
                LetterSpacing = CharacterSpacing,
                LineSpacing = LineSpacing,
            };

            if (Bold)
                txt.Style |= SfmlText.Styles.Bold;

            if (Italic)
                txt.Style |= SfmlText.Styles.Italic;

            return txt;
        }
    }
}
