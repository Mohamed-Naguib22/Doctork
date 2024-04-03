CREATE PROCEDURE AddDoctor
	@Id VARCHAR(255),
	@JobTitle VARCHAR(255),
	@Certificate VARCHAR(255),
	@PracticeLicenceUrl VARCHAR(255),
	@Biography TEXT,
	@IsVerified BIT
AS
BEGIN
	INSERT INTO Doctors(Id, JobTitle, [Certificate], PracticeLicenceUrl, Biography, IsVerified)
	VALUES (@Id, @JobTitle, @Certificate, @PracticeLicenceUrl, @Biography, @IsVerified);
END