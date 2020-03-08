using Iris.Internal;
using System;
using SfmlFont = SFML.Graphics.Font;
using SfmlText = SFML.Graphics.Text;

namespace Iris.Graphics
{
    public class Font : IDisposable
    {
        private bool _disposed;

        internal SfmlFont SfmlFont { get; }
        internal SfmlText MeasureContainer { get; }

        public uint CharacterSize { get; set; } = 12;
        public float CharacterSpacing { get; set; } = 1;
        public float LineSpacing { get; set; } = 1;
        public uint OutlineThickness { get; set; }

        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public bool Underline { get; set; }
        public bool Strikethrough { get; set; }

        ~Font()
        {
            Dispose(false);
        }

        public Font(string filePath)
        {
            SfmlFont = new SfmlFont(filePath);
            MeasureContainer = new SfmlText();
        }

        public Font(Font font)
        {
            font.EnsureNotDisposed();

            SfmlFont = new SfmlFont(font.SfmlFont);
            MeasureContainer = new SfmlText();

            CharacterSize = font.CharacterSize;
            CharacterSpacing = font.CharacterSpacing;
            LineSpacing = font.LineSpacing;
            OutlineThickness = font.OutlineThickness;

            Bold = font.Bold;
            Italic = font.Italic;
            Underline = font.Underline;
            Strikethrough = font.Strikethrough;
        }

        public Rectangle Measure(string text)
        {
            EnsureNotDisposed();

            MeasureContainer.Font = SfmlFont;
            MeasureContainer.DisplayedString = text;
            MeasureContainer.CharacterSize = CharacterSize;
            MeasureContainer.LetterSpacing = CharacterSpacing;
            MeasureContainer.LineSpacing = LineSpacing;
            MeasureContainer.OutlineThickness = OutlineThickness;
            MeasureContainer.Style = SfmlText.Styles.Regular;

            if (Bold)
                MeasureContainer.Style |= SfmlText.Styles.Bold;

            if (Italic)
                MeasureContainer.Style |= SfmlText.Styles.Italic;

            if (Underline)
                MeasureContainer.Style |= SfmlText.Styles.Underlined;

            if (Strikethrough)
                MeasureContainer.Style |= SfmlText.Styles.StrikeThrough;

            return MeasureContainer.GetLocalBounds()
                                   .ToIrisRectangle();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // No managed objects to dispose.
                }

                MeasureContainer.Dispose();
                SfmlFont.Dispose();

                _disposed = true;
            }
        }

        internal SfmlText ConstructText(string text)
        {
            var txt = new SfmlText(text, SfmlFont, CharacterSize)
            {
                LetterSpacing = CharacterSpacing,
                LineSpacing = LineSpacing,
                OutlineThickness = OutlineThickness
            };

            if (Bold)
                txt.Style |= SfmlText.Styles.Bold;

            if (Italic)
                txt.Style |= SfmlText.Styles.Italic;

            if (Underline)
                txt.Style |= SfmlText.Styles.Underlined;

            if (Strikethrough)
                txt.Style |= SfmlText.Styles.StrikeThrough;

            return txt;
        }

        private void EnsureNotDisposed()
        {
            if (_disposed)
                throw new InvalidOperationException("This font has already been disposed of.");
        }
    }
}