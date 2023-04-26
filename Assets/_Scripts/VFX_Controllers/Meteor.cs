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

        private Vector3 _spawnPos;
        private Vector3 _targetPos;

        private float _animationTimer = 0.0f;
        
        public void SetupMeteor(Vector3 spawnPos, Vector3 targetPos)
        {
            _spawnPos = spawnPos;
            this.transform.position = _spawnPos;
            
            _targetPos = targetPos;
            Vector3 lookAtVec = _targetPos - spawnPos;
            Quaternion rot = Quaternion.LookRotation(lookAtVec, Vector3.up);
            this.transform.rotation = rot;
            
            _animationTimer = 0.0f;
        }

        private void Update()
        {
            if (_animationTimer > _animDuration)
            {
                EndMeteor();
                return;
            }
            
            _animationTimer += Time.deltaTime;

            float normalizedAnimTime = _animationTimer / _animDuration;
            float lerpT = _motionCurve.Evaluate(normalizedAnimTime);

            this.transform.position = Vector3.Lerp(_spawnPos, _targetPos, lerpT);
        }

        private void EndMeteor()
        {
            _meteorDone.Invoke();
            Destroy(this.gameObject,5.0f);
        }
    }
}