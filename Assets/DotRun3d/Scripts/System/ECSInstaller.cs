using Leopotam.EcsLite;
using Zenject;

namespace DonRun3D.System 
{
    public class ECSInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var world = new EcsWorld();
            Container.Bind<EcsWorld>().FromInstance(world).AsSingle().NonLazy();
        }
    }
}