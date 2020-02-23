using System;
using System.Threading.Tasks;
using Iris.Diagnostics;
using Iris.Graphics;
using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Sound;

namespace Iris
{
    public class Game : IDisposable
    {
        private bool _alreadyStarted;
        private double _lastUpdateTime;

        private Renderer Renderer { get; }
        private RenderContext RenderContext { get; }
        private GraphicsSettings GraphicsSettings { get; }

        protected Application Application { get; }
        protected Window Window { get; }

        public FpsCounter FpsCounter { get; }

        public bool Initialized { get; private set; }
        public bool Disposed { get; private set; }

        protected Game()
            : this(
                Subsystems.Everything,
                WindowFlags.Desktop,
                ImageFormats.Jpg | ImageFormats.Png,
                MixerFormats.Mod | MixerFormats.Mp3 | MixerFormats.Ogg
            )
        {
        }

        protected Game(
            Subsystems subsystems,
            WindowFlags windowFlags,
            ImageFormats imageFormats,
            MixerFormats mixerFormats)
        {
            GraphicsSettings = new GraphicsSettings(this);
            Initialize(GraphicsSettings);

            Application = new Application(
                subsystems,
                imageFormats,
                mixerFormats
            );
            Application.Quitting += Application_Quitting;

            Window = Window.Create(
                new Size(
                    GraphicsSettings.WindowWidth,
                    GraphicsSettings.WindowHeight
                ),
                windowFlags,
                out Renderer renderer
            );
            
            Renderer = renderer;
            RenderContext = new RenderContext(renderer);
            FpsCounter = new FpsCounter();

            Initialized = true;
        }

        public void Run()
        {
            if (_alreadyStarted)
            {
                throw new InvalidOperationException("Run() was already called before.");
            }
            _alreadyStarted = true;

            try
            {
                while (!Disposed)
                {
                    Application.DispatchEvent();

                    var currentCounter = Timer.PerformanceCounter;
                    var delta = (currentCounter - _lastUpdateTime) * 1000.0 / Timer.PerformanceFrequency;

                    Update(delta);

                    RenderContext.Clear(GraphicsSettings.ClearColor);
                    Draw(RenderContext);
                    Renderer.Present();
                    
                    FpsCounter.Update();

                    _lastUpdateTime = currentCounter;
                }
            }
            catch (SdlException)
            {
                // TODO: Log in the future. Now ignore.
            }
        }

        public async Task RunAsync()
            => await Task.Run(Run);

        public void Dispose()
        {
            Window.Dispose();
            Application.Dispose();

            Disposed = true;
        }

        internal void SetWindowSize(int width, int height)
        {
            Window.Size = new Size(width, height);
        }

        protected virtual void Initialize(GraphicsSettings settings)
        {
            
        }

        protected virtual void Update(double deltaTime)
        {
        }

        protected virtual void Draw(RenderContext context)
        {
        }

        private void Application_Quitting(object sender, SdlEventArgs e)
        {
            Dispose();
        }
    }
}