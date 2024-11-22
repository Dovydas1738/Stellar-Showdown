using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shmup
{
    [RequireComponent(typeof (AudioSource))]
    public class Boss : MonoBehaviour
    {
        [SerializeField] GameObject explosionPrefab;
        [Header("Spawn VFX")]
        [SerializeField] GameObject spawnPortalVFX;
        [SerializeField] AudioClip spawnSFX;
        float spawnTime = 1f;

        [Header("Destroy SFX")]
        [SerializeField] AudioClip explosionClip;
        [SerializeField] float explosionVolume = 0.4f;

        [SerializeField] float maxHealth = 100f;
        float health;

        BossSpawner bossSpawner;
        Collider bossCollider;
        AudioSource soundPlayer;

        public List<BossStage> Stages;
        int currentStage = 0;

        private void Awake()
        {
            health = maxHealth;
            bossCollider = GetComponent<Collider>();
            soundPlayer = GetComponent<AudioSource>();
        }

        private void Start()
        {
            bossCollider.enabled = true;

            StartCoroutine(HandlePortalVFX());

            // Handle stages

            InitializeStage();
        }

        public float GetHealthNormalized() => health/maxHealth;

        void CheckStageComplete()
        {
            if (Stages[currentStage].IsStageComplete())
            {
                AdvanceToNextStage();
            }
        }

        void AdvanceToNextStage()
        {
            currentStage++;
            bossCollider.enabled = true;

            if(currentStage < Stages.Count)
            {
                InitializeStage();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.collider.CompareTag("Player"))
            {
                if (health <= 0)
                {
                    BossDefeated();
                    Destroy(collision.collider.gameObject);
                }
                else
                {
                    health -= 10;
                    Destroy(collision.collider.gameObject);
                }
            }
        }

        void BossDefeated()
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Debug.Log("Boss defeated");
            GameManager.Instance.AddScore(10);

            var audioPlayer = new GameObject("ExplosionSoundPlayer");
            var audioSource = audioPlayer.AddComponent<AudioSource>();
            audioSource.volume = explosionVolume;
            audioSource.clip = explosionClip;
            audioSource.Play();
            Destroy(audioPlayer, explosionClip.length);


            Destroy(gameObject);
            
        }

        public void InitializeStage()
        {
            Debug.Log("STAGE INITIALIZED");
            foreach (var stage in Stages)
            {
                foreach (var system in stage.enemySystems)
                {
                    system.OnSystemDestroyed.AddListener(CheckStageComplete);
                }
            }

            Stages[currentStage].InitializeStage();
            bossCollider.enabled = !Stages[currentStage].IsBossInvulnerable;
        }

        IEnumerator HandlePortalVFX()
        {
            var portal = Instantiate(spawnPortalVFX, transform.position.With(z: -5f), Quaternion.identity);
            portal.transform.localScale = new Vector3(2f, 2f, 2f);

            soundPlayer.clip = spawnSFX;
            soundPlayer.Play();

            yield return new WaitForSeconds(spawnTime);

            Destroy(portal);
        }
    }
}
