DECLARE @quadrantId INT = (SELECT TOP 1 Id FROM Quadrants WHERE [Name] = 'First')
DECLARE @pointId INT = (SELECT TOP 1 Id FROM Points WHERE [X] = 1 AND [Y] = 1)

IF NOT EXISTS(SELECT TOP 1 1 FROM Positions WHERE QuadrantId = @quadrantId AND PointId = @pointId) AND @quadrantId IS NOT NULL AND @pointId IS NOT NULL
	BEGIN
		INSERT INTO [dbo].[Positions](QuadrantId, PointId)
		VALUES(@quadrantId, @pointId)
	END
GO