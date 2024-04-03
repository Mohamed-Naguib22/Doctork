CREATE PROCEDURE AddClinic
	@Name VARCHAR(255),
	@City VARCHAR(255),
	@Area VARCHAR(255),
	@Street VARCHAR(255),
	@ContactNumber VARCHAR(11)
AS
BEGIN
	INSERT INTO Clinics([Name], City, Area, Street, ContactNumber)
	VALUES(@Name, @City, @Area, @Street, @ContactNumber);
	SELECT SCOPE_IDENTITY();
END