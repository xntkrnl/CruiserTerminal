using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace CruiserTerminal
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class CTPlugin : BaseUnityPlugin
    {
        // Mod Details
        private const string modGUID = "mborsh.CruiserTerminal";
        private const string modName = "CruiserTerminal";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        public static ManualLogSource mls;

        public static AssetBundle mainAssetBundle;

        private static CTPlugin Instance;

        void Awake()
        {
            Instance = this;
            mls = BepInEx.Logging.Logger.CreateLogSource("Cruiser Terminal");
            mls = Logger;

            mls.LogInfo("Cruiser Terminal loaded. Patching.");
            harmony.PatchAll(typeof(CTPatches));

            if (!LoadAssetBundle())
            {
                mls.LogError("Failed to load asset bundle! Abort mission!");
                return;
            }

            bool LoadAssetBundle()
            {
                mls.LogInfo("Loading AssetBundle...");
                string sAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                mainAssetBundle = AssetBundle.LoadFromFile(Path.Combine(sAssemblyLocation, "CruiserTerminal"));

                if (mainAssetBundle == null)
                    return false;

                mls.LogInfo($"AssetBundle {mainAssetBundle.name} loaded from {sAssemblyLocation}.");
                return true;
            }
        }
    }
}
