using GameNetcodeStuff;
using HarmonyLib;
using HitIt.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HitIt.Patches
{
    [HarmonyPatch(typeof(ShotgunItem))]
    internal class ShotgunItemPatch
    {
        [HarmonyPatch("ShootGun")]
        [HarmonyPrefix]
        public static void patchShootGun(Vector3 shotgunPosition, Vector3 shotgunForward, ShotgunItem __instance)
        {
            if (HitItCore.shouldShotgunDamageCruiser.Value)
            {
                RaycastHit[] vehicleColliders = new RaycastHit[10];
                Ray ray = new Ray(shotgunPosition - shotgunForward * 10f, shotgunForward);

                int num3 = Physics.SphereCastNonAlloc(ray, 5f, vehicleColliders, 15f, StartOfRound.Instance.collidersAndRoomMaskAndDefault);

                for (int i = 0; i < num3; i++)
                {
                    if (__instance.playerHeldBy != null && vehicleColliders[i].transform.TryGetComponent<VehicleController>(out VehicleController vehicle))
                    {
                        if (vehicle.currentDriver == __instance.playerHeldBy || vehicle.currentPassenger == __instance.playerHeldBy)
                        {
                            continue;
                        }
                    }
                    if (!Physics.Linecast(shotgunPosition, vehicleColliders[i].point, out RaycastHit hitInfo, StartOfRound.Instance.collidersAndRoomMaskAndDefault) && vehicleColliders[i].collider.TryGetComponent<CruiserDamageController>(out CruiserDamageController cdc))
                    {
                        float num4 = Vector3.Distance(shotgunPosition, vehicleColliders[i].point);
                        int force = ((num4 < 3.7f) ? 5 : ((!(num4 < 6f)) ? 2 : 3));

                        cdc.BulletHit(force, shotgunForward, __instance.playerHeldBy, playHitSFX: true);
                    }
                }
            }
        }
    }
}
