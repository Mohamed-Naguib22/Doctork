CREATE PROCEDURE GetRoleByName
	@Name VARCHAR(255)
AS
BEGIN
	SELECT * FROM Roles WHERE [Name] = @Name
END