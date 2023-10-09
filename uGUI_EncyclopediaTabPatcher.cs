using System;
using System.Collections.Generic;
using Discord;
using HarmonyLib;
using QModManager.Utility;

namespace DatabankCompletionLegacy
{
    [HarmonyPatch(typeof(uGUI_EncyclopediaTab))]
    public static class uGUI_EncyclopediaTabPatcher
    {
        [HarmonyPatch(nameof(uGUI_EncyclopediaTab.OnUpdateEntry))]
        [HarmonyPostfix]
        public static void OnUpdateEntryPatch(uGUI_EncyclopediaTab __instance)
        {
            Logger.Log(Logger.Level.Debug, "OnUpdateEntryPatch triggered");
            ProgressCalculator.UpdateAllCategories(__instance.tree, __instance);
        }

        [HarmonyPatch(nameof(uGUI_EncyclopediaTab.OnAddEntry))]
        [HarmonyPostfix]
        public static void OnAddEntryPatch(uGUI_EncyclopediaTab __instance)
        {
            Logger.Log(Logger.Level.Debug, "OnAddEntryPatch triggered");
            ProgressCalculator.UpdateAllCategories(__instance.tree, __instance);
        }

        [HarmonyPatch(nameof(uGUI_EncyclopediaTab.OnRemoveEntry))]
        [HarmonyPostfix]
        public static void OnRemoveEntryPostfix(uGUI_EncyclopediaTab __instance)
        {
            Logger.Log(Logger.Level.Debug, "OnRemoveEntryPatch triggered");
            ProgressCalculator.UpdateAllCategories(__instance.tree, __instance);
        }

        [HarmonyPatch(nameof(uGUI_EncyclopediaTab.CreateNode))]
        [HarmonyPostfix]
        public static void CreateNodePostfix(uGUI_EncyclopediaTab __instance)
        {
            Logger.Log(Logger.Level.Debug, "CreateNodePostfix triggered");
            ProgressCalculator.UpdateAllCategories(__instance.tree,__instance);
        }
    }
}