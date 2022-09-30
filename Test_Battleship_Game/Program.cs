using BattleshipLibrary;
using CustomORM;

UnitOfWork unitOfWork = new UnitOfWork();

Repository<Field> repositoryFields = new Repository<Field>();
Repository<Point> repositoryPoints = new Repository<Point>();
Repository<Ship> repositoryShips = new Repository<Ship>();
Repository<Position> repositoryPositions = new Repository<Position>();
Repository<ShipWrapper> repositoryShipWrappers = new Repository<ShipWrapper>();

//Field newField = new Field(650);
//Point newPoint = new Point(20, 5);
//MixShip newMixShip = new MixShip(Direction.Left, 14, 15, 16);
//Position newPosition = new Position(Quadrant.First, newPoint);
//ShipWrapper newShipWrapper = new ShipWrapper(newField, newMixShip, newPosition);

//repositoryFields.Insert(newField);
//repositoryPoints.Insert(newPoint);
//repositoryShips.Insert(newMixShip);
//repositoryPositions.Insert(newPosition);
//repositoryShipWrappers.Insert(newShipWrapper);

//unitOfWork.SaveChanges();

//WarShip updateShip = new WarShip(Direction.Down, 10, 10, 10);
//repositoryShips.Update(2, updateShip);
//unitOfWork.SaveChanges();

//Console.WriteLine("Before delete point");
//List<Point> points = repositoryPoints.GetAll();
//foreach (var point in points)
//{
//    Console.WriteLine($"[{point.X};{point.Y}]");
//}

//repositoryPoints.Delete(5);
//unitOfWork.SaveChanges();

//Console.WriteLine("After delete point");
//points = repositoryPoints.GetAll();
//foreach (var point in points)
//{
//    Console.WriteLine($"[{point.X};{point.Y}]");
//}

//Ship takedShip = repositoryShips.Get(3);
//Console.WriteLine($"Type - {takedShip.Type}, Direction - {takedShip.Direction}, Length - {takedShip.Length}, Speed - {takedShip.Speed}, Range - {takedShip.Range}");

//List<ShipWrapper> listShipWrappers = repositoryShipWrappers.GetAll();
//foreach (var item in listShipWrappers)
//{
//    Console.WriteLine($"Field value - {item.Field.Size}, type of ship - {item.Ship.Type}, direction of ship - {item.Ship.Direction}\n" +
//                        $"length - {item.Ship.Length}, speed - {item.Ship.Speed}, range - {item.Ship.Range}\n" +
//                        $"quadrant - {item.Position.Quadrant}, point - [{item.Position.Point.X},{item.Position.Point.Y}]");
//    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
//}

