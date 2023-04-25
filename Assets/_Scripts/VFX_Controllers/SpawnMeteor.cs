using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

namespace _Scripts.VFX_Controllers
{
    public class SpawnMeteor : MonoBehaviour
    {
        [SerializeField] private VisualEffect _meteorPrefab;
        [SerializeField] private VisualEffect _portalPrefab;
        [SerializeField] private Transform _spawnOrigin;
        [SerializeField] private AnimationCurve _motionCurve;
        [SerializeField] private float _animDuration;
        [SerializeField] private float _cooldown;

        private Transform _meteorInstance;
        private Vector3 _targetPos;

        private float _cooldownTimer = 0.0f;

        private float _animationTimer = 0.0f;

        public void RequestCast(Vector3 targetPos)
        {
            if (_cooldownTimer > 0.0f)
            {
                return;
            }

            _targetPos = targetPos;
            Cast();
        }

        private void Cast()
        {
            _cooldownTimer = _cooldown;
            _animationTimer = 0.0f;

            _meteorInstance = Instantiate(_meteorPrefab, _spawnOrigin.position, Quaternion.identity).transform;
        }

        private void Update()
        {
            if (_animationTimer > _animDuration)
            {
                return;
            }

            if (_cooldownTimer >= 0.0f)
            {
                _cooldownTimer -= Time.deltaTime;
            }

            _animationTimer += Time.deltaTime;

            float normalizedAnimTime = _animationTimer / _animDuration;

            float lerpT = _motionCurve.Evaluate(normalizedAnimTime);

            _meteorInstance.position = Vector3.Lerp(_spawnOrigin.position, _targetPos, lerpT);
        }
    }
}