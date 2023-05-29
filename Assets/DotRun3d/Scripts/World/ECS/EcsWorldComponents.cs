using DonRun3D.World.Column;
using DonRun3D.World.SwitchPlatform;

namespace DotRun3d.ECS.World
{
    public struct EColumnContainerComponent
    {
        public ColumnContainer container;
    }

    public struct ELineContainerComponent
    {
        public ILineContainer lineContainer;
    }

    public struct ESwitchPlatformContainerComponent
    {
        public SwitchPlatformContainer container;
    }
}
