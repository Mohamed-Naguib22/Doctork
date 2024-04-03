CREATE PROCEDURE ConfirmDoctor (
	@Email VARCHAR(255)
)
AS
BEGIN
	BEGIN TRANSACTION
		UPDATE Users
		SET IsEmailConfirmed = 1
		WHERE Email = @Email;

		UPDATE d
		SET IsVerified = 1
		FROM Doctors d
		INNER JOIN Users u ON d.Id = u.Id
		WHERE u.Email = @Email;
	COMMIT TRANSACTION;
END
