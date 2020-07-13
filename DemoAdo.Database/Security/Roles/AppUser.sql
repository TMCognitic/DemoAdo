CREATE ROLE [AppUser]
Go

Grant Select, Execute On Schema::AppUser To AppUser
Go

Alter ROLE [AppUser]
Add Member [DemoAdo]
Go