using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] Item[] itemPrefabs;
        [SerializeField] float spawnInterval = 3f;
        [SerializeField] float spawnRadius = 3f;

        void Start()
        {
            Timing.RunCoroutine(SpawnItems());
        }

        private void OnDestroy()
        {
            Timing.KillCoroutines();
        }

        IEnumerator<float> SpawnItems()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(spawnInterval);
                var item = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)]);
                item.transform.position = new Vector3 (transform.position.x + Random.insideUnitSphere.x, transform.position.y + Random.insideUnitSphere.y, 0) * spawnRadius;
            }
        }
    }
}
