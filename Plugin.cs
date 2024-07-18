using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using HitIt.Patches;

namespace HitIt
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class HitItCore : BaseUnityPlugin
    {
        private const string modGUID = "JacobG5.HitIt";
        private const string modName = "HitIt";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        public static HitItCore Instance;

        public int defaultTreeHP;

        internal ManualLogSource mls;

        public static ConfigEntry<bool> enableCruiserKnockback;
        public static ConfigEntry<int> treeHealth;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            enableCruiserKnockback = Config.Bind("Vehicle", "enableCruiserKnockback", true, "Makes it so when cruisers take damage from weapons they get pushed back. It's a bit much but it's also funny.");
            treeHealth = Config.Bind("Terrain", "treeHealth", 8, "How many hits with a shovel it takes to destroy a tree. Set to -1 to disable.");

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            defaultTreeHP = treeHealth.Value;

            harmony.PatchAll(typeof(VehicleControllerPatch));
            harmony.PatchAll(typeof(TerrainObstacleTriggerPatch));
        }
    }
}
