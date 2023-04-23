
using Leopotam.EcsLite;
using UnityEngine;

namespace DonRun3D.World.Column
{
    public class ColumnClick : MonoBehaviour, IColumn
    {
        public EcsPackedEntity entity => GetComponentInParent<IEcsEntityAccept>().entity;
    }
}