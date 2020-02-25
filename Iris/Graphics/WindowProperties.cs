using SFML.Window;

namespace Iris.Graphics
{
    public class WindowProperties
    {
        private readonly Game _game;

        private bool _canResize;
        private bool _canClose = true;
        private bool _hasTitleBar = true;
        private bool _isFullScreen = false;
        private string _title = "Iris";

        public uint Width
        {
            get
            {
                if (_game.Window == null)
                    return 0;

                return _game.Window.Size.X;
            } 
        }

        public uint Height
        {
            get
            {
                if (_game.Window == null)
                    return 0;

                return _game.Window.Size.Y;
            }
        }

        public bool CanResize
        {
            get => _canResize;
            set
            {
                _canResize = value;

                if (_game.Window != null)
                    _game.ResetWindow();
            }
        }

        public bool CanClose
        {
            get => _canClose;
            set
            {
                _canClose = value;

                if (_game.Window != null)
                    _game.ResetWindow();
            }
        }

        public bool HasTitleBar
        {
            get => _hasTitleBar;
            set
            {
                _hasTitleBar = value;

                if (_game.Window != null)
                    _game.ResetWindow();
            }
        }

        public bool IsFullScreen
        {
            get => _isFullScreen;
            set
            {
                _isFullScreen = value;
                
                if(_game.Window != null)
                    _game.ResetWindow();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                _game.Window?.SetTitle(_title);
            }
        }

        internal Styles WindowStyle
        {
            get
            {
                var styles = (Styles)0;

                if (CanResize)
                    styles |= Styles.Resize;

                if (CanClose)
                    styles |= Styles.Close;

                if (HasTitleBar)
                    styles |= Styles.Titlebar;

                if (IsFullScreen)
                    styles |= Styles.Fullscreen;
                
                return styles;
            }
        }

        internal WindowProperties(Game game)
        {
            _game = game;
        }
    }
}