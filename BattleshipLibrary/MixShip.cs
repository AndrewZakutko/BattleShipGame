using BattleshipLibrary.Interfaces;
using CustomORM;
using System.ComponentModel.DataAnnotations.Schema;

namespace BattleshipLibrary
{
    [Repository(typeof(System.Type)), Repository(typeof(Direction)), Related(typeof(ShipWrapper))]
    [Table("Ships")]
    public class MixShip : Ship, IShot, IRepair
    {
        public MixShip(Direction diraction, int length, int speed, int range) : base(diraction, length, speed, range)
        {
            Type = Type.MixShip;
        }

        public void Repair()
        {
            
        }

        public void Shot()
        {
            
        }
    }
}
