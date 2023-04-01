using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Zenject;

namespace DonRun3D
{
    public class StartGame : MonoBehaviour
    {
        const string UI = "UI", Game = "Game";

        [SerializeField] SceneContext sceneContext;
        IEnumerator Start()
        {
            SplashScreen.Begin();
            SplashScreen.Draw();

            sceneContext.Run();
            yield return SceneManager.LoadSceneAsync(UI, LoadSceneMode.Additive);
            SplashScreen.Draw();
            yield return SceneManager.LoadSceneAsync(Game, LoadSceneMode.Additive);

            Destroy(gameObject);
        }


    }
}