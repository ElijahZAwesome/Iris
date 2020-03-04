using SFML.Window;

namespace Iris.Input
{
    public class ControllerInfo
    {
        public byte Id { get; internal set; }
        public string Name { get; internal set; }
        
        public short VendorId { get; internal set; }
        public short ProductId { get; internal set; }

        public short ButtonCount { get; internal set; }
        public short AxisCount => 8;

        public bool IsButtonPressed(short button)
            => Joystick.IsButtonPressed(Id, (uint)button);

        public float GetAxisValue(byte axis)
            => Joystick.GetAxisPosition(Id, (Joystick.Axis)axis);
    }
}
