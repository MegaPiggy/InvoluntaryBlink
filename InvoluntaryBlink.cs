using OWML.Common;
using OWML.ModHelper;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace InvoluntaryBlink
{
    public class InvoluntaryBlink : ModBehaviour
    {
        public const float blinkTime = 0.5f;
        public const float animTime = blinkTime / 2f;

        private IEnumerator RunLoop()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
            var cameraEffectController = FindObjectOfType<PlayerCameraEffectController>();
            while (cameraEffectController != null)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
                cameraEffectController.CloseEyes(animTime);
                yield return new WaitForSeconds(animTime);
                GlobalMessenger.FireEvent("PlayerBlink");
                cameraEffectController.OpenEyes(animTime, false);
                yield return new WaitForSeconds(animTime);
                yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
            }
        }

        private void Start()
        {
            ModHelper.Console.WriteLine($"Involuntary Blink has loaded!", MessageType.Success);

            // Example of accessing game code.
            LoadManager.OnCompleteSceneLoad += (scene, loadScene) =>
            {
                if (loadScene != OWScene.SolarSystem) return;
                StartCoroutine(RunLoop());
            };
        }
    }
}
