using System;
using _Scripts.CustomEvents.BoolEvent;
using _Scripts.CustomEvents.VoidEvents;
using UnityEngine;

namespace _Scripts
{
    public class MouseRaycasting : MonoBehaviour
    {
        [SerializeField] private bool _debug;
        [SerializeField] private Transform _effectTransform;
        [SerializeField] private float _lerpSpeed;
        [SerializeField] private BoolEvent _effectToggleEvent;

        private Vector3 _cursorWorldPos;
        private bool _wasClickingBefore = false;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            bool isClickingNow = Input.GetMouseButton(0);

            if (isClickingNow)
            {
                Vector3 screenPos = Input.mousePosition;

                Ray ray = _camera.ScreenPointToRay(screenPos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (_debug)
                    {
                        Debug.Log("Hit point: " + hit.point);
                    }
                    
                    _cursorWorldPos = hit.point;
                    // lerp the old position of the highlight to this position (done later)  
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
                    break;

                case (true, true):
                    HeldClick();
                    break;

                case (false, true):
                    EndedClick();
                    return;
                default:
                    break;
            }

            LerpToCursorPos(_effectTransform);
        }

        private void LerpToCursorPos(Transform target)
        {
            target.position = Vector3.Lerp(target.position, _cursorWorldPos, _lerpSpeed * Time.deltaTime);
        }

        private void StartedClick()
        {
            _effectToggleEvent.Raise(true);
            // Start the VFX
        }

        private void HeldClick()
        {
            // Perhaps this is a do nothing?
        }

        private void EndedClick()
        {
            _effectToggleEvent.Raise(false);
            // End the VFX
        }
    }
}