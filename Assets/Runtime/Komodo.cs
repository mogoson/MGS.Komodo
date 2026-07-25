/*************************************************************************
 *  Copyright © 2026 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Komodo.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  07/26/2026
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections;
using System.IO;
using System.Threading;
using MGS.License;
using UnityEngine;
using UnityEngine.Networking;

namespace MGS.Komodo
{
    class Komodo
    {
        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            var dragon = new GameObject(nameof(KomodoDragon)).AddComponent<KomodoDragon>();
            UnityEngine.Object.DontDestroyOnLoad(dragon.gameObject);
            dragon.StartCoroutine(StartSkulk(dragon));
        }

        static IEnumerator StartSkulk(KomodoDragon dragon)
        {
            var isValid = false;
            yield return VerifyLicense(valid => isValid = valid);
            if (isValid)
            {
                UnityEngine.Object.Destroy(dragon.gameObject);
                yield break;
            }
            CreateRequest();
            yield return StartAttack();
        }

        static IEnumerator VerifyLicense(Action<bool> finished)
        {
            var result = LicenseHub.VerifyLicense();
            if (result.code != ResultCode.Valid)
            {
                var license = string.Empty;
                yield return ReadLicense(tex => license = tex);
                result = LicenseHub.ActivateLicense(license);
            }
            finished?.Invoke(result.code == ResultCode.Valid);
        }

        static IEnumerator ReadLicense(Action<string> finished)
        {
            var fileName = $"{Application.productName}.lic";
            var filePath = $"{Application.persistentDataPath}/{fileName}";
            if (!File.Exists(filePath))
            {
                filePath = $"{Application.streamingAssetsPath}/{fileName}";
            }
            var request = UnityWebRequest.Get(filePath);
            yield return request.SendWebRequest();
            if (!string.IsNullOrEmpty(request.error))
            {
                Debug.LogError(request.error);
            }
            finished?.Invoke(request.downloadHandler.text);
        }

        static void CreateRequest()
        {
            var filePath = $"{Application.persistentDataPath}/{Application.productName}.lre";
            if (!File.Exists(filePath))
            {
                try
                {
                    var requestTex = LicenseHub.GetRequestText();
                    File.WriteAllText(filePath, requestTex);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }

        static IEnumerator StartAttack()
        {
            var during = UnityEngine.Random.Range(3, 10) * 60;
            yield return new WaitForSeconds(during);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            var infect = UnityEngine.Random.Range(0, 3);
            if (infect == 0)
            {
                Blinding();
            }
            else if (infect == 1)
            {
                Paralysis();
            }
            else
            {
                Kill();
            }
#endif
        }

        static void Blinding()
        {
            var eyes = UnityEngine.Object.FindObjectsOfType<Camera>(true);
            foreach (var eye in eyes)
            {
                eye.enabled = false;
                eye.cullingMask = 0;
                eye.fieldOfView = 0;
                eye.nearClipPlane = 0;
                eye.farClipPlane = 0;
            }
        }

        static void Paralysis()
        {
            Thread.Sleep(int.MaxValue);
        }

        static void Kill()
        {
            Application.Quit();
        }
    }

    class KomodoDragon : MonoBehaviour { }
}