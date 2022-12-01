using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace ScreenHotKeys
{

    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {


        public static KeyCode ScrollLeftKey;
        public static KeyCode ScrollRightKey;
        public static KeyCode LocationScrollLeftKey;
        public static KeyCode LocationScrollRightKey;
        public static KeyCode CharacterScreenKey;
        public static KeyCode BlueprintScreenKey;
        public static KeyCode ConfirmActionKey;
        public static KeyCode ExitScreenKey;
        public static KeyCode WaitingOptionsKey;

        public static ManualLogSource Log { get; set; }
        private void Awake()
        {

            SetConfigValues();
            Log = Logger;
            Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_NAME);
            harmony.PatchAll();
        }

        private void SetConfigValues()
        {
            const string sectionName = "Keys";
            ScrollLeftKey = Config.Bind(sectionName, nameof(ScrollLeftKey), KeyCode.A, "Scroll left for most card lines").Value;
            ScrollRightKey = Config.Bind(sectionName, nameof(ScrollRightKey), KeyCode.S, "Scroll right for most card lines").Value;
            LocationScrollLeftKey = Config.Bind(sectionName, nameof(LocationScrollLeftKey), KeyCode.Q, "Scroll left for location cards").Value;
            LocationScrollRightKey = Config.Bind(sectionName, nameof(LocationScrollRightKey), KeyCode.W, "Scroll Right for location cards").Value;
            CharacterScreenKey = Config.Bind(sectionName, nameof(CharacterScreenKey), KeyCode.C, "Open the Character screen").Value;
            BlueprintScreenKey = Config.Bind(sectionName, nameof(BlueprintScreenKey), KeyCode.B, "Open the Blueprint screen").Value;
            ConfirmActionKey = Config.Bind(sectionName, nameof(ConfirmActionKey), KeyCode.Space, "Accept the dialog's left most action").Value;
            ExitScreenKey = Config.Bind(sectionName, nameof(ExitScreenKey), KeyCode.Escape, "Close the current dialog").Value;
            WaitingOptionsKey = Config.Bind(sectionName, nameof(WaitingOptionsKey), KeyCode.T, "Opens the Waiting Options dialog (rest/sleep, etc.)").Value;
        }


        public static string GetGameObjectPath(GameObject obj)
        {
            GameObject searchObject = obj;

            string path = "/" + searchObject.name;
            while (searchObject.transform.parent != null)
            {
                searchObject = searchObject.transform.parent.gameObject;
                path = "/" + searchObject.name + path;
            }
            return path;
        }

    }
}