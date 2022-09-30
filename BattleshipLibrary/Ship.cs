using CustomORM;
using System.ComponentModel.DataAnnotations.Schema;
namespace BattleshipLibrary
{
    [Repository(typeof(System.Type)), Repository(typeof(Direction)), Related(typeof(ShipWrapper))]
    [Table("Ships")]
    public class Ship
    {
        public readonly Guid _id;
        //public readonly Guid _id;
        [ForeignKey("TypeId")]
        public Type Type { get; set; }
        [ForeignKey("DirectionId")]
        public Direction Direction { get; set; }
        //public Guid Id { get; set; }
        [Column("Length")]
        public int Length { get; set; }
        [Column("Speed")]
        public int Speed { get; set; }
        //public Direction Direction { get; set; }
        [Column("Range")]
        public int Range { get; set; }

        public Ship()
        {
            _id = Guid.NewGuid();
            Type = Type.WarShip;
            Direction = Direction.Up;
            Length = default(int);
            Speed = default(int);
            Range = default(int);
        }
        public Ship(Direction diraction, int length, int speed, int range)
        {
            _id = Guid.NewGuid();
            Direction = diraction;
            Length = length;
            Speed = speed;
            Range = range;
        }

        public static bool operator ==(Ship firstShip, Ship secondShip)
        {
            if (firstShip.Length == secondShip.Length && firstShip.Speed == secondShip.Speed && firstShip.GetType() == secondShip.GetType())
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(Ship firstShip, Ship secondShip)
        {
            if (firstShip.Length != secondShip.Length || firstShip.Speed != secondShip.Speed || firstShip.GetType() != secondShip.GetType())
            {
                return true;
            }
            return false;
        }


        public override string ToString()
        {
            return "Ship length: " + Length + " " + "Speed: " + Speed + " " + "Range: " + Range + " " + "Diraction: " + Direction + ".";
        }

    }
}