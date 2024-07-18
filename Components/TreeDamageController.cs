using GameNetcodeStuff;
using UnityEngine;

namespace HitIt.Components
{
    internal class TreeDamageController : MonoBehaviour, IHittable
    {
        public int HP = 8;

        public bool Hit(int force, Vector3 hitDirection, PlayerControllerB playerWhoHit = null, bool playHitSFX = false, int hitID = -1)
        {
            HP -= force;
            HitItCore.Instance.mls.LogInfo("Hit: " + name + " - pos: " + transform.position + " - rot: " + transform.rotation);

            if (HP <= 0 )
            {
                RoundManager.Instance.DestroyTreeOnLocalClient(base.transform.position);
            }

            return true;
        }

        private void Start()
        {
            HitItCore.Instance.mls.LogInfo(name);
        }
    }
}
