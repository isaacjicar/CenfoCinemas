
CREATE PROCEDURE RET_ALL_Movie_PR
AS
BEGIN
	SELECT Id,Created,Updated,Title,Description,ReleaseDate,Genre,Director
	from TBL_Movie;
END
GO
