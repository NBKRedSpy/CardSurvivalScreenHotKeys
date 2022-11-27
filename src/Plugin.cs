using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace ScreenHotKeys
{

    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource Log { get; set; }
        private void Awake()
        {
            Log = Logger;
            Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_NAME);
            harmony.PatchAll();
        }
    }
}