using System.Collections.Generic;
using DonRun3D.World.Column;
using DotRun3d.ECS.World;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace DonRun3D.ECS.LevelContructor
{
    sealed class EcsLevelUpdateSystem : IEcsRunSystem {  
        
        [Inject] protected readonly Columns columns;
        
        public void Run (IEcsSystems systems) {
            
            if (!systems.Have<EUpdateLevelComp>())
                return;
            
            
            systems.Delete<EUpdateLevelComp>();
            return;
            var lineContainer = systems.GetWorld().GetPool<ELineContainerComponent>();
            var lineContainerFilter = systems.GetWorld().Filter<ELineContainerComponent>().End();
            systems.GetFirstComp<EcsGameManagerComponent>(out var managerComponent);

            LineContainer lastLine = null;

           // Dictionary<int, List<>>
           // var border = managerComponent.gameData.tapLine - 1;
           // foreach (var item in lineContainerFilter)
           // {
           //     ref var cl = ref lineContainer.Get(item);
//
           //     if (cl.lineContainer.line >= managerComponent.gameData.tapLine)
           //         continue;
           //     
           //     if (cl.lineContainer.line < border)
           //     {
           //         
           //     }
           //     
           // }
            /*
            var columnsContainer = systems.GetWorld().GetPool<EColumnContainerComponent>();
            var columnsContainerFilter = systems.GetWorld().Filter<EColumnContainerComponent>().End();

            systems.GetFirstComp<EcsGameManagerComponent>(out var managerComponent);

            var border = managerComponent.gameData.tapLine - 1;
            
            var lastPosition = 0f;
            foreach (var item in columnsContainerFilter)
            {
                ref var cc = ref columnsContainer.Get(item);

                if (cc.container.view.transform.position.z >= lastPosition)
                    lastPosition = cc.container.view.transform.position.z;
            }

            foreach (var item in columnsContainerFilter)
            {
                ref var cc = ref columnsContainer.Get(item);
                if(cc.container.line >= managerComponent.gameData.tapLine)
                    continue;

                if (cc.container.line < border)
                {
                    var pos = cc.container.view.transform.position;
                    pos.z = lastPosition + columns.Offset;
                    cc.container.view.transform.position = pos;
                }
            }*/

        }
    }
}