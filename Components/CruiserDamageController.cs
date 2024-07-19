using GameNetcodeStuff;
using HitIt.Patches;
using UnityEngine;

namespace HitIt.Components
{
    internal class CruiserDamageController : MonoBehaviour, IHittable
    {
        private VehicleController vehicle;
        public void connect(VehicleController cruiser)
        {
            vehicle = cruiser;
        }

        public bool BulletHit(int force, Vector3 hitDirection, PlayerControllerB playerWhoHit = null, bool playHitSFX = false, int hitID = -1)
        {
            return DamageWithKnockback(force, hitDirection, HitItCore.enableCruiserBulletKnockback.Value, vehicle.transform.position - (hitDirection * 5), playerWhoHit, playHitSFX, hitID);
        }

        public bool Hit(int force, Vector3 hitDirection, PlayerControllerB playerWhoHit = null, bool playHitSFX = false, int hitID = -1)
        {
            bool hasPlayer = playerWhoHit != null;

            return DamageWithKnockback(force, hitDirection, hasPlayer, hasPlayer ? playerWhoHit.transform.position : Vector3.zero, playerWhoHit, playHitSFX, hitID);
        }

        public bool DamageWithKnockback(int force, Vector3 hitDirection, bool doKnockback, Vector3 knockbackPos, PlayerControllerB playerWhoHit = null, bool playHitSFX = false, int hitID = -1)
        {
            if (vehicle != null)
            {
                VehicleControllerPatch.DealPermanentDamage(vehicle, force);

                if (HitItCore.enableCruiserKnockback.Value && doKnockback)
                {
                    vehicle.PushTruckServerRpc(knockbackPos, hitDirection);
                }
                return true;
            }
            return false;
        }
    }
}
