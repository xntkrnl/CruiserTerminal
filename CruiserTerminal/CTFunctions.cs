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

            //bool tmp = terminalNO.TrySetParent(cruiserNO);
            //CTPlugin.mls.LogInfo("tmp is = " + tmp);
            terminal.name = "Cruiser Terminal";

            ///terminalPosition
            GameObject terminalPosition = GameObject.Instantiate(CTPlugin.mainAssetBundle.LoadAsset("terminalPosition.prefab") as GameObject);
            var terminalPositionNO = terminalPosition.GetComponent<NetworkObject>();
            terminalPosition.name = "terminalPosition";

            if (NetworkManager.Singleton.IsHost)
            {
                terminalNO.Spawn();
                terminalPositionNO.Spawn();
            }

            terminalPositionNO.TrySetParent(cruiserNO);
            terminalPosition.transform.localPosition = new Vector3(1.293f, 0.938f, -3.274f);
        }
    }
}