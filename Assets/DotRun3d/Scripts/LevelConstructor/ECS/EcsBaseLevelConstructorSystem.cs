using System.Collections.Generic;
using DonRun3D.World.Column;
using DotRun3d.LevelConstructor;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace DonRun3D.ECS.LevelContructor
{
    public class EcsBaseLevelConstructorSystem
    {
        [Inject] protected readonly Columns columns;
        [Inject] protected readonly CameraManager cameraManager;
        [Inject] protected readonly WorldFactory worldFactory;
        [Inject] protected readonly EcsWorld ecsWorld;
        
        protected  void SetColors(List<ColumnContainer> data)
        {
            var line = 0;
            var index = 0;
            ecsWorld.GetFirstComp<EcsGameManagerComponent>(out var managerComponent);
            var mainColor = managerComponent.gameData.currentColor;
            foreach (var item in data)
            {
                
                if (index == GameData.COUNT_LINE)
                {
                    line++;
                    index = 0;
                }
                index++;
                
                var mainIndex = Random.Range(0, GameData.COUNT_LINE);
                Material material = default;
                if (index == mainIndex)
                {
                    material = columns.colorMaterials.FindMaterial(mainColor);
                    item.view.SetMaterial(material);
                    continue;
                }
                material = columns.colorMaterials.GetRandomMaterial();
                item.view.SetMaterial(material);
                
            }
        }
    }
}