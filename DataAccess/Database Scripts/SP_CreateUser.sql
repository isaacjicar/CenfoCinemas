--SP para crear un usuario
CREATE PROCEDURE CRE_USER_PR
(
	@P_UserCode nvarchar(30),
	@P_Name  nvarchar(50),
	@P_Email nvarchar(30),
	@P_Password nvarchar(50),
	@P_BirthDate Datetime,
	@P_Status nvarchar(10)
)
AS
BEGIN

Insert into TBL_User(Created,Usercode,Name,Email,Password,BirthDate,Status)
values(GETDATE(),@P_UserCode,@P_Name,@P_Email,@P_Password,@P_BirthDate,@P_Status);


END
GO
