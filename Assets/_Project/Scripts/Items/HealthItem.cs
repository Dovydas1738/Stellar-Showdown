using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class HealthItem : Item
    {
        [SerializeField] float pickupSFXVolume = 0.4f;
        [SerializeField] AudioClip pickupSFX;

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<Player>().AddHealth((int)amount);

            var audioPlayer = new GameObject("HitSoundPlayer");
            var audioSource = audioPlayer.AddComponent<AudioSource>();
            audioSource.volume = pickupSFXVolume;
            audioSource.clip = pickupSFX;
            audioSource.Play();
            Destroy(audioPlayer, pickupSFX.length);

            Destroy(gameObject);
        }
    }
}
