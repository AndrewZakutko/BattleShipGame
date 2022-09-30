using CustomORM;
using System.ComponentModel.DataAnnotations.Schema;

namespace BattleshipLibrary
{
    [Repository(typeof(Quadrant)), Repository(typeof(Point)), Related(typeof(ShipWrapper))]
    [Table("Positions")]
    public class Position
    {
        [ForeignKey("QuadrantId")]
        public Quadrant Quadrant { get; set; }
        [ForeignKey("PointId")]
        public Point Point { get; set; }

        public Position()
        {
            Quadrant = Quadrant.First;
            Point = new Point();
        }
        public Position(Quadrant quadrant, Point point)
        {
            Quadrant = quadrant;
            Point = point;
        }
    }
}
