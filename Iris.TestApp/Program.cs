namespace Iris.TestApp
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            using var game = new MyGame();
            game.Run();
        }
    }
}