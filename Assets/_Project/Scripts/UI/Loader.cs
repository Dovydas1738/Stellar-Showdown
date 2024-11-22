using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Eflatun.SceneReference;
using MEC;

namespace Shmup
{
    public static class Loader
    {
        static SceneReference loadingScene = new SceneReference(SceneGuidToPathMapProvider.ScenePathToGuidMap["Assets/_Project/Scenes/LoadingScreen.unity"]);
        static SceneReference targetScene;

        public static void Load(SceneReference scene)
        {
            targetScene = scene;
            SceneManager.LoadScene("LoadingScreen");
            Timing.RunCoroutine(LoadTargetScene());
        }

        static IEnumerator<float> LoadTargetScene()
        {
            // Doesn't work for some reason

            Timing.WaitForSeconds(1f);
            Debug.Log("Loading scene: " + targetScene.Name);
            SceneManager.LoadScene(targetScene.Name);
            yield return 0;
        }
    }
}
