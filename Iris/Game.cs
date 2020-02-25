using System;
using Iris.Content;
using Iris.Diagnostics;
using Iris.Graphics;
using SFML.Graphics;
using SFML.System;

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
            
            ResetWindow();
            Initialize();

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

        internal void ResetWindow()
        {
            if (Window != null)
            {
                Window.Closed -= Window_Closed;
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
        
        private void InitializeRenderingSystem()
        {
            Window = new RenderWindow(
                GraphicsSettings.VideoMode, 
                WindowProperties.Title,
                WindowProperties.WindowStyle
            );
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