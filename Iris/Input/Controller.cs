using SFML.Window;
using System.Collections.Generic;

namespace Iris.Input
{
    public static class Controller
    {
        public static List<ControllerInfo> AvailableControllers { get; }

        static Controller()
        {
            AvailableControllers = new List<ControllerInfo>();
            UpdateStates();
        }

        public static void UpdateStates()
        {
            Joystick.Update();
            AvailableControllers.Clear();

            for (byte i = 0; i < Joystick.Count; i++)
            {
                if (!Joystick.IsConnected(i))
                    continue;

                var ident = Joystick.GetIdentification(i);

                AvailableControllers.Add(new ControllerInfo
                {
                    Id = i,
                    Name = ident.Name,
                    ProductId = (short)ident.ProductId,
                    VendorId = (short)ident.VendorId,
                    ButtonCount = (short)Joystick.GetButtonCount(i)
                });
            }
        }
    }
}
