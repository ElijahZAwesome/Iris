using System.Collections.Generic;
using SdlSharp.Input;

namespace Iris.Input
{
    public class Keyboard
    {
        private static readonly Dictionary<Keycode, bool> _keyStates;

        static Keyboard()
        {
            _keyStates = new Dictionary<Keycode, bool>();

            SdlSharp.Input.Keyboard.KeyDown += Keyboard_KeyDown;
            SdlSharp.Input.Keyboard.KeyUp += Keyboard_KeyUp;
        }

        public static bool IsKeyDown(Keycode keyCode)
            => _keyStates.ContainsKey(keyCode) && _keyStates[keyCode];

        public static bool IsKeyUp(Keycode keyCode)
        {
            if (!_keyStates.ContainsKey(keyCode))
                return true;

            return _keyStates[keyCode] == false;
        }

        private static void Keyboard_KeyUp(object sender, KeyboardEventArgs e)
        {
            if (!_keyStates.ContainsKey(e.Keycode))
                _keyStates.Add(e.Keycode, false);
            else
                _keyStates[e.Keycode] = false;
        }

        private static void Keyboard_KeyDown(object sender, KeyboardEventArgs e)
        {
            if (!_keyStates.ContainsKey(e.Keycode))
                _keyStates.Add(e.Keycode, true);
            else
                _keyStates[e.Keycode] = true;
        }
    }
}