using System;
using UnityEngine;

namespace _Scripts
{
    public class MouseRaycasting : MonoBehaviour
    {
        private bool _wasClickingBefore = false;
        
        private void Update()
        {
            bool isClickingNow = Input.GetMouseButton(0);
            
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
