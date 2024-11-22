using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class BossStage : MonoBehaviour
    {
        [SerializeField] bool isFinalStage = false;

        public List<Enemy> enemySystems;
        public GameObject bossHealthbar;

        public bool IsBossInvulnerable = true;

        public void InitializeStage()
        {
            if (isFinalStage)
            {
                bossHealthbar.SetActive(true);
            }

            Debug.Log("SYSTEMS ON");
            foreach (var system in enemySystems)
            {
                system.gameObject.SetActive(true);
            }
        }

        public bool IsStageComplete()
        {
            foreach (var system in enemySystems)
            {
                if (system != null && system.GetHealthNormalized() > 0 || isFinalStage)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
