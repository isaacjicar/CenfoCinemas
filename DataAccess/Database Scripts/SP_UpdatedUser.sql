--SP para Actualizar Usuarios
CREATE PROCEDURE UP_USER_PR
(
	@P_Id int,
	@P_UserCode nvarchar(30) ,
	@P_Name  nvarchar(50),
	@P_Email nvarchar(30),
	@P_Password nvarchar(50),
	@P_BirthDate Datetime,
	@P_Status nvarchar(10)
)
AS
BEGIN
    UPDATE TBL_User
    SET
        Name = @P_Name,
		Usercode = @P_UserCode,
        Email = @P_Email,
        Password = @P_Password,
        BirthDate = @P_BirthDate,
        Status = @P_Status
    WHERE Id =@P_Id;  
END
GO