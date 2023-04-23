using System.Collections.Generic;
using System.Linq;
using DonRun3D.World.Column;
using DotRun3d.ECS.World;
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
            var columns = SetColors(CreateLevel());
            
            ecsWorld.GetFirstComp<EcsGameManagerComponent>(out var managerComponent);
            managerComponent.gameData.line = columns.Max(D => D.line);
            managerComponent.gameData.tapLine = 1;
        }

        private List<ILineContainer> CreateLevel()
        {
            var startPos = cameraManager.StartSeePosition + 2f;
            var lines = Mathf.CeilToInt((cameraManager.EndSeePostion - startPos) / columns.Offset) + 2;

            List<ILineContainer> containers = new(lines);

            Vector3 position = new Vector3(0, 0, startPos);
            for (int currentLine = 0; currentLine < lines; currentLine++)
            {
                var lineContainer = new LineContainer(GameData.COUNT_LINE);
                lineContainer.line = currentLine;

                var entity = ecsWorld.NewEntity();
                var pool = ecsWorld.GetPool<ELineContainerComponent>();
                ref var lineComp = ref pool.Add(entity);
                lineComp.lineContainer = lineContainer;

                for (int c = 0; c < GameData.COUNT_LINE; c++)
                {
                    var columnContainer = worldFactory.Create(position);
                    columnContainer.line = currentLine;
                    lineContainer.data.Add(columnContainer);
                    position.x += columns.Offset;
                }

                containers.Add(lineContainer);
                position.z += columns.Offset;
                position.x = 0;
            }

            return containers;
        }

  
    }
}