--SP para Consultar Usuarios
CREATE PROCEDURE Consultation_USER_PR
(
	@P_Id int
)
AS
BEGIN
	select
     Id,
	 Usercode,
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