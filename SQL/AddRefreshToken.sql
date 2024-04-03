CREATE PROCEDURE AddRefreshToken
	@Id INT,
	@UserId VARCHAR(255),
    @Token VARCHAR(255),
    @ExpiresOn datetime2,
    @CreatedOn datetime2,
    @RevokedOn datetime2
AS
BEGIN
	INSERT INTO RefreshTokens(UserId, Token, ExpiresOn, CreatedOn, RevokedOn) 
	VALUES(@UserId, @Token, @ExpiresOn, @CreatedOn, @RevokedOn);
END 