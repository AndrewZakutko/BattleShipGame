using CustomORM;
namespace BattleshipLibrary
{
    [Related(typeof(Ship))]
    public enum Type
    {
        WarShip = 1,
        HelpShip = 2,
        MixShip = 3
    }
}
