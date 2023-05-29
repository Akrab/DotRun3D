using System;
using System.Collections.Generic;
using System.Linq;
using DonRun3D.World.Column;
using UnityEngine;

namespace DotRun3d.Scripts.World
{
    [Serializable]
    public class WorldData
    {
        public List<WorldViewData> data = new List<WorldViewData>();

        public WorldViewData GetFirsData()
        {
            return data.First();
        }
    }
    [Serializable]
    public class WorldViewData
    {
        public string id;

        public float offetLine = 2f;
        
        public GameObject columnView;

        public float columnOffsetZ = 2f;

        public GameObject switchPlatformView;
        
        public List<ColorMaterials> colorMaterials = new List<ColorMaterials>();
    }
}