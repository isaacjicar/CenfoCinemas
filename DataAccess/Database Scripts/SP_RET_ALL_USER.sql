
CREATE PROCEDURE RET_ALL_USERS_PR
AS
BEGIN
	SELECT Id,Created,Updated,UserCode,Name,Email,Password,BirthDate,Status
	from TBL_User;
END
GO
