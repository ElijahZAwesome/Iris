using SFML.Graphics;

namespace Iris.Internal
{
    internal class LineDrawable : Drawable
    {
        private Vector2 A { get; }
        private Vector2 B { get; }
        private Color Color { get; }
        private float Thickness { get; }

        public LineDrawable(Vector2 a, Vector2 b, Color color, float thickness)
        {
            A = a;
            B = b;
            Color = color;
            Thickness = thickness;
        }
         
        public void Draw(RenderTarget target, RenderStates states)
        {
            var normalizedDirection = (B - A).Normalized;
            var perpendicular = new Vector2(-normalizedDirection.Y, normalizedDirection.X);
            var offset = perpendicular * (Thickness * 2f);

            var verts = new Vertex[4];
            verts[0] = new Vertex((A + offset).ToSfmlVector(), Color);
            verts[1] = new Vertex((B + offset).ToSfmlVector(), Color);
            verts[2] = new Vertex((B - offset).ToSfmlVector(), Color);
            verts[3] = new Vertex((A - offset).ToSfmlVector(), Color);

            target.Draw(verts, PrimitiveType.Quads);
        }
    }
}
