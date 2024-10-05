using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CruiserTerminal
{
    public class CruiserTerminal : NetworkBehaviour
    {
        public bool cruiserTerminalInUse;
        public GameObject canvasMainContainer;
        public Terminal terminalScript;
        public GameObject cruiserTerminal;
        public InteractTrigger interactTrigger;
        public AudioSource cruiserTerminalAudio;
        public AudioClip[] cruiserKeyboardClips;
        public AudioClip[] cruiserSyncedAudios;
        public PlayerActions playerActions;
        public GameObject terminalPos;
        public AudioClip enterTerminalSFX;
        public AudioClip leaveTerminalSFX;
        public Light terminalLight;

        private float timeSinceLastKeyboardPress;

        private void Awake()
        {
            playerActions = new PlayerActions();
            playerActions.Movement.Enable();
        }

        private void Start()
        {
            cruiserTerminal = GameObject.Find("Cruiser Terminal");
            terminalPos = GameObject.Find("terminalPosition");

            interactTrigger = base.gameObject.GetComponent<InteractTrigger>();

            cruiserTerminalInUse = false;

            canvasMainContainer = CloneCanvas();
            canvasMainContainer.SetActive(false);

            terminalScript = GameObject.Find("Environment/HangarShip/Terminal/TerminalTrigger/TerminalScript").GetComponent<Terminal>();

            enterTerminalSFX = terminalScript.enterTerminalSFX;
            leaveTerminalSFX = terminalScript.leaveTerminalSFX;

            //cruiserTerminalAudio = terminalScript.terminalAudio;
            cruiserKeyboardClips = terminalScript.keyboardClips;
            cruiserSyncedAudios = terminalScript.syncedAudios;

            terminalLight = GameObject.Find("Cruiser Terminal/terminalLight").GetComponent<Light>();
        }

        private void Update()
        {
            cruiserTerminal.transform.position = terminalPos.transform.position;
            cruiserTerminal.transform.rotation = terminalPos.transform.rotation;

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
            cruiserTerminalInUse = true;
            StartCoroutine(waitUntilFrameEndToSetActive(true));
            terminalScript.BeginUsingTerminal();

            terminalLight.enabled = true;
            cruiserTerminalAudio.PlayOneShot(enterTerminalSFX);
        }

        public void StopUsingCruiserTerminal()
        {
            cruiserTerminalInUse = false;
            StartCoroutine(waitUntilFrameEndToSetActive(false));
        }

        public void QuitCruiserTerminal()
        {
            terminalScript.QuitTerminal();
            interactTrigger.StopSpecialAnimation();

            terminalLight.enabled = false;
            cruiserTerminalAudio.PlayOneShot(leaveTerminalSFX);
        }

        private void OnEnable()
        {
            playerActions.Movement.OpenMenu.performed += PressESC;
        }

        private void OnDisable()
        {
            CTPlugin.mls.LogInfo("Terminal disabled, disabling ESC key listener");
            playerActions.Movement.OpenMenu.performed -= PressESC;
        }

        private void PressESC(InputAction.CallbackContext context)
        {
            if (context.performed && cruiserTerminalInUse)
            {
                QuitCruiserTerminal();
            }
        }

        private IEnumerator waitUntilFrameEndToSetActive(bool active)
        {
            yield return new WaitForEndOfFrame();
            canvasMainContainer.SetActive(active);

        }
    }
}
