using Iris.Content;
using Iris.Diagnostics;
using Iris.Graphics;
using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Sound;
using System;

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

        protected ContentManager Content { get; }

        public FpsCounter FpsCounter { get; }

        public bool Constructed { get; private set; }
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

            Content = new ContentManager();
            Constructed = true;

            LoadContent();
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

                    if (Disposed)
                        break;

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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Disposed = true;

                Window.Dispose();
                Application.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void SetWindowSize(int width, int height)
        {
            Window.Size = new Size(width, height);
        }


        protected virtual void Initialize(GraphicsSettings settings)
        {
        }

        protected virtual void LoadContent()
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