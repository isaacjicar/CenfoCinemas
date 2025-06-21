--SP para Actualizar Usuarios
CREATE PROCEDURE UP_Movie_PR
(
@P_Id int,
@P_Title nvarchar(75),
@P_Description  nvarchar(250),
@P_ReleaseDate Datetime,
@P_Genre nvarchar(20),
@P_Director nchar(30)
)
AS
BEGIN
    UPDATE TBL_Movie
    SET
        Title = @P_Title,
        Description = @P_Description,
        ReleaseDate =@P_ReleaseDate,
        Genre = @P_Genre,
        Director = @P_Director
    WHERE Id =@P_Id;  
END
GO