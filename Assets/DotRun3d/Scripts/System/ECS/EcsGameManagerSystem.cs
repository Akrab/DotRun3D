using DonRun3D.ECS.LevelContructor;
using DonRun3D.World.Column;
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


    sealed class EcsClickToObjSystem : IEcsRunSystem
    {
        private DiContainer _container;

        public EcsClickToObjSystem(DiContainer container)
        {
            _container = container;
        }

        public void Run(IEcsSystems systems)
        {
            if (!systems.Have<EcsClick>())
                return;
            
            EcsClick comp;  
            systems.GetFirstComp(out comp);
            var cl = comp.clickable as ColumnClick;
            Debug.LogError(cl.gameObject.name);
            systems.Delete<EcsClick>();
        }
    }
}