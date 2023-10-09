using HarmonyLib;
using QModManager.API.ModLoading;

namespace DatabankCompletionLegacy
{
    [QModCore]
    public static class DatabankCompletionPatcher
    {
        [QModPatch]
        public static void DatabankCompletionPatch()
        {
            Harmony harmony = new Harmony("com.katthefox.databankcompletionlegacy");
            harmony.PatchAll();
        }
    }
}