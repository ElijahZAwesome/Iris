using SFML.Graphics;

namespace Iris.Graphics
{
    public class GraphicsSettings
    {
        private readonly Game _game;

        private uint _windowWidth = 640;
        private uint _windowHeight = 480;
        private uint _framerateLimit = 60;

        private bool _enableVerticalSync;

        public uint WindowWidth
        {
            get => _windowWidth;
            set
            {
                _windowWidth = value;

                if (_game.Window != null)
                    _game.Resize(_windowWidth, _windowHeight);
            }
        }

        public uint WindowHeight
        {
            get => _windowHeight;
            set
            {
                _windowHeight = value;

                if (_game.Window != null)
                    _game.Resize(_windowWidth, _windowHeight);
            }
        }

        public bool EnableVerticalSync
        {
            get => _enableVerticalSync;
            set
            {
                _enableVerticalSync = value;
                _game.Window?.SetVerticalSyncEnabled(_enableVerticalSync);
            }
        }

        public uint FramerateLimit
        {
            get => _framerateLimit;
            set
            {
                _framerateLimit = value;
                _game.Window?.SetFramerateLimit(_framerateLimit);
            }
        }

        public Color ClearColor { get; set; } = Colors.Black;

        internal GraphicsSettings(Game game)
        {
            _game = game;
        }
    }
}