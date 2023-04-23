namespace DonRun3D
{

    public enum ColorType
    {
        GREEN,
        BLUE,
        ORANGE,
        RED
    }
    
    public class GameData
    {
        public const int COUNT_LINE = 3;
        public ColorType currentColor = ColorType.GREEN;
        public int tapLine = 0;

        public int line = 0;
    }
    
}