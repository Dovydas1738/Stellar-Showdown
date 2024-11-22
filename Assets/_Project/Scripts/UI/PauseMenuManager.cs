using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Eflatun.SceneReference;

namespace Shmup
{
    public class GamePauseManager : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenuUI;
        [SerializeField] Slider masterVolumeSlider;
        [SerializeField] Slider musicVolumeSlider;
        [SerializeField] SceneReference mainMenuScene;

        AudioSource musicSource;

        bool isPaused = false;

        private void Start()
        {
            musicSource = BGM.instance.GetComponent<AudioSource>();
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);

            SetVolume(PlayerPrefs.GetFloat("MasterVolume", 1f));
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));

            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");

            if (masterVolumeSlider.value == 0 && musicVolumeSlider.value == 0)
            {
                masterVolumeSlider.value = 1f;
                musicVolumeSlider.value = 1f;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }

        public void SetVolume(float volume)
        {
            AudioListener.volume = volume;
            PlayerData.instance.SaveVolume(volume);
        }

        public void SetMusicVolume(float volume)
        {
            volume = Mathf.Clamp(volume, 0f, 1f);
            musicSource.volume = volume;
            PlayerData.instance.SaveMusicVolume(volume);
        }

        public void ReturnToMainMenu()
        {
            PlayerData.instance.DisableBGM();
            PlayerData.instance.extraEnemies = 0;
            PlayerData.instance.SaveExtraEnemies();

            Loader.Load(mainMenuScene);
        }
    }
}
