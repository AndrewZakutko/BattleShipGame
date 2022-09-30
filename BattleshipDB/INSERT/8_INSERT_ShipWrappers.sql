DECLARE @field INT = (SELECT TOP 1 Id FROM Fields WHERE [Size] = 100)
DECLARE @ship INT = (SELECT TOP 1 Id FROM Ships WHERE [Length] = 2 AND [TypeId] = (SELECT TOP 1 Id FROM [Types] WHERE [Name] = 'War'))
DECLARE @position INT = (SELECT TOP 1 Id FROM Positions WHERE [QuadrantId] = (SELECT TOP 1 Id FROM Quadrants WHERE [Name] = 'First') AND [PointId] = (SELECT TOP 1 Id FROM Points WHERE [X] = 1 AND [Y] = 1))

IF NOT EXISTS (SELECT TOP 1 1 FROM ShipWrappers WHERE FieldId = @field AND ShipId = @ship)
	BEGIN
		INSERT INTO [dbo].[ShipWrappers](FieldId, ShipId, PositionId)
		VALUES(@field, @ship, @position)
	END
GO