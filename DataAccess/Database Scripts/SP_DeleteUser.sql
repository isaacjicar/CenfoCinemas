﻿--SP para Eliminar Usuario
CREATE PROCEDURE Delete_USER_PR
(
	@P_Id int
)
AS
BEGIN
	DELETE FROM TBL_User
	Where Id =@P_Id


END
GO
