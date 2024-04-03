CREATE PROCEDURE AddTimeSlot
	@ClinicId INT,
	@DoctorId VARCHAR(255),
	@StartTime TIME,
	@EndTime TIME,
	@Day VARCHAR(255)
AS
BEGIN
	INSERT INTO Schedule(ClinicId, DoctorId, StartTime, EndTime, [Day])
	VALUES (@ClinicId, @DoctorId, @StartTime, @EndTime, @Day);
END