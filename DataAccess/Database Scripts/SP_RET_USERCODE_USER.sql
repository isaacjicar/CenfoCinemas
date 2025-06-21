CREATE PROCEDURE RET_UserCode_USERS_PR
(
	@P_Usercode nvarchar(30)
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
    WHERE Usercode =@P_Usercode;  
END
GO