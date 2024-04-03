CREATE PROCEDURE GetActiveRefreshToken
	@UserId VARCHAR(255)
AS
BEGIN
	SELECT * FROM RefreshTokens 
	WHERE UserId = @UserId AND RevokedOn IS NULL AND ExpiresOn > GETDATE();
END 