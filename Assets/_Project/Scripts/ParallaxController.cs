using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class ParallaxController : MonoBehaviour
    {
        [SerializeField] Transform[] backgrounds;
        [SerializeField] float smoothing = 10f;
        [SerializeField] float multiplier = 15f; // How different the speeds will be between layers

        Transform cam; // Main camera
        Vector3 previousCamPos;

        private void Awake()
        {
            cam = Camera.main.transform;
        }

        private void Start()
        {
            previousCamPos = cam.position;
        }

        private void Update()
        {
            for (var i = 0; i < backgrounds.Length; i++)
            {
                var parallax = (previousCamPos.y - cam.position.y) * (i * multiplier);
                var targetY = backgrounds[i].position.y + parallax;

                var targetPosition = new Vector3(backgrounds[i].position.x, targetY, backgrounds[i].position.z);

                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetPosition, smoothing * Time.deltaTime);
            }
            previousCamPos = cam.position;
        }
    }
}
