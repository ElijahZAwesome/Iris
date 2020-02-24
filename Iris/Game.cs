using System;
using Iris.Content;
using Iris.Diagnostics;
using Iris.Graphics;
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

        public RenderWindow Window { get; private set; }
        public GraphicsSettings GraphicsSettings { get; }
        public FpsCounter FpsCounter { get; }

        public bool Running { get; set; }
        public bool Disposed { get; private set; }

        protected Game()
        {
            GraphicsSettings = new GraphicsSettings(this);

            InitializeRenderingSystem(
                new VideoMode(
                    GraphicsSettings.WindowWidth,
                    GraphicsSettings.WindowHeight,
                    24
                )
            );

            Initialize(GraphicsSettings);

            DeltaClock = new Clock();
            FpsCounter = new FpsCounter();
            Content = new ContentManager();
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


                Window.Clear(GraphicsSettings.ClearColor);
                Update(delta);
                Draw(RenderContext);

                Window.Display();
                FpsCounter.Update();

                delta = DeltaClock.ElapsedTime.AsSeconds();
                DeltaClock.Restart();
            }
        }

        public void Dispose()
        {
            Window.Dispose();
            Disposed = true;
        }

        internal void Resize(uint width, uint height)
        {
            Window.Closed -= Window_Closed;
            Window.Close();
            Window.Dispose();

            InitializeRenderingSystem(
                new VideoMode(
                    width,
                    height,
                    24
                )
            );
        }

        protected virtual void Initialize(GraphicsSettings settings)
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
        
        private void InitializeRenderingSystem(VideoMode videoMode)
        {
            Window = new RenderWindow(videoMode, "Iris");
            Window.SetActive(true);

            Window.Closed += Window_Closed;
            RenderContext = new RenderContext(Window);
        }
        
        private void Window_Closed(object sender, EventArgs e)
        {
            Window.Close();
            Running = false;
        }
    }
}