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
            var cruiser = GameObject.Find("CompanyCruiser(Clone)");

            var terminal = CTPlugin.mainAssetBundle.LoadAsset("terminal-cruiser.fbx") as GameObject;

            terminal = GameObject.Instantiate(terminal, cruiser.transform);
            terminal.name = "Cruiser Terminal";
            terminal.layer = cruiser.layer;
            terminal.transform.localPosition = new Vector3(1.3271f, 0.938f, -3.274f);
            terminal.transform.localScale = new Vector3(0.2f, 0.3f, 0.3f);

            CopyFromTerminal(terminal.transform);
        }

        static void CopyFromTerminal(Transform parentObj)
        {
            var go = GameObject.Find("Terminal").transform;
            GameObject canvas;
            GameObject TerminalTrigger;

            foreach (Transform child in go)
            {
                if (child.name == "Canvas" || child.name == "TerminalTrigger" || child.name == "terminalLight")
                {
                    GameObject copy = GameObject.Instantiate(child.gameObject, parentObj);
                    if (copy.name == "Canvas(Clone)")
                        canvas = copy;
                }
            }
        }
    }
}
