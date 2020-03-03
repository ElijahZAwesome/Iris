using Iris.Internal.Math;

namespace Iris
{
    public static class MathF
    {
        private static Simplex Simplex { get; set; } = new Simplex();

        public static float Clamp(float value, float min, float max)
        {
            if (value > max) return max;
            if (value < min) return min;

            return value;
        }

        public static float Lerp(float from, float to, float pos)
        {
            pos = Clamp(pos, 0f, 1f);
            return from * (1 - pos) + to * pos;
        }

        public static Vector2 Lerp(Vector2 from, Vector2 to, float pos)
        {
            pos = Clamp(pos, 0f, 1f);

            float retX = Lerp(from.X, to.X, pos);
            float retY = Lerp(from.Y, to.Y, pos);

            return new Vector2(retX, retY);
        }

        public static void SimplexSeed(long seed)
            => Simplex = new Simplex(seed);

        public static double SimplexNoise(float x)
            => Simplex.Evaluate(x, 0);

        public static double SimplexNoise(float x, float y)
            => Simplex.Evaluate(x, y);

        public static double SimplexNoise(Vector2 coordinates)
            => Simplex.Evaluate(coordinates.X, coordinates.Y);

        public static double SimplexNoise(float x, float y, float z)
            => Simplex.Evaluate(x, y, z);

        public static double SimplexNoise(Vector3 coordinates)
            => Simplex.Evaluate(
                coordinates.X,
                coordinates.Y,
                coordinates.Z
            );
    }
}
