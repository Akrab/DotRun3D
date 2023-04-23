using DonRun3D;
using DonRun3D.ECS.LevelContructor;
using DonRun3D.World.Column;
using DotRun3d.ECS.World;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace DotRun3d.LevelConstructor
{
    public class WorldFactory//: CustomBehaviour
    {

        [Inject] private DiContainer _container;

        private int nameIndex = 0;
        public ColumnContainer Create(Vector3 position)
        {
            _container.Resolve<EcsWorld>().GetFirstComp(out ELevelConstrComp comp);

            var parentSpawn = comp.view.trmParent;
            var columns = _container.Resolve<Columns>();
            var baseColumn = columns.data[0];

            var ecsWorld = _container.Resolve<EcsWorld>();

            var view = _container.InstantiatePrefab(baseColumn.view, position, Quaternion.identity, parentSpawn) .GetComponent<ColumnView>();
            var entity = ecsWorld.NewEntity();

            var pool = ecsWorld.GetPool<EColumnContainerComponent>();
            ref var columnComp = ref pool.Add(entity);
            view.gameObject.name += "_" + nameIndex++;
            
            columnComp.container = new ColumnContainer {
                id = baseColumn.id,
                entity = ecsWorld.PackEntity (entity),
                isPool = true,
                view = view
            };
            
            view.SetEntity(columnComp.container.entity);
            return columnComp.container;
        }
    }
}
