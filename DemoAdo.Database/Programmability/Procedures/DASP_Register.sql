CREATE PROCEDURE [AppUser].[DASP_Register]
	@LastName nvarchar(50),
	@FirstName nvarchar(50),
	@Email nvarchar(320),
	@Passwd nvarchar(20)
AS
Begin
	Insert into Users (LastName, FirstName, Email, Passwd)
	values (@LastName, @FirstName, @Email, HASHBYTES('SHA2_512', dbo.DASF_GetPreSalt() + @Passwd + dbo.DASF_GestPostSalt()));

	Select convert(int, SCOPE_IDENTITY());
	RETURN 0;
End
