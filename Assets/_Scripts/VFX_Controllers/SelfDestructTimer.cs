using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.VFX_Controllers
{
    public class SelfDestructTimer : MonoBehaviour
    {
        [SerializeField] private float _cleanupTime;
        private bool _destroyRequested = false;
        
        public void KillOnTimer(float time)
        {
            _cleanupTime = time;
            RequestDestroy();
        }

        private void RequestDestroy()
        {
            if (_destroyRequested)
            {
                return;
            }

            _destroyRequested = true;
            Destroy(this.gameObject, _cleanupTime);
        }
    }
}