using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

namespace _Scripts.VFX_Controllers
{
    public class Meteor : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _motionCurve;
        [SerializeField] private float _animDuration;
        [SerializeField] private UnityEvent _meteorDone;
        [SerializeField] private UnityEvent<Vector3> _meteorDonePosition;

        private Vector3 _spawnPos;
        private Vector3 _targetPos;

        private float _animationTimer = 0.0f;
        private bool _loopedOnce = false;

        public void SetupMeteor(Vector3 spawnPos, Vector3 targetPos)
        {
            _spawnPos = spawnPos;
            this.transform.position = _spawnPos;

            _targetPos = targetPos;
            Vector3 lookAtVec = _targetPos - spawnPos;
            Quaternion rot = Quaternion.LookRotation(lookAtVec, Vector3.up);
            this.transform.rotation = rot;

            _animationTimer = 0.0f;
            _loopedOnce = false;
        }

        private void Update()
        {
            if (_animationTimer > _animDuration)
            {
                EndMeteor();
                _loopedOnce = true;
                return;
            }

            _animationTimer += Time.deltaTime;

            float normalizedAnimTime = _animationTimer / _animDuration;
            float lerpT = _motionCurve.Evaluate(normalizedAnimTime);

            this.transform.position = Vector3.Lerp(_spawnPos, _targetPos, lerpT);
        }

        private void EndMeteor()
        {
            if (_loopedOnce)
            {
                return;
            }
            
            _meteorDone.Invoke();
            _meteorDonePosition.Invoke(_targetPos);

            Destroy(this.gameObject,0.05f);
        }
    }
}