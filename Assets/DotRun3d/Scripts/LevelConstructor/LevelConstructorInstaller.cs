using UnityEngine;
using Zenject;
namespace DonRun3D.LevelConstructor
{
    public class LevelConstructorInstaller : MonoInstaller
    {

        [SerializeField] private LevelConstructorView levelConstructorView;
        public override void InstallBindings()
        {
            Container.Bind<ILevelConstructorView>().FromInstance(levelConstructorView).AsSingle().NonLazy();
        }
    }
}