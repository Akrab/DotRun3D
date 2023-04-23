using Akrab;
using Leopotam.EcsLite;
using UnityEngine;

namespace DonRun3D.World.Column
{
    
    public class ColumnView : CustomBehaviour, IEcsEntityAccept
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Transform playerTarget;
        [SerializeField] private Transform bonusTarget;

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
