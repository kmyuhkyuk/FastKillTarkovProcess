using FastKillTarkovProcess.Enums;

namespace FastKillTarkovProcess.Interfaces
{
    public interface IShortcut
    {
        public InputDevice InputDevice { get; }

        int Shortcut { get; }

        string Name { get; }

        bool IsNone => InputDevice == InputDevice.None;

        static abstract IShortcut Deserialize(string json);

        string Serialize();
    }
}