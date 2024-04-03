CREATE PROCEDURE AddClinicInsurance
	@ClinicId INT,
	@InsuranceCompanyId INT
AS
BEGIN
	INSERT INTO ClinicInsurances(ClinicId, InsuranceCompanyId)
	VALUES (@ClinicId, @InsuranceCompanyId);
END