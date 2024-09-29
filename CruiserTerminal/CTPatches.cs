using HarmonyLib;

namespace CruiserTerminal
{
    public static class CTPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(VehicleController), "Awake")]
        static void StartPatch()
        {
            CTFunctions.Spawn();
        }

    }
}
