using DG.Tweening;
using DonRun3D.ECS.LevelContructor;
using DonRun3D.World.Column;
using DotRun3d.ECS.World;
using Leopotam.EcsLite;
using Zenject;

namespace DonRun3D.ECS
{
    sealed class EcsMoveToTextLineSystem : IEcsRunSystem
    {

        [Inject] protected readonly Columns columns;

        public void Run(IEcsSystems systems)
        {

            if (!systems.Have<EcsClickNextLine>())
                return;

            var columnsContainer = systems.GetWorld().GetPool<ELineContainerComponent>();
            var columnsContainerFilter = systems.GetWorld().Filter<ELineContainerComponent>().End();
            systems.GetFirstComp(out EcsClick comp);
            var cl = comp.clickable as IColumn;

            var packEntity = cl.entity;

            packEntity.Unpack(systems.GetWorld(), out int clickedColumnEntity);

            Sequence seq = DOTween.Sequence();
            systems.AddComp<EcsGameManagerComponent, EcsLockClick>();
            foreach (var item in columnsContainerFilter)
            {
                ref var r = ref columnsContainer.Get(item);
                var columnLine = r.lineContainer as IColumnLine;


                foreach (var c in columnLine.data)
                {
                    var position = c.view.transform.position;
                    seq.Insert(0, c.view.transform.DOMoveZ(position.z - columns.Offset, 0.25f));
                }
 
            }

            seq.OnComplete(() =>
            {
                systems.GetFirstComp<EcsGameManagerComponent>(out var managerComponent);
                managerComponent.gameData.tapLine++;
                systems.Delete<EcsLockClick>();
                systems.AddComp<EcsGameManagerComponent, EUpdateLevelComp>();
            });

            systems.Delete<EcsClick>();
            systems.Delete<EcsClickNextLine>();
        }
    }
}