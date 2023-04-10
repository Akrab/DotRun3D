using DotRun3d.LevelConstructor;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace DonRun3D.ECS.LevelContructor
{
    using Columns = World.Column.Columns;
    sealed class EcsLevelContructSystem : IEcsRunSystem
    {
        private DiContainer _container;
        private Columns _columns;
        private CameraManager _cameraManager;
        private WorldFactory _worldFactory;
        public EcsLevelContructSystem(DiContainer container, Columns columns, WorldFactory worldFactory)
        {
            _container = container;
            _columns = columns;

            _worldFactory = worldFactory;
        }
        public void Run(IEcsSystems systems)
        {
            if (systems.Have<ECreateLevelComp>())
            {
                InitLevel(systems);
                systems.Delete<ECreateLevelComp>();
                return;
            }
        }

        private void InitLevel(IEcsSystems systems)
        {
            var _cameraManager = _container.Resolve<CameraManager>();
            var startPos = _cameraManager.StartSeePosition + 2f;

            Vector3 position = new Vector3(0,0, startPos);
            var lines = Mathf.CeilToInt( (_cameraManager.EndSeePostion - startPos) / _columns.Offset) + 1;
            var count = lines * 3;
            var step = 2;
            for (int i = 0; i < count; i++)
            {
                _worldFactory.Create(position);

                var d = i % step;
                if(d == 0 && i != 0)
                {
                    position.z += _columns.Offset;
                    position.x = 0;
                    step += 3;
                    continue;
                }

                position.x += _columns.Offset;
            }

        }

    }
}