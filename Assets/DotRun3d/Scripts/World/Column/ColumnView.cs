using Akrab;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace DonRun3D.World.Column
{
    
    public class ColumnView : CustomBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Transform playerTarget;
        [SerializeField] private Transform bonusTarget;
        
        public void SetMaterial(Material material)
        {
            meshRenderer.material = material;
        }
    }
}
