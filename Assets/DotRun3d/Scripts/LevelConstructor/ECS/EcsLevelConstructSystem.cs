using System.Collections.Generic;
using DonRun3D.World.Column;
using Leopotam.EcsLite;
using UnityEngine;

namespace DonRun3D.ECS.LevelContructor
{
    sealed class EcsLevelConstructSystem : EcsBaseLevelConstructorSystem, IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            if (systems.Have<ECreateLevelComp>())
            {
                InitLevel();
                systems.Delete<ECreateLevelComp>();
                return;
            }
        }

        private void InitLevel()
        {
            SetColors(CreateLevel());
            ecsWorld.GetFirstComp<EcsGameManagerComponent>(out var managerComponent);
            managerComponent.gameData.tapLine = 1;
        }

        private List<ColumnContainer> CreateLevel()
        {
            List<ColumnContainer> containers = new();
            //var _cameraManager = _container.Resolve<CameraManager>();
            var startPos = cameraManager.StartSeePosition + 2f;

            Vector3 position = new Vector3(0,0, startPos);
            var lines = Mathf.CeilToInt( (cameraManager.EndSeePostion - startPos) / columns.Offset) + 2;
            var count = lines * GameData.COUNT_LINE;
            var step = 2;
            var currentLine = 0;
            for (int i = 0; i < count; i++)
            {
                var countainer = worldFactory.Create(position);
                containers.Add(countainer);
                countainer.line = currentLine;
                var d = i % step;
                if(d == 0 && i != 0)
                {
                    position.z += columns.Offset;
                    position.x = 0;
                    step += GameData.COUNT_LINE;
                    currentLine++;
                    continue;
                }

                position.x += columns.Offset;
            }

            return containers;
        }

  
    }
}