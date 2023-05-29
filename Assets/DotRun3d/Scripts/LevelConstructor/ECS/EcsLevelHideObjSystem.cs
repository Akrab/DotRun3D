using System.Collections.Generic;
using DonRun3D.World.Column;
using DonRun3D.World.SwitchPlatform;
using DotRun3d.ECS.World;
using DotRun3d.Scripts.World;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace DonRun3D.ECS.LevelContructor
{
    sealed class EcsLevelHideObjSystem : IEcsRunSystem {  
        
        [Inject] protected readonly WorldData worldData;

        public void Run(IEcsSystems systems)
        {

            if (!systems.Have<EHideLevelComp>())
                return;
            systems.Delete<EHideLevelComp>();
            
            var lineContainer = systems.GetWorld().GetPool<ELineContainerComponent>();
            var lineContainerFilter = systems.GetWorld().Filter<ELineContainerComponent>().End();
            systems.GetFirstComp<EcsGameManagerComponent>(out var managerComponent);

            var border = managerComponent.gameData.tapLine - 2;
            List<ILineContainer> linesToHide = new List<ILineContainer>(2);
            foreach (var item in lineContainerFilter)
            {
                ref var cl = ref lineContainer.Get(item);
                if (cl.lineContainer.line < border)
                    linesToHide.Add(cl.lineContainer);

            }
            
            foreach (var item in linesToHide)
            {
                var columntContainer = item as IColumnLine;

                if (columntContainer != null)
                    HideColumns(columntContainer);

                var switchContainer = item as ISwitchPlatform;

                if (switchContainer != null)
                    HideSwitchPlatform(switchContainer);
            }
            
            managerComponent.gameData.countHideLines = linesToHide.Count;
            
            systems.AddComp<EcsGameManagerComponent, EUpdateRuntimeLevelComp>();
        }

        private void HideColumns(IColumnLine line)
        {

            foreach (var column in line.data)
            {
                var transform = column.view.transform;
                var position = transform.position;
                position = new Vector3(position.x,
                    position.y, -10f);
                transform.position = position;
                column.line = -1;
            }

            line.line = -1;
            line.IsPool = true;
        }

        private void HideSwitchPlatform(ISwitchPlatform switchContainer)
        {
            var transform = switchContainer.data.view.transform;
            var position = transform.position;
            position = new Vector3(position.x,
                position.y, -10f);
            transform.position = position;
            
            switchContainer.line = -1;
            switchContainer.IsPool = true;
        }
    }
}