using CustomORM;

namespace BattleshipLibrary
{
    [Related(typeof(Position))]
    public enum Quadrant
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4
    }
}
