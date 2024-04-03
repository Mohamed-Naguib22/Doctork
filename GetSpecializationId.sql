CREATE PROCEDURE GetSpecializationId
	@Name VARCHAR(255)
AS
BEGIN
	SELECT Id FROM Specializations WHERE [Name] = @Name
END