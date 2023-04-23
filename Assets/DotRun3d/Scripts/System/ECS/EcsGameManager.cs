using DonRun3D.World.Column;

namespace DonRun3D.ECS
{

    public struct EcsGameManagerComponent
    {
        public GameData gameData;
    }

    public struct EcsStartupLoadEnd { }

    public struct EcsClickNextLine { }

    public struct EcsClick
    {
        public IClickable clickable;
    }

    public struct EcsLockClick
    {
        
    }

}
