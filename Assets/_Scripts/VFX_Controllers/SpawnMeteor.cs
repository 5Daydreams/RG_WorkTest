using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

namespace _Scripts.VFX_Controllers
{
    public class SpawnMeteor : MonoBehaviour
    {
        [SerializeField] private Meteor _meteorPrefab;
        [SerializeField] private Transform _spawnOrigin;
        [SerializeField] private float _cooldown;

        private VisualEffect _portalInstance;

        private float _cooldownTimer = 0.0f;

        private bool isOnCooldown => _cooldownTimer > 0.0f;

        public void RequestCast(Vector3 targetPos)
        {
            if (isOnCooldown)
            {
                return;
            }

            Cast(targetPos);
        }

        private void Cast(Vector3 targetPos)
        {
            _cooldownTimer = _cooldown;

            Meteor meteorInstance = Instantiate(_meteorPrefab, _spawnOrigin.position, Quaternion.identity);
            meteorInstance.SetupMeteor(_spawnOrigin.position, targetPos);

            bool selfDestructEnabled = meteorInstance.GetComponent<SelfDestructTimer>();

            if (!selfDestructEnabled)
            {
                SelfDestructTimer thing = meteorInstance.AddComponent<SelfDestructTimer>();
                thing.KillOnTimer(_cooldown * 2.0f);
            }
        }

        private void Update()
        {
            if (!isOnCooldown)
            {
                return;
            }

            _cooldownTimer -= Time.deltaTime;
        }
    }
}