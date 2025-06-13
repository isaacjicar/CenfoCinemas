--SP para Eliminar Usuario
CREATE PROCEDURE Delete_USER_PR
(
	@P_UserCode nvarchar(30)
)
AS
BEGIN
	DELETE FROM TBL_User
	Where Usercode =@P_UserCode


END
GO
