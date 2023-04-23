using DonRun3D.World.Column;
using DotRun3d.ECS.World;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace DonRun3D.ECS
{
    sealed class EcsClickToObjSystem : IEcsRunSystem
    {

        public void Run(IEcsSystems systems)
        {
            if (!systems.Have<EcsClick>())
                return;

            if (systems.Have<EcsLockClick>())
            {
                systems.Delete<EcsClick>();
                return;
            }
            
            systems.GetFirstComp(out EcsClick comp);
            var cl = comp.clickable as IColumn;

            var packEntity = cl.entity;

            if (!packEntity.Unpack(systems.GetWorld(), out int unpacked))
            {
                systems.Delete<EcsClick>();
                return;
            }

            var pool = systems.GetWorld().GetPool<EColumnContainerComponent>();
            ref var eC = ref pool.Get(unpacked);
            
            systems.GetFirstComp<EcsGameManagerComponent>(out var managerComponent);

            var isNextLine = eC.container.line >= managerComponent.gameData.tapLine;
            if (!isNextLine)
            {
                systems.Delete<EcsClick>();
                return;
            }
            
            if (eC.container.line == managerComponent.gameData.tapLine)
            {
                if (eC.container.colorMaterial.colorType == managerComponent.gameData.currentColor)
                {
                    systems.AddComp<EcsGameManagerComponent, EcsClickNextLine >();
                }
                else
                {
                    systems.Delete<EcsClick>();
                    Debug.LogError("Bad Color");
                }
            }
            else
            {
                systems.Delete<EcsClick>();
                Debug.LogError("Bad line");
            }
        }
        
    }
}