using Iris.Internal;
using SfmlMouse = SFML.Window.Mouse;

namespace Iris.Input
{
    public class Mouse
    {
        private static Game _game;

        public static Vector2 GetPosition(bool absolute = false)
        {
            return absolute
                ? SfmlMouse.GetPosition().ToIrisVector()
                : SfmlMouse.GetPosition(_game.RenderWindow).ToIrisVector();
        }

        public static void SetPosition(Vector2 position, bool absolute = false)
        {
            if (absolute)
                SfmlMouse.SetPosition(position);
            else
                SfmlMouse.SetPosition(position, _game.RenderWindow);
        }

        public static bool IsButtonDown(int button)
            => SfmlMouse.IsButtonPressed((SfmlMouse.Button)button);

        public static bool IsButtonUp(int button)
            => !IsButtonDown(button);

        internal static void SetActiveGame(Game game)
            => _game = game;
    }
}