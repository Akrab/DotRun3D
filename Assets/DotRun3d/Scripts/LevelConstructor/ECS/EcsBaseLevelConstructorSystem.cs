using System.Collections.Generic;
using DonRun3D.World.Column;
using DotRun3d;
using DotRun3d.LevelConstructor;
using DotRun3d.Scripts.World;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace DonRun3D.ECS.LevelContructor
{
    public class EcsBaseLevelConstructorSystem
    {
        [Inject] protected readonly WorldData worldData;
        [Inject] protected readonly CameraManager cameraManager;
        [Inject] protected readonly WorldFactory worldFactory;
        [Inject] protected readonly EcsWorld ecsWorld;


        protected int GetRandomMainIndex()
        {
            Dictionary<int, int> weights = new Dictionary<int, int>(GameData.COUNT_LINE);

            var w = 1000 / GameData.COUNT_LINE;

            for (int i = 0; i < GameData.COUNT_LINE; i++)
                weights.Add(i, w);

            var mainIndex =  WeightedRandomizer.From(weights).TakeOne();// Random.Range(0, GameData.COUNT_LINE);

            return mainIndex;
        }
        protected List<ILineContainer> SetColors(List<ILineContainer> data)
        {
            var line = 0;
            var index = 0;
            ecsWorld.GetFirstComp<EcsGameManagerComponent>(out var managerComponent);
            var mainColor = managerComponent.gameData.currentColor;
            var mainIndex = GetRandomMainIndex();
            foreach (var lineContainer in data)
            {

                var columnLine = lineContainer as IColumnLine;
                if(columnLine == null)
                    continue; // TODO : add new Type
                
                SetColumnsColor(columnLine, mainColor);
                
            }
            
            return data;
        }

        protected void SetColumnsColor(IColumnLine line, ColorType mainColor)
        {
            var mainIndex = GetRandomMainIndex();
            int index = 0;

            var colorMaterials = worldData.GetFirsData().colorMaterials;
            
            foreach (var column in line.data)
            {
                ColorMaterial colorMaterial = null;
                if (index == mainIndex)
                {
                    colorMaterial = new ColorMaterial();
                    colorMaterial.material = colorMaterials.FindMaterial(mainColor);
                    colorMaterial.colorType = mainColor;
                }
                else
                    colorMaterial = colorMaterials.GetRandomMaterial();

                column.colorMaterial = colorMaterial;
                column.view.SetMaterial(colorMaterial.material);
                index++;
            }
        }
    }
}