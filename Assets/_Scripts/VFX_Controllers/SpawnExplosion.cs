using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

namespace _Scripts.VFX_Controllers
{
    public class SpawnExplosion : MonoBehaviour
    {
        [SerializeField] private VisualEffect _explosionPrefab;
        [SerializeField] private float _cleanupTime;

        public void SpawnEffect(Vector3 spawnPos)
        {
            VisualEffect explosionInstance = Instantiate(_explosionPrefab, spawnPos, Quaternion.identity);

            bool selfDestructEnabled = explosionInstance.GetComponent<SelfDestructTimer>();

            if (!selfDestructEnabled)
            {
                SelfDestructTimer thing = explosionInstance.AddComponent<SelfDestructTimer>();
                thing.KillOnTimer(_cleanupTime);
            }
        }
    }
}