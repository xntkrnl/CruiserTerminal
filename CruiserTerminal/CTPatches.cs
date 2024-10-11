using HarmonyLib;
using Unity.Netcode;
using UnityEngine;

namespace CruiserTerminal
{
    public static class CTPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(VehicleController), "Start")]
        static void StartPatch()
        {
            CTFunctions.Spawn();
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(GameNetworkManager), "Start")]
        private static void AddPrefabsToNetwork()
        {
            GameObject terminalPrefab = CTPlugin.mainAssetBundle.LoadAsset<GameObject>("CruiserTerminal.prefab");
            NetworkManager.Singleton.AddNetworkPrefab(terminalPrefab);

            GameObject terminalPosPrefab = CTPlugin.mainAssetBundle.LoadAsset<GameObject>("terminalPosition.prefab");
            NetworkManager.Singleton.AddNetworkPrefab(terminalPosPrefab);
        }
    }
}
