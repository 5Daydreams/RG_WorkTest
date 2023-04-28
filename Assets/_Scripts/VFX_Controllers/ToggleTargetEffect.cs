using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

namespace _Scripts.VFX_Controllers
{
    [RequireComponent(typeof(VisualEffect))]
    public class ToggleTargetEffect : MonoBehaviour
    {
        [SerializeField] private VisualEffect _vfx;

        private void OnEnable()
        {
            if (_vfx == null)
            {
                _vfx = this.GetComponent<VisualEffect>();
            }
            
            _vfx.Play();
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
                _vfx.SendEvent("Stop");
            }
        }
                                                
    }
}

