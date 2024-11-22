using Eflatun.SceneReference;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shmup
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] SceneReference startingLevel;
        [SerializeField] Button playButton;
        [SerializeField] Button quitButton;
        [SerializeField] Text highScoreText;
        [SerializeField] Slider musicVolumeSlider;
        [SerializeField] AudioSource musicPlayer;

        private void Awake()
        {
            playButton.onClick.AddListener(() => Loader.Load(startingLevel));
            quitButton.onClick.AddListener(() => Application.Quit());

            Time.timeScale = 1f;
        }

        private void Start()
        {
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);

            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1f));
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");

            int highScore = PlayerPrefs.GetInt("HighScore", 0);

            highScoreText.text = "HIGH SCORE: " + highScore.ToString();
        }

        public void SetMusicVolume(float volume)
        {
            volume = Mathf.Clamp(volume, 0f, 1f);
            musicPlayer.volume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);
            PlayerPrefs.Save();
        }

    }
}
