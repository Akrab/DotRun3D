using Akrab;
using DonRun3D;
using DonRun3D.ECS.LevelContructor;
using DonRun3D.LevelConstructor;
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


        public ColumnContainer Create(Vector3 position)
        {

             ELevelConstrComp comp;
            _container.Resolve<EcsWorld>().GetFirstComp(out comp);

            var parentSpawn = comp.view.trmParent;
            var columns = _container.Resolve<Columns>();
            var baseColumn = columns.data[0];

            var ecsWorld = _container.Resolve<EcsWorld>();

            var view = _container.InstantiatePrefab(baseColumn.view, position, Quaternion.identity, parentSpawn) .GetComponent<ColumnView>();
            var entity = ecsWorld.NewEntity();

            var pool = ecsWorld.GetPool<EColumnContainerComponent>();
            pool.Add(entity);

            return new ColumnContainer {
                id = baseColumn.id,
                entity = entity,
                isPool = true,
                view = view
            };
        }
    }
}
