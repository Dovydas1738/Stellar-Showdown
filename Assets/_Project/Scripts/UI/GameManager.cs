using Eflatun.SceneReference;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shmup
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] SceneReference mainMenuScene;
        [SerializeField] SceneReference levelScene;
        [SerializeField] GameObject gameOverUI;
        [SerializeField] BossSpawner bossSpawner;
        [SerializeField] CameraEffects cameraEffects;
        [SerializeField] PostProcessController postProcessController;

        [SerializeField] EnemySpawner enemySpawner;
        [SerializeField] GameObject bossPrefab;

        AudioSource audioSource;
        [Header("stage change SFX and VFX")]
        [SerializeField] AudioClip nextStageRumbleSFX;
        [SerializeField] AudioClip nextStageBuildupSFX;
        [SerializeField] AudioClip levelCompleted;

        SpriteRenderer spriteRenderer;
        Transform vignetteTransform;

        bool isTransitioning = false;

        public static GameManager Instance {  get; private set; }

        public Player Player => player;

        Player player;

        int score;

        float restartTimer = 3f;

        public bool IsGameOver()
        {
            if(player.GetHealthNormalized() <= 0 || player.GetFuelNormalized() <= 0)
            {
                PlayerData.instance.SaveHighScore(score);
                PlayerData.instance.extraEnemies = 0;
                PlayerData.instance.SaveExtraEnemies();

                return true;
            }
            else
            {
                return false;
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            if (PlayerData.instance.playerScore != 0)
            {
                score = PlayerData.instance.playerScore;
            }
        }

        private void Update()
        {
            if (IsGameOver())
            {
                restartTimer -= Time.deltaTime;

                if(gameOverUI.activeSelf == false)
                {
                    gameOverUI.SetActive(true);
                }
            }

            if (restartTimer <= 0)
            {
                if(restartTimer > -1f)
                {
                    Destroy(BGM.instance.gameObject);
                    PlayerData.instance.playerScore = 0;
                    Loader.Load(mainMenuScene);
                    restartTimer = -1f;
                }
            }

            CheckScore();

            CheckForEnemies();
        }

        public void AddScore(int amount)
        {
            score += amount;
            PlayerData.instance.playerScore = score;
        }
        public int GetScore() => score;

        // Check the score and spawn boss every 100 points
        void CheckScore()
        {
            if (GetScore() % 100 == 0 && GetScore() != 0 && bossSpawner.bossSpawned == false)
            {
                bossSpawner.SpawnBoss();
            }
        }

        IEnumerator NextStage()
        {
            StartCoroutine(cameraEffects.TransitionEffect(3f, 0.02f));

            if (isTransitioning) yield break;

            isTransitioning = true;

            PlayerData.instance.playerScore = score;
            StartCoroutine(postProcessController.LerpChromaticAbberation(0f, 1f, 3f));

            audioSource.PlayOneShot(nextStageBuildupSFX);
            audioSource.PlayOneShot(nextStageRumbleSFX);
            audioSource.PlayOneShot(levelCompleted);
            Debug.Log("NEXT STAGE");

            yield return new WaitForSeconds(3f);

            PlayerData.instance.extraEnemies++;
            PlayerData.instance.SaveExtraEnemies();

            Loader.Load(levelScene);

            isTransitioning = false;
        }

        void CheckForEnemies()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("SimpleEnemy");
            GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");

            if (enemies.Length == 0 && bosses.Length == 0 && enemySpawner.enemiesSpawned == enemySpawner.maxEnemies)
            {
                StartCoroutine(NextStage());
            }
        }

        private void OnApplicationQuit()
        {
            PlayerData.instance.extraEnemies = 0;
            PlayerData.instance.SaveExtraEnemies();
        }
    }
}
