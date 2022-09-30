using CustomORM;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BattleshipLibrary
{
    [Related(typeof(Ship))]
    public enum Direction
    {
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }
}
