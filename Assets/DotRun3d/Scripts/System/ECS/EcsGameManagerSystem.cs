using DonRun3D.ECS.LevelContructor;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace DonRun3D.ECS
{
    sealed class EcsGameManagerSystem : IEcsRunSystem
    {
        private DiContainer _container;

        public EcsGameManagerSystem(DiContainer container)
        {
            _container = container;
        }

        public void Run(IEcsSystems systems)
        {
            if (systems.Have<EcsStartupLoadEnd>())
                systems.AddComp<EcsGameManagerComponent, ECreateLevelComp>();

            systems.Delete<EcsStartupLoadEnd>();

        }

    }
}