using System.Collections;
using System.Collections.Generic;
using Akrab;
using Leopotam.EcsLite;
using UnityEngine;

namespace DonRun3D.World.SwitchPlatform
{
    public class SwitchPlatformView : CustomBehaviour, IEcsEntityAccept
    {
        [SerializeField] private MeshRenderer meshRenderer;
        public EcsPackedEntity entity { get; private set; }
        
        public void SetMaterial(Material material)
        {
            meshRenderer.material = material;
        }
        public void SetEntity(EcsPackedEntity entity)
        {
            this.entity = entity;
        }
    }
}