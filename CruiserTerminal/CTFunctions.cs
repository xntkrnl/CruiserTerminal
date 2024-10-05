using Unity.Netcode;
using UnityEngine;

namespace CruiserTerminal
{
    public static class CTFunctions
    {
        //playerpos -5 -0.4 5.5
        public static void Spawn()
        {
            ///Cruiser Terminal
            var cruiser = GameObject.Find("CompanyCruiser(Clone)");
            var terminal = GameObject.Instantiate(CTPlugin.mainAssetBundle.LoadAsset("CruiserTerminal.prefab") as GameObject);

            var cruiserNO = cruiser.GetComponent<NetworkObject>();
            var terminalNO = terminal.GetComponent<NetworkObject>();

            CTPlugin.mls.LogInfo("terminalNO is null = " + (terminalNO == null));
            CTPlugin.mls.LogInfo("cruiserNO is null = " + (cruiserNO == null));

            terminalNO.Spawn();

            //bool tmp = terminalNO.TrySetParent(cruiserNO);
            //CTPlugin.mls.LogInfo("tmp is = " + tmp);

            terminal.name = "Cruiser Terminal";

            ///terminalPosition
            GameObject terminalPosition = new GameObject("terminalPosition");
            terminalPosition.transform.SetParent(cruiser.transform);
            terminalPosition.transform.localPosition = new Vector3(1.293f, 0.938f, -3.274f);
        }
    }
}
