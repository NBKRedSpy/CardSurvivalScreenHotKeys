using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Bindings;

namespace ScreenHotKeys
{

    [HarmonyPatch(typeof(GraphicsManager), "Update")]
    internal class GraphicsManager_Patch
    {
        public static FieldInfo ButtonMoveTargetInfo = AccessTools.Field(typeof(CardLine), "ButtonMoveTarget");

        public static void Prefix(GraphicsManager __instance, ref bool __runOriginal)
        {

            if (Input.GetKeyDown(Plugin.BlueprintScreenKey))
            {
                if (__instance.HasPopup == false && __instance.BlueprintModelsPopup.gameObject.activeInHierarchy == false)
                {
                    __instance.BlueprintModelsPopup.Show();
                    __runOriginal = false;
                }
            }
            //Character window
            else if (Input.GetKeyDown(Plugin.CharacterScreenKey))
            {
                if (__instance.HasPopup == false && __instance.CharacterWindow.gameObject.activeInHierarchy == false)
                {
                    __instance.CloseAllPopups();
                    __instance.CharacterWindow.Open();
                    __runOriginal = false;
                }
                
            }
            //Closes all popups with the escape key
            else if (Input.GetKeyDown(Plugin.ExitScreenKey) && (__instance.HasPopup || __instance.StatInspection.gameObject.activeInHierarchy))
            {
                __instance.CloseAllPopups();
                __runOriginal = false;
                return;
            }
            //LocationSlots is the top line
            //BaseSlots is the middle line.
            else if( MoveCardLine(__instance, __instance.LocationSlotsLine, Plugin.LocationScrollLeftKey, Plugin.LocationScrollRightKey, ref __runOriginal) ||
                MoveCardLine(__instance, __instance.BaseSlotsLine, Plugin.ScrollLeftKey, Plugin.ScrollRightKey, ref __runOriginal) ||
                MoveCardLine(null, __instance.BlueprintSlotsLine, Plugin.ScrollLeftKey, Plugin.ScrollRightKey, ref __runOriginal)
                )
            {
                return;
            }
        }

        /// <summary>
        ///// Moves a card line forward or back as long as the line is not currently moving.
        /// </summary>
        /// <param name="cardLine">The card line to move</param>
        /// <param name="moveBackKey">The key to press to move the line backwards</param>
        /// <param name="moveFowardKey">The key to press to move the line forwards</param>
        /// <param name="runOriginal">Will set to false if the line was moved.</param>
        /// <returns>Returns true if one of the keys were pressed</returns>
        public static bool MoveCardLine(GraphicsManager graphicsManager, CardLine cardLine, KeyCode moveBackKey, KeyCode moveFowardKey, ref bool runOriginal)
        {

            bool moveBack = Input.GetKey(moveBackKey);
            bool moveForward = Input.GetKey(moveFowardKey);


            //Check for popup just incase other if branches targets a popup.
            if (moveBack == false && moveForward == false) return false;
            if (graphicsManager != null && graphicsManager.HasPopup) return false;

            if (runOriginal == false || (float)ButtonMoveTargetInfo.GetValue(cardLine) != 0f) return true;

            if (moveForward) 
            { 
                cardLine.MoveToNextPos(); 
            } 
            else 
            { 
                cardLine.MoveToPrevPos(); 
            }

            runOriginal = false;

            return true;
        }
    }
}
