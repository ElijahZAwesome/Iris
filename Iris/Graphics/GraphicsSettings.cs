using SdlSharp;
using SdlSharp.Graphics;

namespace Iris.Graphics
{
    public class GraphicsSettings
    {
        private readonly Game _game;

        private int _windowWidth = 640;
        private int _windowHeight = 480;

        private bool _enableVerticalSync;

        public int WindowWidth
        {
            get => _windowWidth;
            set
            {
                _windowWidth = value;

                if (_game.Initialized)
                    _game.SetWindowSize(_windowWidth, _windowHeight);
            }
        }

        public int WindowHeight
        {
            get => _windowHeight;
            set
            {
                _windowHeight = value;

                if (_game.Initialized)
                    _game.SetWindowSize(_windowWidth, _windowHeight);
            }
        }

        public bool EnableVerticalSync
        {
            get => _enableVerticalSync;
            set
            {
                _enableVerticalSync = value;
                Hint.RenderVsync.Set(_enableVerticalSync ? "1" : "0", HintPriority.Override);
            }
        }

        public Color ClearColor { get; set; } = Colors.Black;

        internal GraphicsSettings(Game game)
        {
            _game = game;
        }
    }
}