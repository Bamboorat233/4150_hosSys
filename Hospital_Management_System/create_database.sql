-- Create Hospital Management System Database
-- Execute this script first to set up the database

USE master;
GO

-- Drop database if exists (for clean install)
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'hospital_schema')
BEGIN
    ALTER DATABASE hospital_schema SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE hospital_schema;
END
GO

-- Create new database
CREATE DATABASE hospital_schema;
GO

USE hospital_schema;
GO

-- Create Department Table
CREATE TABLE dbo.Department
(
    DepartmentID INT IDENTITY(1,1) CONSTRAINT PK_Department PRIMARY KEY,
    Name         NVARCHAR(100) NOT NULL,
    Location     NVARCHAR(100) NULL,
    CONSTRAINT UQ_Department_Name UNIQUE (Name)
);

-- Create Patient Table
CREATE TABLE dbo.Patient
(
    PatientID INT IDENTITY(1,1) CONSTRAINT PK_Patient PRIMARY KEY,
    Name      NVARCHAR(100) NOT NULL,
    DOB       DATE NOT NULL,
    Gender    NVARCHAR(10) NOT NULL
        CONSTRAINT CK_Patient_Gender CHECK (Gender IN (N'Male', N'Female', N'Other')),
    Contact   NVARCHAR(50)  NULL,
    [Address] NVARCHAR(255) NULL
);

-- Create Doctor Table
CREATE TABLE dbo.Doctor
(
    DoctorID      INT IDENTITY(1,1) CONSTRAINT PK_Doctor PRIMARY KEY,
    Name          NVARCHAR(100) NOT NULL,
    Specialization NVARCHAR(100) NULL,
    Contact       NVARCHAR(50)  NULL,
    DepartmentID  INT NOT NULL,
    CONSTRAINT FK_Doctor_Department
        FOREIGN KEY (DepartmentID)
        REFERENCES dbo.Department(DepartmentID)
        ON UPDATE CASCADE
        ON DELETE NO ACTION
);

-- Create Appointment Table
CREATE TABLE dbo.Appointment
(
    AppointmentID   INT IDENTITY(1,1) CONSTRAINT PK_Appointment PRIMARY KEY,
    PatientID       INT NOT NULL,
    DoctorID        INT NOT NULL,
    AppointmentDate DATE NOT NULL,
    AppointmentTime TIME(0) NOT NULL,
    Status          NVARCHAR(20) NOT NULL
        CONSTRAINT CK_Appointment_Status
        CHECK (Status IN (N'Scheduled', N'Completed', N'Cancelled', N'No-Show')),
    CONSTRAINT FK_Appointment_Patient
        FOREIGN KEY (PatientID)
        REFERENCES dbo.Patient(PatientID)
        ON UPDATE CASCADE
        ON DELETE NO ACTION,
    CONSTRAINT FK_Appointment_Doctor
        FOREIGN KEY (DoctorID)
        REFERENCES dbo.Doctor(DoctorID)
        ON UPDATE CASCADE
        ON DELETE NO ACTION
);

-- Create User Table for Authentication
CREATE TABLE dbo.[User]
(
    UserID      INT IDENTITY(1,1) CONSTRAINT PK_User PRIMARY KEY,
    Username    NVARCHAR(50)  NOT NULL,
    [Password]  NVARCHAR(100) NOT NULL,
    Role        NVARCHAR(20)  NOT NULL
        CONSTRAINT CK_User_Role 
        CHECK (Role IN (N'Admin', N'Doctor', N'Nurse', N'Receptionist')),
    FullName    NVARCHAR(100) NOT NULL,
    Email       NVARCHAR(100) NULL,
    CreatedDate DATETIME2(0)  NOT NULL CONSTRAINT DF_User_CreatedDate DEFAULT (GETDATE()),
    IsActive    BIT NOT NULL CONSTRAINT DF_User_IsActive DEFAULT (1),
    CONSTRAINT UQ_User_Username UNIQUE (Username)
);

-- Create Indexes
CREATE INDEX IX_Doctor_Department ON dbo.Doctor(DepartmentID);
CREATE INDEX IX_Appointment_Patient ON dbo.Appointment(PatientID);
CREATE INDEX IX_Appointment_Doctor  ON dbo.Appointment(DoctorID);
CREATE INDEX IX_Appointment_Date    ON dbo.Appointment(AppointmentDate);
CREATE INDEX IX_User_Username ON dbo.[User](Username);
CREATE INDEX IX_User_Role ON dbo.[User](Role);

-- Insert Sample Departments
INSERT INTO dbo.Department (Name, Location)
VALUES 
    (N'Cardiology', N'Building A, Floor 3'),
    (N'Surgery', N'Building B, Floor 1'),
    (N'Pediatrics', N'Building C, Floor 2'),
    (N'Internal Medicine', N'Building A, Floor 2');

-- Insert Test Users
INSERT INTO dbo.[User] (Username, [Password], Role, FullName, Email, CreatedDate, IsActive)
VALUES 
    (N'admin', N'admin123', N'Admin', N'System Administrator', N'admin@hospital.com', GETDATE(), 1),
    (N'doctor1', N'doc123', N'Doctor', N'Dr. John Smith', N'doctor1@hospital.com', GETDATE(), 1),
    (N'nurse1', N'nurse123', N'Nurse', N'Mary Johnson', N'nurse1@hospital.com', GETDATE(), 1),
    (N'reception1', N'rec123', N'Receptionist', N'Sarah Williams', N'reception1@hospital.com', GETDATE(), 1);

-- Verify setup
PRINT '========================================';
PRINT 'Database Setup Complete!';
PRINT '========================================';
PRINT 'Departments: ' + CAST((SELECT COUNT(*) FROM Department) AS VARCHAR);
PRINT 'Users: ' + CAST((SELECT COUNT(*) FROM [User]) AS VARCHAR);
PRINT '';
PRINT 'Test Accounts:';
PRINT '  Admin: admin / admin123';
PRINT '  Doctor: doctor1 / doc123';
PRINT '  Nurse: nurse1 / nurse123';
PRINT '  Receptionist: reception1 / rec123';
PRINT '========================================';

-- Show created tables
SELECT 
    TABLE_NAME,
    CASE 
        WHEN TABLE_TYPE = 'BASE TABLE' THEN 'Table'
        ELSE TABLE_TYPE
    END AS Type
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA = 'dbo'
ORDER BY TABLE_NAME;

