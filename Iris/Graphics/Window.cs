using Iris.Internal;

namespace Iris.Graphics
{
    public class Window
    {
        private readonly Game _game;
        
        private string _title = "Iris Engine";
        private Texture _icon = new Texture(1, 1);
        
        private bool _showCursor;
        private bool _captureCursor;
        
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                _game?.RenderWindow?.SetTitle(_title);
            }
        }

        public Texture Icon
        {
            get => _icon;
            set
            {
                if (_icon != null)
                {
                    _icon.SfmlImage?.Dispose();
                    _icon.SfmlTexture?.Dispose();
                }
                
                _icon = value;
                _game?.RenderWindow.SetIcon(_icon.Width, _icon.Height, _icon.PixelData);
            }
        }

        public Vector2 Position
        {
            get => _game.RenderWindow.Position.ToIrisVector();
            set => _game.RenderWindow.Position = value;
        }

        public Vector2 Dimensions
            => _game.RenderWindow.Size.ToIrisVector();

        public bool CaptureCursor
        {
            get => _captureCursor;
            set
            {
                _captureCursor = value;
                _game.RenderWindow?.SetMouseCursorGrabbed(_captureCursor);
            }
        }

        public bool ShowCursor
        {
            get => _showCursor;
            set
            {
                _showCursor = value;
                _game.RenderWindow?.SetMouseCursorVisible(_showCursor);
            }
        }

        internal Window(Game game)
        {
            _game = game;
        }
    }
}