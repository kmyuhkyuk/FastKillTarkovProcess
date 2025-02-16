using System.Text.Json;
using System.Text.Json.Serialization;
using FastKillTarkovProcess.Enums;
using FastKillTarkovProcess.Interfaces;
using SharpHook.Native;

namespace FastKillTarkovProcess.Models
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(ShortcutModel))]
    internal partial class ShortcutModelContext : JsonSerializerContext;

    [method: JsonConstructor]
    public class ShortcutModel(InputDevice inputDevice, int shortcut, string name) : IShortcut
    {
        public InputDevice InputDevice { get; } = inputDevice;

        public int Shortcut { get; } = shortcut;

        public string Name { get; } = name;

        public bool IsNone => InputDevice != InputDevice.None;

        public static ShortcutModel None => new();

        private ShortcutModel() : this(InputDevice.None, 0, "None")
        {
        }

        public ShortcutModel(KeyCode shortcut) : this(InputDevice.Keyboard, (int)shortcut, shortcut.ToString())
        {
        }

        public ShortcutModel(MouseButton shortcut) : this(InputDevice.Mouse, (int)shortcut, shortcut.ToString())
        {
        }

        public static IShortcut Deserialize(string json)
        {
            return JsonSerializer.Deserialize(json, typeof(ShortcutModel), ShortcutModelContext.Default)
                as ShortcutModel ?? new ShortcutModel();
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this, typeof(ShortcutModel), ShortcutModelContext.Default);
        }
    }
}