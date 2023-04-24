using System;
using UnityEngine;

namespace _Scripts
{
    public class MouseRaycasting : MonoBehaviour
    {
        [SerializeField] private bool _debug;
        private bool _wasClickingBefore = false;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            bool isClickingNow = Input.GetMouseButton(0);

            if (isClickingNow && _debug)
            {
                Vector3 screenPos = Input.mousePosition;
                
                Ray ray = Camera.main.ScreenPointToRay(screenPos);
                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Hit point: " + hit.point);
                    
                    // lerp the old position of the highlight to this position  
                }
            }
            
            HandleClicking(isClickingNow);

            // Previous state becomes current state
            _wasClickingBefore = isClickingNow;
        }

        private void HandleClicking(bool isClickingNow)
        {
            switch (isClickingNow, _wasClickingBefore)
            {
                case (false, false):
                    // Exit if no clicks had been recorded previously
                    return;

                case (true, false):
                    StartedClick();
                    return;

                case (true, true):
                    HeldClick();
                    return;

                case (false, true):
                    EndedClick();
                    return;
                default:
                    break;
            }
        }

        private void StartedClick()
        {
            // Start the VFX
        }

        private void HeldClick()
        {
            // Perhaps this is a do nothing?
        }

        private void EndedClick()
        {
            // End the VFX
        }
    }
}
