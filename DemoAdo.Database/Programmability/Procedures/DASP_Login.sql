CREATE PROCEDURE [AppUser].[DASP_Login]
	@Email nvarchar(320),
	@Passwd nvarchar(20)
AS
Begin
	SELECT Id, LastName, FirstName, @Email Email 
	from Users
	Where Email = @Email and Passwd = HASHBYTES('SHA2_512', dbo.DASF_GetPreSalt() + @Passwd + dbo.DASF_GestPostSalt());
	RETURN 0;
End
