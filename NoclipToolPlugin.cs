using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace NoclipTool
{
    [BepInPlugin(PluginConstants.Guid, PluginConstants.Name, PluginConstants.Version)]
    public class NoclipToolPlugin : BaseUnityPlugin
    {
        public static ConfigEntry<KeyboardShortcut> ToggleNoclipKey;
        public static ConfigEntry<KeyboardShortcut> ToggleMenuKey;
        public static ConfigEntry<float> SpeedMultiplier;
        internal static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;

            ToggleNoclipKey = Config.Bind("General", "ToggleNoclipKey",
                new KeyboardShortcut(KeyCode.F5), "Клавиша вкл/выкл noclip");

            ToggleMenuKey = Config.Bind("General", "ToggleMenuKey",
                new KeyboardShortcut(KeyCode.F7), "Клавиша открытия меню");

            SpeedMultiplier = Config.Bind("General", "SpeedMultiplier", 2.0f,
                new ConfigDescription("Множитель скорости",
                    new AcceptableValueRange<float>(0.5f, 10f)));

            Harmony.CreateAndPatchAll(typeof(NoclipToolPlugin).Assembly, PluginConstants.Guid);

            var menuObj = new GameObject("NoclipTool_Menu");
            DontDestroyOnLoad(menuObj);
            menuObj.AddComponent<NoclipMenu>();

            Logger.LogInfo($"{PluginConstants.Name} v{PluginConstants.Version} загружен.");
        }
    }
}