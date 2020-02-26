using System;
using Iris.Content;
using Iris.Diagnostics;
using Iris.Graphics;
using Iris.Input;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Iris
{
    public class Game : IDisposable
    {
        private RenderContext RenderContext { get; set; }
        private Clock DeltaClock { get; }

        protected ContentManager Content { get; }

        internal RenderWindow Window { get; private set; }

        public GraphicsSettings GraphicsSettings { get; }
        public WindowProperties WindowProperties { get; }

        public FpsCounter FpsCounter { get; }

        public bool Running { get; set; }
        public bool Disposed { get; private set; }

        protected Game()
        {
            WindowProperties = new WindowProperties(this);
            GraphicsSettings = new GraphicsSettings(this);

            DeltaClock = new Clock();
            FpsCounter = new FpsCounter();
            Content = new ContentManager();

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
                if (Window == null)
                    continue;

                Window.DispatchEvents();

                Update(delta);
                Draw(RenderContext);

                Window.Display();
                FpsCounter.Update();

                delta = DeltaClock.ElapsedTime.AsSeconds();
                DeltaClock.Restart();
            }

            Window?.Close();
            Exiting();
        }

        public void Dispose()
        {
            Window.Dispose();
            Disposed = true;
        }

        internal void ResetWindow()
        {
            if (Window != null)
            {
                DisconnectWindowEvents();

                Window.Close();
                Window.Dispose();
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

        protected virtual void Exiting()
        {
        }

        private void InitializeRenderingSystem()
        {
            Window = new RenderWindow(
                GraphicsSettings.VideoMode,
                WindowProperties.Title,
                WindowProperties.WindowStyle
            );
            Window.SetActive(true);

            ConnectWindowEvents();
            RenderContext = new RenderContext(Window);
        }

        private void ConnectWindowEvents()
        {
            Window.Closed += Window_Closed;
            Window.KeyPressed += Window_KeyPressed;
            Window.KeyReleased += Window_KeyReleased;
            Window.TextEntered += Window_TextEntered;
        }

        private void DisconnectWindowEvents()
        {
            Window.Closed -= Window_Closed;
            Window.TextEntered -= Window_TextEntered;
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
            var modifiers = KeyModifiers.None;

            if (e.Control) modifiers |= KeyModifiers.Ctrl;
            if (e.Alt) modifiers |= KeyModifiers.Alt;
            if (e.System) modifiers |= KeyModifiers.System;
            if (e.Shift) modifiers |= KeyModifiers.Shift;

            KeyReleased((KeyCode)e.Code, modifiers);
        }

        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            var modifiers = KeyModifiers.None;

            if (e.Control) modifiers |= KeyModifiers.Ctrl;
            if (e.Alt) modifiers |= KeyModifiers.Alt;
            if (e.System) modifiers |= KeyModifiers.System;
            if (e.Shift) modifiers |= KeyModifiers.Shift;

            KeyPressed((KeyCode)e.Code, modifiers);
        }
    }
}