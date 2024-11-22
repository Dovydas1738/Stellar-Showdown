using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace Shmup
{
    public class PostProcessController : MonoBehaviour
    {
        PostProcessVolume volume;
        ChromaticAberration chromatic;

        private void Awake()
        {
            volume = GetComponent<PostProcessVolume>();
            volume.profile.TryGetSettings(out chromatic);
        }

        private void OnEnable()
        {
            chromatic.intensity.value = 1;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }


        public IEnumerator LerpChromaticAbberation(float from, float to, float duration)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                chromatic.intensity.value = Mathf.Lerp(from, to, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            StartCoroutine(LerpChromaticAbberation(1f, 0f, 2f));
        }
    }
}
