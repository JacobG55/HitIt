using HarmonyLib;
using HitIt.Components;
using System;
using UnityEngine;

namespace HitIt.Patches
{
    [HarmonyPatch(typeof(VehicleController))]
    internal class VehicleControllerPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        public static void patchStart(VehicleController __instance)
        {
            CruiserDamageController cdc = __instance.gameObject.AddComponent<CruiserDamageController>();
            cdc.connect(__instance);
        }

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(VehicleController), "DealPermanentDamage")]
        public static void DealPermanentDamage(object instance, int damageAmount, Vector3 damagePosition = default(Vector3)) =>
            throw new NotImplementedException("It's a stub");
    }
}
