CREATE PROCEDURE AddDoctorClinic
	@Fees decimal(10, 2),
	@DoctorId VARCHAR(255),
    @ClinicId INT
AS
BEGIN
	INSERT INTO DoctorClinics(Fees, DoctorId, ClinicId) 
	VALUES(@Fees, @DoctorId, @ClinicId);
END 