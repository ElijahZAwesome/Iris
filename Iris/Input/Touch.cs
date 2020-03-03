using Iris.Internal;

namespace Iris.Input
{
    public class Touch
    {
        private static Game _game;

        internal Touch(Game game)
        {
            _game = game;
        }

        public static bool IsDown(uint finger)
            => SFML.Window.Touch.IsDown(finger);

        public static Vector2 GetPosition(uint finger, bool absolute = false)
        {
            if (absolute)
                return SFML.Window.Touch.GetPosition(finger)
                                        .ToIrisVector();

            return SFML.Window.Touch.GetPosition(finger, _game.RenderWindow)
                                    .ToIrisVector();
        }
    }
}