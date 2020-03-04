using Iris.Graphics;
using Iris.Input;
using System;
using System.Text;

namespace Iris.TestApp
{
    public class MyGame : Game
    {
        private Font _font;
        private string _text = "YOU COULD CALL THIS ART\n     I SUPPOSE...";

        protected override void Initialize()
        {
            GraphicsSettings.BackBufferWidth = 1024;
            GraphicsSettings.BackBufferHeight = 600;
            GraphicsSettings.ColorDepth = 24;
            GraphicsSettings.EnableVerticalSync = true;

            GraphicsSettings.CommitChanges();
        }

        protected override void LoadContent()
        {
            _font = Content.Load<Font>("c64style.ttf");
            _font.CharacterSize = 16;
        }

        protected override void Draw(RenderContext context)
        {
            context.Clear(Color.Black);
            context.DrawString(_font, _text, new Vector2(0, 0), Color.White);
        }

        private static readonly StringBuilder sb = new StringBuilder();
        protected override void Update(float deltaTime)
        {
            sb.Clear();
            foreach (var controller in Controller.AvailableControllers)
            {
                sb.AppendLine($"Controller ID: {controller.Id} ({controller.VendorId}:{controller.ProductId})");
                sb.AppendLine($"Ctrlr. name: {controller.Name}");
                sb.AppendLine($"Buttons: ");

                for (byte i = 0; i < controller.ButtonCount; i++)
                {
                    sb.AppendLine($"  {i}: {(controller.IsButtonPressed(i) ? "PRESSED" : "NOT PRESSED")}");
                }

                sb.AppendLine("Axes: ");
                for (byte i = 0; i < controller.AxisCount; i++)
                {
                    sb.AppendLine($"  {i}: {controller.GetAxisValue(i)}");
                }

                sb.AppendLine("-----------");
            }

            _text = sb.ToString();
        }
    }
}