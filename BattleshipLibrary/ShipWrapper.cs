using CustomORM;
using System.ComponentModel.DataAnnotations.Schema;

namespace BattleshipLibrary
{
    [Repository(typeof(Field)), Repository(typeof(Ship)), Repository(typeof(Position))]
    [Table("ShipWrappers")]
    public class ShipWrapper : IComparable<ShipWrapper>
    {
        [ForeignKey("FieldId")]
        public Field Field { get; set; }
        [ForeignKey("ShipId")]
        public Ship Ship { get; set; }
        [ForeignKey("PositionId")]
        public Position Position { get; set; }
        public List<Point> OccupiedPoints { get; set; }

        public ShipWrapper()
        {
            Field = new Field();
            Ship = new Ship();
            Position = new Position();
        }

        public ShipWrapper(Field field, Ship ship, Position position)
        {
            Field = field;
            Position = position;
            Ship = ship;
            OccupiedPoints = new List<Point>();

            AddOccupiedPoints(position, ship);
        }


        private void AddOccupiedPoints(Position position, Ship ship)
        {
            OccupiedPoints.Add(position.Point);

            if (ship.Length < 2) { return; }

            var x = Position.Point.X;
            var y = Position.Point.Y;

            for (int i = 1; i < ship.Length; i++)
            {
                switch (ship.Direction)
                {
                    case Direction.Up:
                        y += 1;
                        break;
                    case Direction.Down:
                        y -= 1;
                        break;
                    case Direction.Left:
                        x -= 1;
                        break;
                    case Direction.Right:
                        x += 1;
                        break;
                }
                OccupiedPoints.Add(new Point(x, y));
            }
        }


        private double GetMinDistance()
        {
            var occupiedPointsLength = OccupiedPoints.Count;
            if (occupiedPointsLength == 1)
            {
                return Math.Sqrt(OccupiedPoints[0].X * OccupiedPoints[0].X + OccupiedPoints[0].Y * OccupiedPoints[0].Y);
            }
            else
            {
                return Math.Min(Math.Sqrt(OccupiedPoints[0].X * OccupiedPoints[0].X + OccupiedPoints[0].Y * OccupiedPoints[0].Y),
                                Math.Sqrt(OccupiedPoints[occupiedPointsLength - 1].X * OccupiedPoints[occupiedPointsLength - 1].X +
                                OccupiedPoints[occupiedPointsLength - 1].Y * OccupiedPoints[occupiedPointsLength - 1].Y));
            }
        }


        public int CompareTo(ShipWrapper other)
        {
            return GetMinDistance().CompareTo(other.GetMinDistance());
        }

    }
}
