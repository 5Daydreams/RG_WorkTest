using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

namespace _Scripts.VFX_Controllers
{
    public class ToggleTargetEffect : MonoBehaviour
    {
        [SerializeField] private VisualEffect _vfx;

        private void OnEnable()
        {
            _vfx.Reinit();
            _vfx.Stop();
        }

        public void ToggleResponse(bool value)
        {
            if (value)
            {
                _vfx.Reinit();
            }
            else
            {
                _vfx.Stop();
            }
        }
                                                
    }
}
