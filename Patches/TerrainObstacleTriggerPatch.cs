using HarmonyLib;
using HitIt.Components;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace HitIt.Patches
{
    [HarmonyPatch(typeof(TerrainObstacleTrigger))]
    internal class TerrainObstacleTriggerPatch
    {

        [HarmonyPatch("OnTriggerEnter")]
        [HarmonyPrefix]
        public static void patchTrigger(TerrainObstacleTrigger __instance)
        {
            if (__instance.transform.Find("TreeDamageController") == null)
            {
                int configDefault = HitItCore.Instance.defaultTreeHP;
                if (configDefault >= 0)
                {
                    HitItCore.Instance.mls.LogInfo("Creating Tree Damage Controller");

                    GameObject DamageController = new GameObject("TreeDamageController");
                    DamageController.transform.parent = __instance.transform;
                    DamageController.transform.localPosition = Vector3.zero;
                    DamageController.transform.localRotation = Quaternion.identity;

                    DamageController.layer = 21;

                    CapsuleCollider collider = DamageController.AddComponent<CapsuleCollider>();
                    collider.height = 8;
                    collider.direction = 1;
                    collider.radius = 0.55f;// 0.25f;

                    TreeDamageController tdc = DamageController.AddComponent<TreeDamageController>();
                    tdc.HP = configDefault;
                }
            }
        }
    }
}
