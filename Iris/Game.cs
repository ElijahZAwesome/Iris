using System;
using System.Threading;
using System.Threading.Tasks;
using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Sound;

namespace Iris
{
    public class Game : IDisposable
    {
        private Renderer Renderer { get; }

        protected Application Application { get; }
        protected Window Window { get; }

        public bool Disposing { get; private set; }
        public bool Disposed { get; private set; }

        protected Game() : this(new (Hint, string)[] { })
        {
        }

        protected Game(params (Hint hint, string value)[] hints)
            : this(
                Subsystems.Everything,
                ImageFormats.Jpg | ImageFormats.Png,
                MixerFormats.Mod | MixerFormats.Mp3 | MixerFormats.Ogg,
                hints
            )
        {
        }

        protected Game(
            Subsystems subsystems,
            ImageFormats imageFormats,
            MixerFormats mixerFormats,
            (Hint hint, string value)[] hints)
        {
            Application = new Application(
                subsystems,
                imageFormats,
                mixerFormats,
                true,
                hints
            );

            Application.Quitting += Application_Quitting;

            Window = Window.Create(
                new Size(320, 240),
                WindowFlags.Vulkan | WindowFlags.Resizable,
                out Renderer renderer
            );

            Renderer = renderer;
        }

        public void Run()
        {
            while (!Disposing)
            {
                Application.DispatchEvent();

                try
                {
                    Update();
                    Draw();
                    
                    Renderer.Flush();
                    Renderer.Present();
                    Window.UpdateSurface();
                }
                catch (SdlException)
                {
                    if (!Disposing)
                        throw;
                }

                Thread.Sleep(16);
            }
        }

        public async Task RunAsync()
            => await Task.Run(Run);

        public void Dispose()
        {
            Disposing = true;

            Renderer?.Dispose();
            Window?.Dispose();
            Application?.Dispose();

            Disposing = false;
            Disposed = true;
        }
        
        protected virtual void Update()
        {
        }

        protected virtual void Draw()
        {
        }

        protected void Clear(Color color)
        {
            Renderer.DrawColor = color;
            Renderer.Clear();
        }

        protected void FillRectangle(float x, float y, float width, float height, Color color)
        {
            var prevColor = Renderer.DrawColor;
            
            Renderer.DrawColor = color;
            Renderer.FillRectangle(new RectangleF(new PointF(x, y), new SizeF(width, height)));

            Renderer.DrawColor = prevColor;
        }

        private void Application_Quitting(object sender, SdlEventArgs e)
        {
            Dispose();
        }
    }
}