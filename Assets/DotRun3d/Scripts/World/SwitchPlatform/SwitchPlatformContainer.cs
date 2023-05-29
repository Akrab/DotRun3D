using DonRun3D.World.Column;
using Leopotam.EcsLite;
using UnityEngine;

namespace DonRun3D.World.SwitchPlatform
{
    public class SwitchPlatformContainer
    {
        public EcsPackedEntity entity;
        public int line;
        public ColorMaterial colorMaterial;
        public SwitchPlatformView view;
    }
    
    public interface ISwitchPlatform : ILineContainer
    {
        public SwitchPlatformContainer data { get; }
    }

    public class SwitchPlatformLineContainer : ISwitchPlatform, IPosition
    {
        public SwitchPlatformContainer data { get; private set; }
        public SwitchPlatformLineContainer(SwitchPlatformContainer data)
        {
            this.data = data;
        }

        public int line { get; set; }

        public bool IsPool { get; set; } = false;
        public EcsPackedEntity entity { get; set; }
        public Vector3 position => data.view.transform.position;
    }
}