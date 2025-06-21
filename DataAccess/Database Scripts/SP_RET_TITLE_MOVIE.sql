CREATE PROCEDURE SP_RET_TITLE_Movie
(
	@P_Title nvarchar(75)
)
AS
BEGIN
SELECT Id,Created,Updated,Title,Description,ReleaseDate,Genre,Director
	FROM TBL_Movie
	WHERE Title = @P_Title;
END
GO