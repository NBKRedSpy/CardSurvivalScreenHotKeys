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


        public static void Postfix(InspectionPopup __instance, List<DismantleActionButton> ___OptionsButtons)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Check for a container screen.
                if (__instance.EmptyInventoryButton != null)
                {
                    //Assume the user wants to empty.
                    if (__instance.EmptyInventoryButton.interactable)
                    {
                        __instance.EmptyInventory();
                    }

                    //All other actions are ignored.  They should be dissasemble or put out fire, etc.
                    return;
                }
                else
                {
                    //check for diabled.  Generally dark.
                    if (___OptionsButtons[0].Interactable)
                    {
                        //Execute the first action
                        OnButtonClickedMethodInfo.Invoke(__instance, new object[] { 0, false });
                    }

                }
            }

        }
    }
}
