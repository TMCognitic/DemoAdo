CREATE VIEW [AppUser].[V_Users]
	AS SELECT Id, LastName, FirstName, Email 
	FROM [Users]
	Where Active = 1
