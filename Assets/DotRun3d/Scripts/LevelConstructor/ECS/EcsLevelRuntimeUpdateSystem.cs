using DonRun3D.World.Column;
using DonRun3D.World.SwitchPlatform;
using DotRun3d.ECS.World;
using DotRun3d.LevelConstructor;
using DotRun3d.Scripts.World;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;


namespace DonRun3D.ECS.LevelContructor
{
    public class EcsLevelRuntimeUpdateSystem : IEcsRunSystem
    {
        private const int MIN_LINE_FOR_CREAE_PLATFORM = 5;
        [Inject] protected readonly WorldData worldData;
        [Inject] protected readonly WorldFactory worldFactory;
        

        private int countSkipSwitchPlatform = MIN_LINE_FOR_CREAE_PLATFORM;
        
        public void Run(IEcsSystems systems)
        {
            if (!systems.Have<EUpdateRuntimeLevelComp>())
                return;
            systems.Delete<EUpdateRuntimeLevelComp>();
    
            var lastLineContainer = GetLastLineContainer(systems);
            
            var lineContainer = systems.GetWorld().GetPool<ELineContainerComponent>();
            var lineContainerFilter = systems.GetWorld().Filter<ELineContainerComponent>().End();
            
            systems.GetFirstComp<EcsGameManagerComponent>(out var managerComponent);

            var countHideLines = managerComponent.gameData.countHideLines;
            var lastLine = lastLineContainer.line;
            
            var nextPos = (lastLineContainer as IPosition).position;

            var createSwitchColorPlatform = false;

            for (int i = 0; i < countHideLines; i++)
            {
                lastLine++;
                nextPos.z += worldData.GetFirsData().offetLine;
                nextPos.x = 0;
                if (--countSkipSwitchPlatform <= 0)
                {
                    countSkipSwitchPlatform = 0;
                    createSwitchColorPlatform = Random.Range(0, 101) > 90;

                    if (createSwitchColorPlatform)
                        countSkipSwitchPlatform = MIN_LINE_FOR_CREAE_PLATFORM;

                }

                ILineContainer lc = null;
                if (createSwitchColorPlatform)
                {
                    foreach (var item in lineContainerFilter)
                    {
                        ref var cl = ref lineContainer.Get(item);

                        if (cl.lineContainer as ISwitchPlatform != null && cl.lineContainer.IsPool)
                        {
                            lc = cl.lineContainer;
                            break;
                        }
                    }

                    if (lc == null)
                        lc = worldFactory.CreateSwitchPlatformLineContainer(nextPos, systems.GetWorld(), lastLine);

                    (lc as ISwitchPlatform).data.line = lastLine;
                }

                else
                {
                    foreach (var item in lineContainerFilter)
                    {
                        ref var cl = ref lineContainer.Get(item);

                        if (cl.lineContainer as IColumnLine != null && cl.lineContainer.IsPool)
                        {
                            lc = cl.lineContainer;
                            break;
                        }
                    }

                    if (lc == null)
                        lc = worldFactory.CreateLineContainer(nextPos, systems.GetWorld(), lastLine);

                    var columns = lc as IColumnLine;
                    foreach (var item in columns.data)
                        item.line = lastLine;

                }

                lc.IsPool = false;
                lc.line = lastLine;
                
                createSwitchColorPlatform = false;
            }

        }

        private ILineContainer GetLastLineContainer(IEcsSystems systems)
        {
            var lineContainer = systems.GetWorld().GetPool<ELineContainerComponent>();
            var lineContainerFilter = systems.GetWorld().Filter<ELineContainerComponent>().End();
            ILineContainer lastLineContainer = null;
            int lastLine = 0;
            foreach (var item in lineContainerFilter)
            {
                ref var cl = ref lineContainer.Get(item);
                if (cl.lineContainer.line >= lastLine)
                {
                    lastLine = cl.lineContainer.line;
                    lastLineContainer = cl.lineContainer;
                }
            }
            return lastLineContainer;
        }
        
        
    }
}