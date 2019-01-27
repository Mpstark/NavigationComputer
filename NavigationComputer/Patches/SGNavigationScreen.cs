﻿using BattleTech;
using BattleTech.UI;
using Harmony;
using UnityEngine;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace NavigationComputer.Patches
{
    [HarmonyPatch(typeof(SGNavigationScreen), "Update")]
    public static class SGNavigationScreen_Update_Patch
    {
        public static void Postfix()
        {
            foreach (var key in Main.DiscreteMapModes.Keys)
            {
                if (Input.GetKeyUp(key))
                    Main.ToggleMapMode(key);
            }

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F))
                Main.StartSearching();
        }
    }

    [HarmonyPatch(typeof(SGNavigationScreen), "Init", typeof(SimGameState), typeof(SGRoomController_Navigation))]
    public static class SGNavigationScreen_Init_Patch
    {
        public static void Postfix(SGNavigationScreen __instance, SimGameState simGame)
        {
            Main.SetupUIObjects(__instance);
            Main.SimGame = simGame;
        }
    }

    [HarmonyPatch(typeof(SGNavigationScreen), "HandleEscapeKeypress")]
    public static class SGNavigationScreen_HandleEscapeKeypress_Patch
    {
        public static bool Prefix(ref bool __result)
        {
            if (Main.CurrentMapMode == null)
                return true;

            // the return value in __result is if the esc was handled
            Main.TurnMapModeOff();
            __result = true;
            return false;
        }
    }
}