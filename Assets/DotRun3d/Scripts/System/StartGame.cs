using DonRun3D.ECS;
using Leopotam.EcsLite;
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
            SplashScreen.Draw();
            yield return null;
            yield return new WaitForEndOfFrame();

            var container = FindObjectOfType<SceneContext>().Container;
            var world = container.Resolve<EcsWorld>();

            var filter = world.Filter<EcsGameManagerComponent>().End();
            var pool2 = world.GetPool<EcsStartupLoadEnd>();

            foreach (int entity in filter)
                pool2.Add(entity);
            Destroy(gameObject);
        }


    }
}