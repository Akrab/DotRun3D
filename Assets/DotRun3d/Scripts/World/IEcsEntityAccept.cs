using Leopotam.EcsLite;

namespace DonRun3D.World
{
    public interface IEcsEntityAccept
    {
        public EcsPackedEntity entity { get; }
    }
}