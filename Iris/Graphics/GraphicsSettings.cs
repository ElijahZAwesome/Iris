using SFML.Window;

namespace Iris.Graphics
{
    public class GraphicsSettings
    {
        private readonly Game _game;
        
        private uint _backBufferWidth = 640;
        private uint _backBufferHeight = 480;
        private uint _framerateLimit = 60;
        private uint _screenDepth = 24;

        private bool _enableVerticalSync;

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

        public uint ScreenDepth
        {
            get => _screenDepth;
            set
            {
                _screenDepth = value;

                if (_game.Window != null)
                    _game.ResetWindow();
            }
        }

        public Color ClearColor { get; set; } = Color.Black;

        internal VideoMode VideoMode 
            => new VideoMode(
                BackBufferWidth,
                BackBufferHeight,
                ScreenDepth
            );

        internal GraphicsSettings(Game game)
        {
            _game = game;
        }
    }
}