using CustomORM;
using System.ComponentModel.DataAnnotations.Schema;

namespace BattleshipLibrary
{
    [Table("Points")]
    [Related(typeof(Position))]
    public class Point
    {
        private int _x;
        private int _y;
        [Column("X")]
        public int X { get { return _x; } set { _x = value; } }
        [Column("Y")]
        public int Y { get { return _y; } set { _y = value; } }

        public Point()
        {
            X = default(int);
            Y = default(int);
        }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
