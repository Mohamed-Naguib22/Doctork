CREATE PROCEDURE AddDoctorSpecialization
	@Type VARCHAR(255),
	@DoctorId VARCHAR(255),
    @SpecializationId INT
AS
BEGIN
	INSERT INTO DoctorSpecializations([Type], DoctorId, SpecializationId) 
	VALUES(@Type, @DoctorId, @SpecializationId);
END 