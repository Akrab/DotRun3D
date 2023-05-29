using DonRun3D;
using DonRun3D.ECS.LevelContructor;
using DonRun3D.World.Column;
using DonRun3D.World.SwitchPlatform;
using DotRun3d.ECS.World;
using DotRun3d.Scripts.World;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace DotRun3d.LevelConstructor
{
    public class WorldFactory//: CustomBehaviour
    {

        [Inject] private DiContainer _container;
        [Inject] protected readonly WorldData worldData;

        private int nameIndex = 0;
        private int nameSwitchPlatformIndex = 0;
        public ColumnContainer Create(Vector3 position)
        {
            _container.Resolve<EcsWorld>().GetFirstComp(out ELevelConstrComp comp);

            var parentSpawn = comp.view.trmParent;
            var worldData = _container.Resolve<WorldData>();
            var baseColumn = worldData.GetFirsData();

            var ecsWorld = _container.Resolve<EcsWorld>();

            var view = _container.InstantiatePrefab(baseColumn.columnView, position, Quaternion.identity, parentSpawn) .GetComponent<ColumnView>();
            var entity = ecsWorld.NewEntity();

            var pool = ecsWorld.GetPool<EColumnContainerComponent>();
            ref var columnComp = ref pool.Add(entity);
            view.gameObject.name += "_" + nameIndex++;
            
            columnComp.container = new ColumnContainer {
                entity = ecsWorld.PackEntity (entity),
                view = view
            };
            
            view.SetEntity(columnComp.container.entity);
            return columnComp.container;
        }

        public ILineContainer CreateLineContainer(Vector3 position,  EcsWorld ecsWorld, int line)
        {
            var lineContainer = new LineContainer(GameData.COUNT_LINE);
            
            lineContainer.line = line;
            
            var entity = ecsWorld.NewEntity();
            var pool = ecsWorld.GetPool<ELineContainerComponent>();
            ref var lineComp = ref pool.Add(entity);
            lineComp.lineContainer = lineContainer;
            
            for (int c = 0; c < GameData.COUNT_LINE; c++)
            {
                var columnContainer = Create(position);
                columnContainer.line = line;
                lineContainer.data.Add(columnContainer);
                position.x += worldData.GetFirsData().offetLine;
            }

            return lineContainer;
        }

        public SwitchPlatformContainer CreateSwitchPlatformContainer(Vector3 position, int currentLine)
        {
            _container.Resolve<EcsWorld>().GetFirstComp(out ELevelConstrComp comp);

            var parentSpawn = comp.view.trmParent;
            var worldData = _container.Resolve<WorldData>().GetFirsData();

            var ecsWorld = _container.Resolve<EcsWorld>();

            var view = _container.InstantiatePrefab(worldData.switchPlatformView, position, Quaternion.identity, parentSpawn) .GetComponent<SwitchPlatformView>();
            var entity = ecsWorld.NewEntity();

            var pool = ecsWorld.GetPool<ESwitchPlatformContainerComponent>();
            ref var component = ref pool.Add(entity);
            view.gameObject.name += "_" + nameSwitchPlatformIndex++;
            
            component.container = new SwitchPlatformContainer {
                entity = ecsWorld.PackEntity (entity),
                view = view
            };
            
            view.SetEntity(component.container.entity);
            component.container.line = currentLine;
            return component.container;
        }

        public SwitchPlatformLineContainer CreateSwitchPlatformLineContainer(Vector3 position, EcsWorld ecsWorld, int currentLine)
        {
            var switchPlatformContainer = CreateSwitchPlatformContainer(position, currentLine);
            var lineContainer = new SwitchPlatformLineContainer(switchPlatformContainer);
            lineContainer.line = currentLine;
            lineContainer.IsPool = currentLine == -1;
            
            var entity = ecsWorld.NewEntity();
            var pool = ecsWorld.GetPool<ELineContainerComponent>();
            ref var lineComp = ref pool.Add(entity);
            lineComp.lineContainer = lineContainer;

            return lineContainer;
        }
    }
}
