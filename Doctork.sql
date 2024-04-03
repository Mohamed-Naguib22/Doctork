/*DROP TABLE Appointments
DROP TABLE AppointmentSlots
DROP TABLE DoctorClinics
DROP TABLE DoctorSpecializations
DROP TABLE PatientInsurances
DROP TABLE Specializations
DROP TABLE Clinics
DROP TABLE InsuranceCompanies
DROP TABLE Doctors
DROP TABLE Patients
DROP TABLE RefreshTokens
DROP TABLE Users
DROP TABLE Roles
*/

CREATE TABLE Roles (
	Id VARCHAR(255) PRIMARY KEY,
	[Name] VARCHAR(255) NOT NULL,
);

CREATE TABLE Users (
	Id VARCHAR(255) PRIMARY KEY,
	Email VARCHAR(255) UNIQUE NOT NULL,
	FirstName VARCHAR(255) NOT NULL,
	LastName VARCHAR(255) NOT NULL,
	PhoneNumber VARCHAR(11) NOT NULL,
	Gender INT NOT NULL,
	DateOfBirth datetime2 NOT NULL,
	ImgUrl VARCHAR(255),
	PasswordHash VARCHAR(255) NOT NULL,
	RoleId VARCHAR(255) NOT NULL,
	FOREIGN KEY (RoleId) REFERENCES Roles(Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE RefreshTokens (
	Id INT PRIMARY KEY IDENTITY(1, 1),
	UserId VARCHAR(255) NOT NULL,
	Token VARCHAR(255) NOT NULL,
	ExpiresOn datetime2 NOT NULL,
	CreatedOn datetime2 NOT NULL,
	RevokedOn datetime2,
	FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE ON UPDATE CASCADE,
);

CREATE TABLE Doctors (
    Id VARCHAR(255) PRIMARY KEY FOREIGN KEY REFERENCES Users(Id),
	JobTitle VARCHAR(255) NOT NULL,
	[Certificate] VARCHAR(255) NOT NULL,
	PracticeLicenceUrl VARCHAR(255) NOT NULL,
	Biography TEXT,
	IsVerified BIT NOT NULL,
);

CREATE TABLE Patients (
    Id VARCHAR(255) PRIMARY KEY FOREIGN KEY REFERENCES Users(Id),
);

CREATE TABLE InsuranceCompanies (
	Id INT PRIMARY KEY IDENTITY(1, 1),
	[Name] VARCHAR(255) NOT NULL
);

CREATE TABLE ClinicInsurances (
	ClinicId INT NOT NULL,
	InsuranceCompanyId INT NOT NULL,
	PRIMARY KEY (ClinicId, InsuranceCompanyId),
	FOREIGN KEY (ClinicId) REFERENCES Clinics(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (InsuranceCompanyId) REFERENCES InsuranceCompanies(Id) ON DELETE CASCADE ON UPDATE CASCADE,
);

CREATE TABLE PatientInsurances (
  Id INT PRIMARY KEY,
  PolicyNumber VARCHAR(255) NOT NULL,
  ExpiryDate datetime2 NOT NULL,
  PatientId VARCHAR(255) NOT NULL,
  InsuranceCompanyId INT NOT NULL,
  FOREIGN KEY (PatientId) REFERENCES Patients(Id) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (InsuranceCompanyId) REFERENCES InsuranceCompanies(Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Specializations (
	Id INT PRIMARY KEY IDENTITY(1, 1),
	[Name] VARCHAR(255) NOT NULL,
	ImgUrl VARCHAR(255)
);

CREATE TABLE DoctorSpecializations (
	SpecializationId INT NOT NULL,
	DoctorId VARCHAR(255) NOT NULL,
	IsMain BIT NOT NULL,
	PRIMARY KEY (DoctorId, SpecializationId),
	FOREIGN KEY (DoctorId) REFERENCES Doctors(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (SpecializationId) REFERENCES Specializations(Id) ON DELETE CASCADE ON UPDATE CASCADE,
);

CREATE TABLE Clinics (
	Id INT PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(255) NOT NULL,
	City VARCHAR(255) NOT NULL,
	Area VARCHAR(255) NOT NULL,
	Street VARCHAR(255) NOT NULL,
	ContactNumber VARCHAR(8)
);

CREATE TABLE DoctorClinics (
	DoctorId VARCHAR(255) NOT NULL,
	ClinicId INT NOT NULL,
	Fees DECIMAL(10, 2) NOT NULL,
	PRIMARY KEY (DoctorId, ClinicId),
	FOREIGN KEY (DoctorId) REFERENCES Doctors(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (ClinicId) REFERENCES Clinics(Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Schedule (
  Id INT PRIMARY KEY IDENTITY(1,1),
  [Day] VARCHAR(255) NOT NULL,
  StartTime TIME NOT NULL,
  EndTime TIME NOT NULL,
  DoctorId VARCHAR(255) NOT NULL,
  ClinicId INT NOT NULL,
  FOREIGN KEY (DoctorId) REFERENCES Doctors(Id) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (ClinicId) REFERENCES Clinics(Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Appointments (
  Id INT PRIMARY KEY IDENTITY(1,1),
  PatientId VARCHAR(255) NOT NULL,
  ScheduleId INT NOT NULL,
  [Date] datetime2 NOT NULL,
  [Status] INT NOT NULL,
  FOREIGN KEY (PatientId) REFERENCES Patients(Id) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (ScheduleId) REFERENCES Schedule(Id) ON DELETE CASCADE ON UPDATE CASCADE
);