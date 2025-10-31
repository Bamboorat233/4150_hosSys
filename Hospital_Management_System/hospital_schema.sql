CREATE TABLE dbo.Department
(
    DepartmentID INT IDENTITY(1,1) CONSTRAINT PK_Department PRIMARY KEY,
    Name         NVARCHAR(100) NOT NULL,
    Location     NVARCHAR(100) NULL,
    CONSTRAINT UQ_Department_Name UNIQUE (Name)
);
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
CREATE INDEX IX_Doctor_Department ON dbo.Doctor(DepartmentID);
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
CREATE INDEX IX_Appointment_Patient ON dbo.Appointment(PatientID);
CREATE INDEX IX_Appointment_Doctor  ON dbo.Appointment(DoctorID);
CREATE INDEX IX_Appointment_Date    ON dbo.Appointment(AppointmentDate);
CREATE TABLE dbo.MedicalRecord
(
    RecordID  INT IDENTITY(1,1) CONSTRAINT PK_MedicalRecord PRIMARY KEY,
    PatientID INT NOT NULL,
    DoctorID  INT NOT NULL,
    Diagnosis NVARCHAR(MAX) NULL,
    Treatment NVARCHAR(MAX) NULL,
    VisitDate DATE NOT NULL,
    CONSTRAINT FK_Record_Patient
        FOREIGN KEY (PatientID)
        REFERENCES dbo.Patient(PatientID)
        ON UPDATE CASCADE
        ON DELETE NO ACTION,
    CONSTRAINT FK_Record_Doctor
        FOREIGN KEY (DoctorID)
        REFERENCES dbo.Doctor(DoctorID)
        ON UPDATE CASCADE
        ON DELETE NO ACTION
);
CREATE INDEX IX_Record_Patient ON dbo.MedicalRecord(PatientID);
CREATE INDEX IX_Record_Doctor  ON dbo.MedicalRecord(DoctorID);
CREATE INDEX IX_Record_Visit   ON dbo.MedicalRecord(VisitDate);
CREATE TABLE dbo.Medication
(
    MedID    INT IDENTITY(1,1) CONSTRAINT PK_Medication PRIMARY KEY,
    Name     NVARCHAR(100) NOT NULL,
    Dosage   NVARCHAR(50)  NULL,
    Price    DECIMAL(10,2) NOT NULL CONSTRAINT DF_Medication_Price DEFAULT (0.00),
    Quantity INT NOT NULL CONSTRAINT DF_Medication_Quantity DEFAULT (0),
    CONSTRAINT UQ_Medication_Name UNIQUE (Name),
    CONSTRAINT CK_Medication_Price CHECK (Price >= 0),
    CONSTRAINT CK_Medication_Quantity CHECK (Quantity >= 0)
);
CREATE TABLE dbo.Prescription
(
    RecordID INT NOT NULL,
    MedID    INT NOT NULL,
    Quantity INT NOT NULL,
    CONSTRAINT PK_Prescription PRIMARY KEY (RecordID, MedID),
    CONSTRAINT FK_Presc_Record
        FOREIGN KEY (RecordID)
        REFERENCES dbo.MedicalRecord(RecordID)
        ON UPDATE CASCADE
        ON DELETE CASCADE,      -- 删除就诊记录时级联删除处方
    CONSTRAINT FK_Presc_Med
        FOREIGN KEY (MedID)
        REFERENCES dbo.Medication(MedID)
        ON UPDATE CASCADE
        ON DELETE NO ACTION,
    CONSTRAINT CK_Presc_Quantity CHECK (Quantity > 0)
);
CREATE INDEX IX_Prescription_Med ON dbo.Prescription(MedID);
CREATE TABLE dbo.Invoice
(
    InvoiceID     INT IDENTITY(1,1) CONSTRAINT PK_Invoice PRIMARY KEY,
    PatientID     INT NOT NULL,
    AppointmentID INT NOT NULL,
    Amount        DECIMAL(10,2) NOT NULL CONSTRAINT DF_Invoice_Amount DEFAULT (0.00),
    DateIssued    DATETIME2(0)  NOT NULL CONSTRAINT DF_Invoice_DateIssued DEFAULT (SYSUTCDATETIME()),
    Status        NVARCHAR(20)  NOT NULL
        CONSTRAINT CK_Invoice_Status
        CHECK (Status IN (N'Unpaid', N'Paid', N'Voided', N'Refunded')),
    CONSTRAINT UQ_Invoice_Appointment UNIQUE (AppointmentID),
    CONSTRAINT FK_Invoice_Patient
        FOREIGN KEY (PatientID)
        REFERENCES dbo.Patient(PatientID)
        ON UPDATE CASCADE
        ON DELETE NO ACTION,
    CONSTRAINT FK_Invoice_Appointment
        FOREIGN KEY (AppointmentID)
        REFERENCES dbo.Appointment(AppointmentID)
        ON UPDATE CASCADE
        ON DELETE NO ACTION
);
CREATE INDEX IX_Invoice_Patient ON dbo.Invoice(PatientID);
CREATE TABLE dbo.Payment
(
    PaymentID      INT IDENTITY(1,1) CONSTRAINT PK_Payment PRIMARY KEY,
    InvoiceID      INT NOT NULL,
    [Method]       NVARCHAR(20) NOT NULL
        CONSTRAINT CK_Payment_Method
        CHECK ([Method] IN (N'Cash', N'Card', N'Online', N'Insurance')),
    DatePaid       DATETIME2(0) NOT NULL CONSTRAINT DF_Payment_DatePaid DEFAULT (SYSUTCDATETIME()),
    ConfirmationNo NVARCHAR(100) NULL,
    CONSTRAINT UQ_Payment_Confirmation UNIQUE (ConfirmationNo),
    CONSTRAINT FK_Payment_Invoice
        FOREIGN KEY (InvoiceID)
        REFERENCES dbo.Invoice(InvoiceID)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);
CREATE INDEX IX_Payment_Invoice ON dbo.Payment(InvoiceID);