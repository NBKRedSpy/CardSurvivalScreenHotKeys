using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenHotKeys
{

    


    /// <summary>
    /// Maps space bar to the "Explore" and "Continue" buttons on the exploration popup.
    /// </summary>
    [HarmonyPatch(typeof(ExplorationPopup), "LateUpdate")]
    public static class ExploreAccept_Patch
    {

        private static Button ExploreButton;

        static ExploreAccept_Patch()
        {

        }

        public static void Prefix(GraphicsManager __instance, DismantleActionButton ___MainButton)
        {

            //Do not execute hotkeys if the user is in the guide screen.  It interferes with the search
            if (GraphicsManager_Patch.IsGuideScreenOpen())
            {
                return;
            }

            if (ExploreButton == null)
            {
                ExploreButton = ___MainButton.gameObject.transform.GetChild(0).GetComponent<Button>();
            }

            //check for key and enabled button.
            if (Input.GetKeyDown(Plugin.ConfirmActionKey) && ExploreButton.isActiveAndEnabled && ExploreButton.IsInteractable())
            {
                ExploreButton.onClick.Invoke();
            }
        }


    }
}
