using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shmup
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] Image healthBar;
        [SerializeField] GameObject enemyObject;
        [SerializeField] Vector3 offset;

        Transform enemyTransform;
        Enemy enemy;
        Boss boss;

        Camera mainCamera;

        private void Awake()
        {
            enemyTransform = enemyObject.GetComponent<Transform>();
            enemy = enemyObject.GetComponent<Enemy>();
            boss = enemyObject.GetComponent<Boss>();
        }

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (boss == null)
            {
                healthBar.fillAmount = enemy.GetHealthNormalized();
            }
            else if (enemy == null)
            {
                healthBar.fillAmount = boss.GetHealthNormalized();
            }

        }

        private void LateUpdate()
        {
            transform.position = enemyTransform.position + offset;

            transform.LookAt(Camera.main.transform);
        }
    }
}
