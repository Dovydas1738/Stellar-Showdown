using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace Shmup
{
    public class Enemy : Spaceship
    {
        [SerializeField] GameObject explosionPrefab;
        [SerializeField] AudioClip explosionSFX;
        [SerializeField] float explosionVolume = 0.4f;

        protected override void Die()
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            OnSystemDestroyed?.Invoke();

            var audioPlayer = new GameObject("HitSoundPlayer");
            var audioSource = audioPlayer.AddComponent<AudioSource>();
            audioSource.volume = explosionVolume;
            audioSource.clip = explosionSFX;
            audioSource.Play();
            Destroy(audioPlayer, explosionSFX.length);

            Destroy(gameObject);
            GameManager.Instance.AddScore(10);
        }

        public UnityEvent OnSystemDestroyed;
    }
}
