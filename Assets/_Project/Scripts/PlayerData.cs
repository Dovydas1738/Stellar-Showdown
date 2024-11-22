using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Shmup
{
    public class PlayerData : MonoBehaviour
    {
        public static PlayerData instance;

        AudioSource musicVolumeSource;

        public int playerScore = 0;
        public int extraEnemies = 0;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void DisableBGM()
        {
            Destroy(BGM.instance.gameObject);
        }

        public void SaveHighScore(int score)
        {
            int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);

            if(playerScore > currentHighScore)
            {
                PlayerPrefs.SetInt("HighScore", score);
                PlayerPrefs.Save();
            }
        }

        public void SaveVolume(float masterVolume)
        {
            PlayerPrefs.SetFloat("MasterVolume", masterVolume);
            PlayerPrefs.Save();
        }

        public void SaveMusicVolume(float musicVolume)
        {
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            PlayerPrefs.Save();
        }

        public void SaveExtraEnemies()
        {
            PlayerPrefs.SetInt("ExtraEnemies", extraEnemies);
            PlayerPrefs.Save();
        }
    }
}
