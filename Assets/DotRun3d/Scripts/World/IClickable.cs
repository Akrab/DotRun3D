using Leopotam.EcsLite;

namespace DonRun3D.World.Column
{
    public interface IClickable
    {
        public EcsPackedEntity entity { get; }
    }
    
    public interface IColumn : IClickable{}
}