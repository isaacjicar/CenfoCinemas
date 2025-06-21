
--SP para Consultar Usuarios
CREATE PROCEDURE RET_Email_USERS_PR
(
	@P_Email nvarchar(30)
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
    WHERE Email =@P_Email;  
END
GO