using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.UI;

namespace ScreenHotKeys
{

    [HarmonyPatch(typeof(GraphicsManager), "Update")]
    internal static class GraphicsManager_Patch
    {
        private static FieldInfo ButtonMoveTargetInfo = AccessTools.Field(typeof(CardLine), "ButtonMoveTarget");
        private static Button CardDestoryButton;
        private static Button CharacterButton;

        private static FieldInfo GuideFieldInfo = AccessTools.Field(typeof(GameManager), "Guide");

        /// <summary>
        /// The card info screen.
        /// </summary>
        private static GameObject GuideScreenGameObject;


        public static bool IsGuideScreenOpen()
        {
            return GuideScreenGameObject?.activeInHierarchy ?? false;
        }

        public static void Prefix(GraphicsManager __instance, GameManager ___GM, ref bool __runOriginal)
        {
            if (GuideScreenGameObject == null)
            {
                ContentDisplayer guideScreen;
                guideScreen = (ContentDisplayer)GuideFieldInfo.GetValue(___GM);
                GuideScreenGameObject = guideScreen?.gameObject;
            }

            //Do not execute hotkeys if the user is in the guide screen.  It interferes with the search
            if (IsGuideScreenOpen())
            {
                return;
            }

            //Debug update
            if (Input.GetKeyDown(Plugin.WaitingOptionsKey))
            {

                Button button = GameObject.Find("MainCanvas/StatsCanvas/TimeSkip/ButtonObject").GetComponent<Button>();

                if (button.isActiveAndEnabled && button.IsInteractable())
                {
                    button.onClick.Invoke();
                }

                __runOriginal = false;
            }
            else if (Input.GetKeyDown(Plugin.BlueprintScreenKey))
            {
                Button button = __instance.BlueprintsButtonTr.gameObject.GetComponent<Button>();

                if(button.isActiveAndEnabled && button.IsInteractable())
                {
                    button.onClick.Invoke();
                }
                __runOriginal = false;
            }
            //Character window
            else if (Input.GetKeyDown(Plugin.CharacterScreenKey))
            {

                //Encounter screen.  For example, Seagull raid.
                if (CharacterButton == null)
                {
                    CharacterButton = GameObject.Find(
                        "MainCanvas/StatsCanvas/Equipment_Large_Bar/ButtonObject")
                        .GetComponent<Button>();
                }

                if (CharacterButton.isActiveAndEnabled && CharacterButton.IsInteractable())
                {
                    CharacterButton.onClick.Invoke();
                }
            }
            //Closes all popups with the escape key
            else if (Input.GetKeyDown(Plugin.ExitScreenKey) && (__instance.HasPopup || __instance.StatInspection.gameObject.activeInHierarchy))
            {
                __instance.CloseAllPopups();
                __runOriginal = false;
                return;
            }
            else if (Input.GetKeyDown(Plugin.ConfirmActionKey))
            {
                //Encounter screen.  For example, Seagull raid.
                if(CardDestoryButton == null)
                {
                    CardDestoryButton = GameObject.Find(
                        "MainCanvas/TooltipRect/DestroyedCardsPopup/ShadowAndPopupWithTitle/Content/Content/ButtonBase/ButtonObject")
                        .GetComponent<Button>();
                }

                if(CardDestoryButton.isActiveAndEnabled && CardDestoryButton.IsInteractable() )
                {
                    CardDestoryButton.onClick.Invoke();
                }
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
