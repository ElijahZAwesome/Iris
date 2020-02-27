using SFML.Window;

namespace Iris.Graphics
{
    public class WindowProperties
    {
        private readonly Game _game;
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

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                _game.Window?.SetTitle(_title);
            }
        }

        public bool CanResize { get; set; }
        public bool CanClose { get; set; }
        public bool HasTitleBar { get; set; }
        public bool IsFullScreen { get; set; }

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