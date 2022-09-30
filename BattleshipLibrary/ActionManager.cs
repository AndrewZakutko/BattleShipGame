namespace BattleshipLibrary
{
    public class ActionManager
    {
        public void AddShip(Field field, Position position, Ship ship, List<ShipWrapper> listShips)
        {
            var isPositionAvailable = IsPositionAvailable(field, position, ship, listShips);
            if (isPositionAvailable)
            {
                listShips.Add(new ShipWrapper(field, ship, position));
            }
        }

        private bool IsPositionAvailable(Field field, Position position, Ship ship, List<ShipWrapper> listShips)
        {
            var shipWrapper = new ShipWrapper(field, ship, position);
            if (listShips == null || listShips != null && listShips.Count == 0)
            {
                return true;
            }
            if (shipWrapper.OccupiedPoints.Count >= 1)
            {
                var result = listShips.SelectMany(x => x.OccupiedPoints).Any(x => shipWrapper.OccupiedPoints.Any(y => y.X == x.X && y.Y == x.Y));

                if (result)
                {
                    return false;
                }
            }
            return true;
        }

        public static List<ShipWrapper> GetSortedShips(List<ShipWrapper> listShips)
        {
            listShips.Sort();
            return listShips;
        }

        public void Move()
        {

        }
    }
}
