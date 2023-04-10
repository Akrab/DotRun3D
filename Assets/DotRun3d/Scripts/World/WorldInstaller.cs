using DotRun3d.LevelConstructor;
using UnityEngine;
using Zenject;
namespace DonRun3D.World
{
    public class WorldInstaller : MonoInstaller<WorldInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<WorldFactory>().FromNew().AsSingle().NonLazy();
            var camManager = FindObjectOfType<CameraManager>();
            Container.Bind<CameraManager>().FromInstance(camManager).AsSingle().NonLazy();

        }
    }
}