using DG.Tweening;
using DonRun3D.ECS.LevelContructor;
using DonRun3D.World.Column;
using DonRun3D.World.SwitchPlatform;
using DotRun3d.ECS.World;
using DotRun3d.Scripts.World;
using Leopotam.EcsLite;
using Zenject;

namespace DonRun3D.ECS.LevelContructor
{
    sealed class EcsMoveToNextLineSystem : IEcsRunSystem
    {
        protected const float DURATION = 0.25f;
        [Inject] protected readonly WorldData worldData;

        public void Run(IEcsSystems systems)
        {

            if (!systems.Have<EcsClickNextLine>())
                return;
            
            var lineContainer = systems.GetWorld().GetPool<ELineContainerComponent>();
            var lineContainerFilter = systems.GetWorld().Filter<ELineContainerComponent>().End();
            systems.GetFirstComp(out EcsClick comp);
            var cl = comp.clickable as IColumn;

            var packEntity = cl.entity;

            packEntity.Unpack(systems.GetWorld(), out int clickedColumnEntity);

            Sequence seq = DOTween.Sequence();
            systems.AddComp<EcsGameManagerComponent, EcsLockClick>();
            foreach (var item in lineContainerFilter)
            {
                ref var r = ref lineContainer.Get(item);
                var columnLine = r.lineContainer as IColumnLine;

                if (columnLine != null)
                {
                    if(columnLine.IsPool)
                        continue;
                    foreach (var c in columnLine.data)
                    {
                        var position = c.view.transform.position;
                        seq.Insert(0, c.view.transform.DOMoveZ(position.z - worldData.GetFirsData().offetLine, DURATION));
                    }
                }

                var switchLine = r.lineContainer as ISwitchPlatform;
                if (switchLine != null)
                {
                    if(switchLine.IsPool)
                        continue;
                    var position = switchLine.data.view.transform.position;
                    seq.Insert(0, switchLine.data.view.transform.DOMoveZ(position.z - worldData.GetFirsData().offetLine, DURATION));
                }

            }

            seq.OnComplete(() =>
            {
                systems.GetFirstComp<EcsGameManagerComponent>(out var managerComponent);
                managerComponent.gameData.tapLine++;
                systems.Delete<EcsLockClick>();
                systems.AddComp<EcsGameManagerComponent, EHideLevelComp>();
            });

            systems.Delete<EcsClick>();
            systems.Delete<EcsClickNextLine>();
        }
    }
}