using DotRun3d.Player;
using Leopotam.EcsLite;
using Zenject;

namespace DonRun3D.ECS.Player
{

    sealed class ESpawnPlayerSystem : IEcsRunSystem
    {

        private DiContainer _container;
        public ESpawnPlayerSystem(DiContainer container)
        {
            _container = container;
        }
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            if (!IsCreate(systems))
                return;

            var pool = world.GetPool<EPlayerComp>();

            var _playerView =  _container.Resolve<IPlayerView>();
            var ecsIndex = world.NewEntity();

            ref EPlayerComp pl = ref pool.Add(ecsIndex);
            pl.playerView = _playerView;
        }

        private bool IsCreate(IEcsSystems systems)
        {
            return systems.Have<EPlayerCreate>();
        }
    }
}
