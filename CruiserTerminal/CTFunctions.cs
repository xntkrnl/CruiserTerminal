using Unity.Netcode;
using UnityEngine;

namespace CruiserTerminal
{
    public static class CTFunctions
    {

        //canvar scale 0.003 0.003 0.001566473 position -0.03 0.592 -0.017
        //playerpos -5 -0.4 5.5
        //TerminalScript scale 0.3571936 0.24 1.71 position -0.06 
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

            bool tmp = terminalNO.TrySetParent(cruiserNO);
            CTPlugin.mls.LogInfo("tmp is = " + tmp);

            terminal.name = "Cruiser Terminal";

            ///terminalPosition
            GameObject terminalPosition = new GameObject("terminalPosition");
            terminalPosition.transform.parent = cruiser.transform;
            terminalPosition.transform.localPosition = new Vector3(1.293f, 0.938f, -3.274f);
        }
    }
}
