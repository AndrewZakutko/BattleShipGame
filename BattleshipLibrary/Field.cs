using CustomORM;
using System.ComponentModel.DataAnnotations.Schema;

namespace BattleshipLibrary
{
    [Related(typeof(ShipWrapper))]
    [Table("Fields")]
    public class Field
    {
        [Column("Size")]
        public int Size { get; set; }
        public List<ShipWrapper> ListShips { get; set; }

        public Field()
        {
            Size = default(int);
        }
        public Field(int size)
        {
            if (size < 10)
            {
                throw new Exception("The minimum field size is 10!");
            }
            else
            {
                Size = size;
                ListShips = new List<ShipWrapper>();
            }

        }

        public ShipWrapper this[Quadrant quadrant, int x, int y]
        {
            get
            {
                var resultShipWrapper = ListShips.FirstOrDefault(t => t.Position.Quadrant == quadrant
                                                                        && Math.Abs(t.Position.Point.X) == Math.Abs(x)
                                                                        && Math.Abs(t.Position.Point.Y) == Math.Abs(y));
                return resultShipWrapper;
            }
        }

        public override string ToString()
        {
            var sortedList = ActionManager.GetSortedShips(ListShips);
            var result = String.Empty;
            foreach (ShipWrapper item in sortedList)
            {
                result += item.Ship.GetType() + " Id: " + item.Ship._id.ToString() + "\n";
            }
            return result;

        }
    }
}
