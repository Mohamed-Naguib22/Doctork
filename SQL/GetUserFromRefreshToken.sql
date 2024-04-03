CREATE PROCEDURE GetUserFromRefreshToken
	@Token VARCHAR(255)
AS
BEGIN
	SELECT u.* FROM Users u
	JOIN RefreshTokens r
	ON u.Id = r.UserId
	WHERE r.Token = @Token;
END