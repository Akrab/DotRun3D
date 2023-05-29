using System.Collections.Generic;
using System.Linq;
using DonRun3D.World.Column;
using DonRun3D.World.SwitchPlatform;
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
            var gameData = managerComponent.gameData;
            gameData.line = columns.Max(D => D.line);
            gameData.tapLine = 1;

            gameData.canCreateSwitchPlatform = false;
            gameData.countLineAfterSwitchPlatform = managerComponent.gameData.line;

            CreateOneSwitchPlatform();
        }

        private List<ILineContainer> CreateLevel()
        {
            var startPos = cameraManager.StartSeePosition + 2f;
            var lines = Mathf.CeilToInt(
                (cameraManager.EndSeePostion - startPos) / worldData.GetFirsData().offetLine) + 2;

            List<ILineContainer> containers = new(lines);

            Vector3 position = new Vector3(0, 0, startPos);
            for (int currentLine = 0; currentLine < lines; currentLine++)
            {

                var lineContainer = worldFactory.CreateLineContainer(position, ecsWorld, currentLine);
                containers.Add(lineContainer);
                position.z += worldData.GetFirsData().offetLine;
                position.x = 0;
            }

            return containers;
        }

        private void CreateOneSwitchPlatform()
        {
            var startPos = cameraManager.StartSeePosition - 10f;
            Vector3 position = new Vector3(0, 0, 0)
            {
                x = worldData.GetFirsData().offetLine,
                z = startPos
            };

            worldFactory.CreateSwitchPlatformLineContainer(position, ecsWorld, -1);

        }

    }
}