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

        public bool Hit(int force, Vector3 hitDirection, PlayerControllerB playerWhoHit = null, bool playHitSFX = false, int hitID = -1)
        {
            if (vehicle != null)
            {
                VehicleControllerPatch.DealPermanentDamage(vehicle, force);

                if (playerWhoHit != null)
                {
                    vehicle.PushTruckServerRpc(playerWhoHit.transform.position, hitDirection);
                }
                return true;
            }
            return false;
        }
    }
}
