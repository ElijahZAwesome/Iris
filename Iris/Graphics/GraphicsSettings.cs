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
        private uint _antialiasingLevel = 1;

        private bool _useSrgbCapableFrameBuffer;
        private bool _enableVerticalSync;
        private bool _useOpenGlCoreProfile;

        private OpenGlVersion _openGlVersion = OpenGlVersion.OpenGl30;

        public bool UpdateImmediately { get; set; }

        public uint BackBufferWidth
        {
            get => _backBufferWidth;
            set
            {
                _backBufferWidth = value;

                if (UpdateImmediately)
                    CommitChanges();
            }
        }

        public uint BackBufferHeight
        {
            get => _backBufferHeight;
            set
            {
                _backBufferHeight = value;

                if (UpdateImmediately)
                    CommitChanges();
            }
        }

        public uint ColorDepth
        {
            get => _colorDepth;
            set
            {
                _colorDepth = value;

                if (UpdateImmediately)
                    CommitChanges();
            }
        }

        public bool EnableVerticalSync
        {
            get => _enableVerticalSync;
            set
            {
                _enableVerticalSync = value;

                if (UpdateImmediately)
                    CommitChanges();
            }
        }

        public uint FramerateLimit
        {
            get => _framerateLimit;
            set
            {
                _framerateLimit = value;

                if (UpdateImmediately)
                    CommitChanges();
            }
        }

        public OpenGlVersion OpenGlVersion
        {
            get => _openGlVersion;
            set
            {
                _openGlVersion = value;

                if (UpdateImmediately)
                    CommitChanges();
            }
        }

        public bool UseOpenGlCoreProfile
        {
            get => _useOpenGlCoreProfile;
            set
            {
                _useOpenGlCoreProfile = value;

                if (UpdateImmediately)
                    CommitChanges();
            }
        }

        public uint AntialiasingLevel
        {
            get => _antialiasingLevel;
            set
            {
                _antialiasingLevel = value;

                if (UpdateImmediately)
                    CommitChanges();
            }
        }

        public bool UseSrgbCapableFrameBuffer
        {
            get => _useSrgbCapableFrameBuffer;
            set
            {
                _useSrgbCapableFrameBuffer = value;

                if (UpdateImmediately)
                    CommitChanges();
            }
        }

        public bool IsFullScreen { get; set; }
        public bool IsBorderless { get; set; }

        public void CommitChanges()
        {
            if (_game.Window != null)
            {
                _game.ResetWindow();

                _game.Window.SetFramerateLimit(_framerateLimit);
                _game.Window.SetVerticalSyncEnabled(_enableVerticalSync);
            }
        }

        internal Styles WindowStyle
        {
            get
            {
                var style = (Styles)0;

                if (!IsBorderless)
                    style |= Styles.Close;

                if (IsFullScreen)
                    style |= Styles.Fullscreen;

                return style;
            }
        }

        internal VideoMode VideoMode
            => new VideoMode(
                BackBufferWidth,
                BackBufferHeight,
                ColorDepth
            );

        internal ContextSettings ContextSettings
            => new ContextSettings(
                0, 0,
                AntialiasingLevel,
                OpenGlVersion.Major,
                OpenGlVersion.Minor,
                UseOpenGlCoreProfile
                    ? ContextSettings.Attribute.Core
                    : ContextSettings.Attribute.Default,
                UseSrgbCapableFrameBuffer
            );

        internal GraphicsSettings(Game game)
        {
            _game = game;
        }
    }
}