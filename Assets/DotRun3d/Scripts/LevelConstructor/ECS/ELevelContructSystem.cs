using Leopotam.EcsLite;
using Zenject;

namespace DonRun3D.ECS.LevelContructor
{
    sealed class ELevelContructSystem : IEcsRunSystem
    {
        private DiContainer _container;
        public ELevelContructSystem(DiContainer container)
        {
            _container = container;
        }
        public void Run(IEcsSystems systems)
        {
 
        }
    }
}