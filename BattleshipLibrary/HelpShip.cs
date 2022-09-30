using BattleshipLibrary.Interfaces;
using CustomORM;
using System.ComponentModel.DataAnnotations.Schema;

namespace BattleshipLibrary
{
    [Repository(typeof(System.Type)), Repository(typeof(Direction)), Related(typeof(ShipWrapper))]
    [Table("Ships")]
    public class HelpShip : Ship, IRepair
    {
        public HelpShip(Direction diraction, int length, int speed, int range) : base(diraction, length, speed, range)
        {
            Type = Type.HelpShip;
        }

        public void Repair()
        {
            
        }
    }
}
