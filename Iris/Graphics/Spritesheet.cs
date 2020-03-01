using System.Collections.Generic;

namespace Iris.Graphics
{
    public class Spritesheet
    {
        private uint _cellWidth;
        private uint _cellHeight;

        private List<Rectangle> Cells { get; }

        internal Sprite Sprite { get; }

        public uint CellWidth
        {
            get => _cellWidth;
            set
            {
                _cellWidth = value;
                GenerateSourceRectangles();
            }
        }

        public uint CellHeight
        {
            get => _cellHeight;
            set
            {
                _cellHeight = value;
                GenerateSourceRectangles();
            }
        }

        public int CellCount => Cells.Count;

        internal Spritesheet(string filePath)
        {
            Cells = new List<Rectangle>();
            Sprite = new Sprite(filePath);

            CellWidth = 0;
            CellHeight = 0;
        }

        public Vector2 GetGranularXY(int cellIndex)
            => new Vector2(
                Cells[cellIndex].Left / _cellWidth,
                Cells[cellIndex].Top / _cellHeight
            );

        internal void Configure(int cellIndex, Vector2 position, Vector2 scale, Color color)
        {
            Sprite.SourceRectangle = Cells[cellIndex];
            Sprite.Position = position;
            Sprite.Color = color;
            Sprite.Scale = scale;
        }

        private void GenerateSourceRectangles()
        {
            if (_cellWidth == 0 || _cellHeight == 0)
                return;

            Cells.Clear();

            var totalCellsX = Sprite.Width / _cellWidth;
            var totalCellsY = Sprite.Height / _cellHeight;

            for (uint y = 0; y < totalCellsY; y++)
            {
                for (uint x = 0; x < totalCellsX; x++)
                {
                    Cells.Add(
                        new Rectangle(
                            x * CellWidth,
                            y * CellHeight,
                            CellWidth,
                            CellHeight
                        )
                    );
                }
            }
        }
    }
}
