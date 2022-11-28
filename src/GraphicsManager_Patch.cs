using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ScreenHotKeys
{

    [HarmonyPatch(typeof(GraphicsManager), "Update")]
    internal class GraphicsManager_Patch
    {
        public static void Prefix(GraphicsManager __instance, ref bool __runOriginal)
        {

            if (Input.GetKeyDown(KeyCode.B))
            {
                if (__instance.HasPopup == false && __instance.BlueprintModelsPopup.gameObject.activeInHierarchy == false)
                {
                    __instance.BlueprintModelsPopup.Show();
                    __runOriginal = false;
                }
            }
            //Character window
            else if (Input.GetKeyDown(KeyCode.C))
            {
                if (__instance.HasPopup == false && __instance.CharacterWindow.gameObject.activeInHierarchy == false)
                {
                    __instance.CloseAllPopups();
                    __instance.CharacterWindow.Open();
                    __runOriginal = false;
                }
                
            }
            //Closes all popups with the escape key
            else if (Input.GetKeyDown(KeyCode.Escape) && (__instance.HasPopup || __instance.StatInspection.gameObject.activeInHierarchy))
            {
                __instance.CloseAllPopups();
                __runOriginal = false;
                return;
            }
        }
    }
}
