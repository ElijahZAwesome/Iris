using SFML.Window;

namespace Iris.Graphics
{
    public class GraphicsSettings
    {
        private readonly Game _game;

        private uint _backBufferWidth = 640;
        private uint _backBufferHeight = 480;
        private uint _framerateLimit = 60;
        private uint _colorDepth = 24;

        private bool _enableVerticalSync;

        private OpenGlVersion _openGlVersion = OpenGlVersion.OpenGl30;

        public uint BackBufferWidth
        {
            get => _backBufferWidth;
            set
            {
                _backBufferWidth = value;

                if (_game.Window != null)
                    _game.ResetWindow();
            }
        }

        public uint BackBufferHeight
        {
            get => _backBufferHeight;
            set
            {
                _backBufferHeight = value;

                if (_game.Window != null)
                    _game.ResetWindow();
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

        public uint ColorDepth
        {
            get => _colorDepth;
            set
            {
                _colorDepth = value;

                if (_game.Window != null)
                    _game.ResetWindow();
            }
        }

        public OpenGlVersion OpenGlVersion
        {
            get => _openGlVersion;
            set
            {
                _openGlVersion = value;

                if (_game.Window != null)
                    _game.ResetWindow();
            }
        }

        public Color ClearColor { get; set; } = Color.Black;

        internal VideoMode VideoMode
            => new VideoMode(
                BackBufferWidth,
                BackBufferHeight,
                ColorDepth
            );

        internal ContextSettings ContextSettings
            => new ContextSettings(0, 0, 0,
                OpenGlVersion.Major,
                OpenGlVersion.Minor,
                ContextSettings.Attribute.Default,
                true
            );

        internal GraphicsSettings(Game game)
        {
            _game = game;
        }
    }
}