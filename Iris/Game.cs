using System;
using Iris.Content;
using Iris.Diagnostics;
using Iris.Graphics;
using Iris.Input;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Mouse = Iris.Input.Mouse;
using Window = Iris.Graphics.Window;

namespace Iris
{
    public class Game : IDisposable
    {
        private RenderContext RenderContext { get; set; }
        private Clock DeltaClock { get; }

        protected ContentManager Content { get; }

        internal RenderWindow RenderWindow { get; private set; }

        public Window Window { get; }
        public GraphicsSettings GraphicsSettings { get; }
        public FpsCounter FpsCounter { get; }

        public bool Running { get; set; }
        public bool Disposed { get; private set; }

        protected Game()
        {
            GraphicsSettings = new GraphicsSettings(this);

            DeltaClock = new Clock();
            FpsCounter = new FpsCounter();
            Content = new ContentManager();

            Window = new Window(this);
            Mouse.SetActiveGame(this);

            ResetWindow();
            Initialize();
            LoadContent();
        }

        public void Run()
        {
            if (Running)
            {
                throw new InvalidOperationException("Run() was already called before.");
            }

            Running = true;

            var delta = 0f;
            while (Running)
            {
                if (RenderWindow == null)
                    continue;

                RenderWindow.DispatchEvents();

                Update(delta);
                Draw(RenderContext);

                RenderWindow.Display();
                FpsCounter.Update();

                delta = DeltaClock.ElapsedTime.AsSeconds();
                DeltaClock.Restart();
            }

            RenderWindow?.Close();
            Exiting();
        }

        public void Dispose()
        {
            RenderWindow.Dispose();
            Disposed = true;
        }

        internal void ResetWindow()
        {
            if (RenderWindow != null)
            {
                DisconnectWindowEvents();

                RenderWindow.Close();
                RenderWindow.Dispose();
            }

            InitializeRenderingSystem();
        }

        protected virtual void Initialize()
        {
        }

        protected virtual void LoadContent()
        {
        }

        protected virtual void Update(float deltaTime)
        {
        }

        protected virtual void Draw(RenderContext context)
        {
        }

        protected virtual void TextInput(char character)
        {
        }

        protected virtual void KeyPressed(KeyCode keyCode, KeyModifiers modifiers)
        {
        }

        protected virtual void KeyReleased(KeyCode keyCode, KeyModifiers modifiers)
        {
        }

        protected virtual void MouseMoved(Vector2 position)
        {
        }

        protected virtual void MousePressed(Vector2 position, int button)
        {
        }

        protected virtual void MouseReleased(Vector2 position, int button)
        {
        }

        protected virtual void MouseWheelScrolled(Vector2 movement, float delta, MouseWheel wheel)
        {
        }

        protected virtual void Exiting()
        {
        }

        private void InitializeRenderingSystem()
        {
            RenderWindow = new RenderWindow(
                GraphicsSettings.VideoMode,
                Window.Title,
                GraphicsSettings.WindowStyle,
                GraphicsSettings.ContextSettings
            );

            RenderWindow.SetMouseCursorGrabbed(Window.CaptureCursor);
            RenderWindow.SetMouseCursorVisible(Window.ShowCursor);

            RenderWindow.SetActive(true);

            ConnectWindowEvents();
            RenderContext = new RenderContext(RenderWindow);
        }

        private void ConnectWindowEvents()
        {
            RenderWindow.Closed += Window_Closed;
            RenderWindow.MouseMoved += Window_MouseMoved;
            RenderWindow.MouseButtonPressed += Window_MouseButtonPressed;
            RenderWindow.MouseButtonReleased += Window_MouseButtonReleased;
            RenderWindow.MouseWheelScrolled += Window_MouseWheelScrolled;
            RenderWindow.KeyPressed += Window_KeyPressed;
            RenderWindow.KeyReleased += Window_KeyReleased;
            RenderWindow.TextEntered += Window_TextEntered;
        }

        private void DisconnectWindowEvents()
        {
            RenderWindow.Closed -= Window_Closed;
            RenderWindow.MouseMoved -= Window_MouseMoved;
            RenderWindow.MouseButtonPressed -= Window_MouseButtonPressed;
            RenderWindow.MouseButtonReleased -= Window_MouseButtonReleased;
            RenderWindow.MouseWheelScrolled -= Window_MouseWheelScrolled;
            RenderWindow.KeyPressed -= Window_KeyPressed;
            RenderWindow.KeyReleased -= Window_KeyReleased;
            RenderWindow.TextEntered -= Window_TextEntered;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Running = false;
        }

        private void Window_TextEntered(object sender, TextEventArgs e)
        {
            TextInput(e.Unicode[0]);
        }

        private void Window_KeyReleased(object sender, KeyEventArgs e)
        {
            var keyCode = (KeyCode)e.Code;
            var modifiers = KeyModifiers.None;

            if (e.Control) modifiers |= KeyModifiers.Ctrl;
            if (e.Alt) modifiers |= KeyModifiers.Alt;
            if (e.System) modifiers |= KeyModifiers.System;
            if (e.Shift) modifiers |= KeyModifiers.Shift;

            KeyReleased(keyCode, modifiers);
        }

        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            var keyCode = (KeyCode)e.Code;
            var modifiers = KeyModifiers.None;

            if (e.Control) modifiers |= KeyModifiers.Ctrl;
            if (e.Alt) modifiers |= KeyModifiers.Alt;
            if (e.System) modifiers |= KeyModifiers.System;
            if (e.Shift) modifiers |= KeyModifiers.Shift;

            KeyPressed(keyCode, modifiers);
        }

        private void Window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            MouseMoved(
                new Vector2(e.X, e.Y)
            );
        }

        private void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            MousePressed(
                new Vector2(e.X, e.Y),
                (int)e.Button
            );
        }

        private void Window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            MouseReleased(
                new Vector2(e.X, e.Y),
                (int)e.Button
            );
        }

        private void Window_MouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            MouseWheelScrolled(
                new Vector2(e.X, e.Y),
                e.Delta,
                (MouseWheel)e.Wheel
            );
        }
    }
}