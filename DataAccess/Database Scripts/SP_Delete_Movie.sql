--SP para Eliminar Usuario
CREATE PROCEDURE Delete_Movie_PR
(
	@P_Id int
)
AS
BEGIN
	DELETE FROM TBL_Movie
	Where Id =@P_Id


END
GO
