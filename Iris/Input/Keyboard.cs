using SfmlKeyboard = SFML.Window.Keyboard;

namespace Iris.Input
{
    public class Keyboard
    {
        public static bool CtrlPressed => IsKeyDown(KeyCode.LeftControl) || IsKeyDown(KeyCode.RightControl);
        public static bool AltPressed => IsKeyDown(KeyCode.LeftAlt) || IsKeyDown(KeyCode.RightAlt);
        public static bool ShiftPressed => IsKeyDown(KeyCode.LeftShift) || IsKeyDown(KeyCode.RightShift);

        public static bool IsKeyDown(KeyCode keyCode)
            => SfmlKeyboard.IsKeyPressed((SfmlKeyboard.Key)keyCode);

        public static bool IsKeyUp(KeyCode keyCode)
            => !IsKeyDown(keyCode);

        private Keyboard() { }

        internal static KeyCode FromSfmlKeyCode(SfmlKeyboard.Key key)
        {
            if (key == SfmlKeyboard.Key.KeyCount)
                return KeyCode.Unsupported;

            return (KeyCode)key;
        }
    }
}
