using HarmonyLib;

namespace CruiserTerminal
{
    public static class CTPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(VehicleController), "Start")]
        static void StartPatch()
        {
            CTFunctions.Spawn();
        }

    }
}
