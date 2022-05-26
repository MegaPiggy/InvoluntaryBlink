using OWML.Common;
using OWML.ModHelper;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace InvoluntaryBlink
{
    public class InvoluntaryBlink : ModBehaviour
    {
        private IEnumerator RunLoop()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
            var cameraEffectController = FindObjectOfType<PlayerCameraEffectController>();
            while (cameraEffectController != null)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
                cameraEffectController.CloseEyes(0);
                GlobalMessenger.FireEvent("PlayerBlink");
                cameraEffectController.OpenEyes(1, true);
                cameraEffectController._wakeLength = 0;
                cameraEffectController._waitForWakeInput = false;
                yield return new WaitForSeconds(1);
                yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
            }
        }

        private void Start()
        {
            // Starting here, you'll have access to OWML's mod helper.
            ModHelper.Console.WriteLine($"My mod {nameof(InvoluntaryBlink)} is loaded!", MessageType.Success);

            // Example of accessing game code.
            LoadManager.OnCompleteSceneLoad += (scene, loadScene) =>
            {
                if (loadScene != OWScene.SolarSystem) return;
                StartCoroutine(RunLoop());
            };
        }
    }
}
