--SP para crear un Peliculas
CREATE PROCEDURE CRE_MOVIES_PR
(
	@P_Title nvarchar(75),
	@P_Description  nvarchar(250),
	@P_ReleaseDate Datetime,
	@P_Genre nvarchar(20),
	@P_Director nchar(30)
)
AS
BEGIN

Insert into TBL_Movie(Created,Title,Description ,ReleaseDate ,Genre,Director)
values(GETDATE(),@P_Title,@P_Description,@P_ReleaseDate,@P_Genre,@P_Director);


END
GO
