CREATE PROCEDURE AddUser
	@Id VARCHAR(255),
	@Email VARCHAR(255),
	@FirstName VARCHAR(255),
	@LastName VARCHAR(255),
	@PhoneNumber VARCHAR(11),
	@Gender INT,
	@DateOfBirth datetime2,
	@PasswordHash VARCHAR(255),
	@RoleId VARCHAR(255)
AS
BEGIN
	INSERT INTO Users(Id, Email, FirstName,LastName, PhoneNumber, Gender, DateOfBirth, PasswordHash, RoleId)
	VALUES (@Id, @Email, @FirstName, @LastName, @PhoneNumber, @Gender, @DateOfBirth, @PasswordHash, @RoleId);
END