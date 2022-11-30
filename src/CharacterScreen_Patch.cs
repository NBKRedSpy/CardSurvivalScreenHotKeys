using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace ScreenHotKeys
{
    [HarmonyPatch(typeof(CharacterScreen), "LateUpdate")]
    public static class CharacterScreen_Patch
    {
        public static void Prefix(CharacterScreen __instance, ref bool __runOriginal)
        {

            //Do not execute hotkeys if the user is in the guide screen.  It interferes with the search
            if (GraphicsManager_Patch.IsGuideScreenOpen())
            {
                return;
            }

            GraphicsManager_Patch.MoveCardLine(null, __instance.EquipmentSlotsLine, Plugin.ScrollLeftKey, Plugin.ScrollRightKey, ref __runOriginal);
        }

    }
}
