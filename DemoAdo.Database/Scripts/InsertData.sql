/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

exec AppUser.DASP_Register @LastName = 'Morre', @FirstName = 'Thierry', @Email = 'thierry.morre@cognitic.be', @Passwd = 'Test1234='
exec AppUser.DASP_Register @LastName = 'Person', @FirstName = 'Michael', @Email = 'michael.person@cognitic.be', @Passwd = 'Test1234='
