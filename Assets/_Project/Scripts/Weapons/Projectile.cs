using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    [RequireComponent (typeof (AudioSource))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] float hitVolume = 0.4f;
        [SerializeField] GameObject muzzlePrefab;
        [SerializeField] GameObject hitPrefab;
        [SerializeField] AudioClip[] shootClips;
        [SerializeField] AudioClip hitClip;

        AudioSource audioSource;
        Transform parent;

        public void SetSpeed(float speed) => this.speed = speed;
        public void SetParent(Transform parent) => this.parent = parent;

        public Action Callback;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            if (gameObject.CompareTag("Rocket"))
            {
                transform.up = transform.forward;
            }
        }

        void Start()
        {
            if (muzzlePrefab != null)
            {
                var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
                audioSource.clip = shootClips[UnityEngine.Random.Range(0, shootClips.Length - 1)];

                audioSource.PlayOneShot(audioSource.clip);

                muzzleVFX.transform.forward = gameObject.transform.forward;
                muzzleVFX.transform.SetParent(parent);

                DestroyParticleSystem(muzzleVFX);
            }
        }

        private void Update()
        {
            if(gameObject.CompareTag("Rocket"))
            {
                transform.SetParent(null);
                transform.position += transform.forward * (speed * Time.deltaTime);
            }
            else
            {
                transform.SetParent(null);
                transform.position += transform.up * (speed * Time.deltaTime);

            }
            Callback?.Invoke();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (hitPrefab != null)
            {
                ContactPoint contact = collision.contacts[0];
                var hitVFX = Instantiate(hitPrefab, contact.point, Quaternion.identity);

                var audioPlayer = new GameObject("HitSoundPlayer");
                var audioSource = audioPlayer.AddComponent<AudioSource>();
                audioSource.volume = hitVolume;
                audioSource.clip = hitClip;
                audioSource.Play();
                Destroy(audioPlayer, hitClip.length);

                DestroyParticleSystem (hitVFX);

                // if hit a spaceship, damage it

                var spaceship = collision.gameObject.GetComponent<Spaceship>();
                if (spaceship != null && spaceship.GetHealthNormalized() > 0)
                {
                    spaceship.TakeDamage(10);
                    Destroy(gameObject);
                }
            }
        }

        void DestroyParticleSystem(GameObject vfx)
        {
            var ps = vfx.GetComponent<ParticleSystem>();
            if (ps == null)
            {
                ps = vfx.GetComponentInChildren<ParticleSystem>();
            }
            Destroy(vfx, ps.main.duration);
        }
    }
}
