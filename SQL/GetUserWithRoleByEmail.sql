CREATE PROCEDURE GetUserWithRoleByEmail
	@Email VARCHAR(255)
AS
BEGIN
	SELECT u.*, r.Name AS Role 
    FROM Users u
    JOIN Roles r ON r.Id = u.RoleId
    WHERE Email = @Email;
END