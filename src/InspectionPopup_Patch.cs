using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenHotKeys
{


    /// <summary>
    /// Handles the majority of the popup type dialogs.
    /// Maps space to the button located at index zero.  
    /// Most often the dialogs only have a single button.
    /// </summary>
    [HarmonyPatch(typeof(InspectionPopup), "Update")]
    public static class InspectionPopup_Patch
    {

        static MethodInfo OnButtonClickedMethodInfo;

        static InspectionPopup_Patch()
        {
            OnButtonClickedMethodInfo = AccessTools.Method(typeof(InspectionPopup), "OnButtonClicked", new[] { typeof(int), typeof(bool) });
        }


        public static void Prefix(InspectionPopup __instance, List<DismantleActionButton> ___OptionsButtons, ref bool __runOriginal)
        {

            //Do not execute hotkeys if the user is in the guide screen.  It interferes with the search
            if (GraphicsManager_Patch.IsGuideScreenOpen())
            {
                return;
            }

            if (GraphicsManager_Patch.MoveCardLine(null, __instance.InventorySlotsLine, Plugin.ScrollLeftKey, Plugin.ScrollRightKey, ref __runOriginal)) { }
            else if (Input.GetKeyDown(Plugin.ConfirmActionKey))
            {
                Button emptyInventoryButton = __instance.EmptyInventoryButton;
                //Check for a container screen.
                if ( emptyInventoryButton  != null)
                {
                    //Assume the user wants to empty.
                    if (emptyInventoryButton.isActiveAndEnabled && emptyInventoryButton.interactable)
                    {
                        emptyInventoryButton.onClick.Invoke();
                        __runOriginal = false;
                    }

                    //All other actions are ignored.  They should be dissasemble or put out fire, etc.
                    return;
                }
                else
                {
                    if(___OptionsButtons.Count > 0)
                    {
                        DismantleActionButton button = ___OptionsButtons[0];
                        //check for diabled.  Generally dark.
                        if (button != null && button.isActiveAndEnabled)
                        {
                            button.OnClicked.Invoke(0);
                            __runOriginal = false;
                        }
                    }

                }
            }

        }
    }
}
