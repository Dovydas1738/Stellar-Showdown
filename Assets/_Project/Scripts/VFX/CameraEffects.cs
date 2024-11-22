using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Shmup
{
    public class CameraEffects : MonoBehaviour
    {
        public IEnumerator TransitionEffect(float duration, float magnitude)
        {
            Vector3 originalPosition = transform.position;

            float elapsed = 0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-0.1f, 0.1f) * magnitude;
                float y = Random.Range(-0.1f, 0.1f) * magnitude;

                transform.position = new Vector3(x, transform.position.y + y, transform.position.z);

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.position = originalPosition;
        }
    }
}
