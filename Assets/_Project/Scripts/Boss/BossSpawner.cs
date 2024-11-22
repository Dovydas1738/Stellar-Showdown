using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace Shmup
{
    public class BossSpawner : MonoBehaviour
    {
        [SerializeField] GameObject[] bossPrefabs;

        [SerializeField] SplineContainer bossSpline;

        public bool bossSpawned = false;

        public void SpawnBoss()
        {
            GameObject bossInstance = Instantiate(bossPrefabs[Random.Range(0, bossPrefabs.Length)]);

            Boss bossScript = bossInstance.GetComponent<Boss>();

            SplineAnimate splineAnimate = GameObject.FindGameObjectWithTag("Boss").GetComponent<SplineAnimate>();
            splineAnimate.Container = bossSpline;
            splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
            splineAnimate.ObjectUpAxis = SplineAnimate.AlignAxis.YAxis;
            splineAnimate.ObjectForwardAxis = SplineComponent.AlignAxis.ZAxis;
            splineAnimate.MaxSpeed = 1f;

            float randomPosition = Random.Range(0f, 6f);
            bossInstance.transform.position = bossSpline.EvaluatePosition(randomPosition); //doesn't work

            splineAnimate.Restart(true);

            bossSpawned = true;
        }
    }
}
