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
            GraphicsManager_Patch.MoveCardLine(null, __instance.EquipmentSlotsLine, Plugin.ScrollLeftKey, Plugin.ScrollRightKey, ref __runOriginal);
        }

    }
}
