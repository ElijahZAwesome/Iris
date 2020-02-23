using System.Threading.Tasks;

namespace Iris.TestApp
{
    internal static class Program
    {
        internal static async Task Main(string[] args)
        {
            using var game = new MyGame();
            await game.RunAsync();
        }
    }
}