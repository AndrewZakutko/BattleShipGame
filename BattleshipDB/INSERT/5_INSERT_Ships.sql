DECLARE @typeId INT = (SELECT TOP 1 Id FROM [Types] WHERE [Name] = 'War')
DECLARE @directionId INT = (SELECT TOP 1 Id FROM Directions WHERE [Name] = 'Down')

IF NOT EXISTS(SELECT TOP 1 1 FROM Ships WHERE TypeId = @typeId AND DirectionId = @directionId) AND @typeId IS NOT NULL AND @directionId IS NOT NULL
	BEGIN
		INSERT INTO [dbo].[Ships](TypeId, DirectionId, [Length], Speed, [Range])
		VALUES(@typeId, @directionId, 4, 4, 4)
	END
GO