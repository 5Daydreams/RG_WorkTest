using System;
using _Scripts.CustomEvents.BoolEvent;
using _Scripts.CustomEvents.VoidEvents;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts
{
    public class MouseCastController : MonoBehaviour
    {
        [SerializeField] private bool _debug;
        [SerializeField] private Transform _targetEffectTransform;
        [SerializeField] private float _lerpSpeed;
        
        [SerializeField] private UnityEvent<bool> _effectToggleEvent;
        [SerializeField] private UnityEvent<Vector3> _endClickEvent;

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
                case (true, false):
                    StartedClick();
                    break;

                case (true, true):
                    // HeldClick();
                    break;

                case (false, true):
                    EndedClick();
                    return;

                case (false, false):
                    // NoClick();
                    return;
            }

            LerpToCursorPos(_targetEffectTransform, _lerpSpeed * Time.deltaTime);
        }

        private void LerpToCursorPos(Transform target, float lerpT)
        {
            target.position = Vector3.Lerp(target.position, _cursorWorldPos, lerpT);
        }

        private void StartedClick()
        {
            _targetEffectTransform.position = _cursorWorldPos;
            _effectToggleEvent.Invoke(true);
            // Start the channeling VFX
        }

        private void HeldClick()
        {
            // Perhaps this is a do nothing?
            // I am thinking if there is a behavior which might be required to loop while the cast is held.
            // Not able to think of anything just yet
        }

        private void EndedClick()
        {
            _effectToggleEvent.Invoke(false);
            _endClickEvent.Invoke(_cursorWorldPos);
            // End the player's casting and the targeting VFX 
        }

        private void NoClick()
        {
            // Perhaps this is a do nothing?
            // I am thinking if there is a behavior which might be required to loop while the cast is not held.
            // Not able to think of anything just yet
        }
    }
}