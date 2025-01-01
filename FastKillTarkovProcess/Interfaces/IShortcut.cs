using FastKillTarkovProcess.Enums;

namespace FastKillTarkovProcess.Interfaces
{
    public interface IShortcut
    {
        public InputDevice InputDevice { get; }

        int Shortcut { get; }

        bool IsNone => InputDevice == InputDevice.None;

        string Name { get; }

        static abstract IShortcut Deserialize(string json);

        string Serialize();
    }
}