using Unity.Netcode;
using UnityEngine;

namespace CruiserTerminal
{
    public class CruiserTerminal : NetworkBehaviour
    {
        public bool cruiserTerminalInUse;

        //update
        public GameObject canvasMainContainer;

        //start
        public Terminal terminalScript;

        public AudioSource cruiserTerminalAudio;
        public AudioClip[] cruiserKeyboardClips;
        public AudioClip[] cruiserSyncedAudios;

        private void Start()
        {
            cruiserTerminalInUse = false;

            canvasMainContainer = CloneCanvas();
            canvasMainContainer.SetActive(false);

            terminalScript = GameObject.Find("Environment/HangarShip/Terminal/TerminalTrigger/TerminalScript").GetComponent<Terminal>();

            cruiserTerminalAudio = terminalScript.terminalAudio;
            cruiserKeyboardClips = terminalScript.keyboardClips;
            cruiserSyncedAudios = terminalScript.syncedAudios;
        }

        private void Update()
        {
            if (cruiserTerminalInUse)
            {
                Destroy(canvasMainContainer);
                canvasMainContainer = CloneCanvas();
            }
        }

        internal GameObject FindCanvas()
        {
            return GameObject.Find("Environment/HangarShip/Terminal/Canvas/MainContainer");
        }

        internal GameObject CloneCanvas()
        {
            return Instantiate(FindCanvas(), GameObject.Find("Cruiser Terminal/Canvas").transform);
        }

        public void BeginUsingCruiserTerminal()
        {
            canvasMainContainer.SetActive(true);
            cruiserTerminalInUse = true;
            terminalScript.BeginUsingTerminal();
        }

        public void StopUsingCruiserTerminal()
        {
            canvasMainContainer.SetActive(false);
            cruiserTerminalInUse = false;
            terminalScript.SetTerminalNoLongerInUse();
        }


    }
}
