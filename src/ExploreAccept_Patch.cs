using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace ScreenHotKeys
{

    


    /// <summary>
    /// Maps space bar to the "Explore" and "Continue" buttons on the exploration popup.
    /// </summary>
    [HarmonyPatch(typeof(ExplorationPopup), "LateUpdate")]
    public static class ExploreAccept_Patch
    {

        private static MethodInfo ClickMainButtonMethodInfo;

        static ExploreAccept_Patch()
        {
            ClickMainButtonMethodInfo = AccessTools.Method(typeof(ExplorationPopup), "ClickMainButton");
        }

        public static void Prefix(GraphicsManager __instance, DismantleActionButton ___MainButton)
        {
            //check for key and enabled button.
            if (Input.GetKeyDown(KeyCode.Space) && ___MainButton.Interactable)
            {
                ClickMainButtonMethodInfo.Invoke(__instance, null);
                return;
            }
        }

    }
}
