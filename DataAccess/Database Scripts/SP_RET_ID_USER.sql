--SP para Consultar Usuarios
CREATE PROCEDURE RET_ID_USERS_PR
(
	@P_Id int
)
AS
BEGIN
	select
     Id,
	 UserCode,
	 Name,
	 Email,
	 Password,
	 BirthDate,
	 Status,
	 Created,
	 Updated
	 from TBL_User
    WHERE Id =@P_Id;  
END
GO