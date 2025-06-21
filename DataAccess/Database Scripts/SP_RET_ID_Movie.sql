CREATE PROCEDURE SP_RET_ID_Movie
(
	@P_Id int
)
AS
BEGIN
SELECT Id,Created,Updated,Title,Description,ReleaseDate,Genre,Director
	FROM TBL_Movie
	WHERE Id = @P_Id;
END
GO