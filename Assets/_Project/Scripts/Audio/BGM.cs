using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    [RequireComponent(typeof (AudioSource))]
    public class BGM : MonoBehaviour
    {
        public static BGM instance;

        AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            audioSource.Play();
        }
    }
}
