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
    internal class PopUpEscapeKey_Patch
    {
        public static void Prefix(GraphicsManager __instance, ref bool __runOriginal)
        {
            //Closes all popups with the escape key
            if (Input.GetKeyDown(KeyCode.Escape) && (__instance.HasPopup || __instance.StatInspection.gameObject.activeInHierarchy))
            {
                __instance.CloseAllPopups();
                __runOriginal = false;
                return;
            }
        }
    }
}
