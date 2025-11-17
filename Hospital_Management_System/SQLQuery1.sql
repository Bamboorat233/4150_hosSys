INSERT INTO Department (Name, Location) VALUES
('Cardiology', 'Building A - Floor 3'),
('Neurology', 'Building B - Floor 2'),
('Pediatrics', 'Building C - Floor 1'),
('Orthopedics', 'Building A - Floor 5'),
('General Medicine', 'Building D - Floor 1'),
('Emergency', 'Main Building - Ground Floor');
INSERT INTO Doctor (Name, Specialization, Contact, DepartmentID) VALUES
('Dr. Emily Carter', 'Cardiologist', '555-2011', 1),
('Dr. Jonathan Reed', 'Neurologist', '555-3322', 2),
('Dr. Sarah Mitchell', 'Pediatrician', '555-1144', 3),
('Dr. Michael Davis', 'Orthopedic Surgeon', '555-7788', 4),
('Dr. Olivia Bennett', 'General Physician', '555-9911', 5),
('Dr. Daniel Brooks', 'Emergency Specialist', '555-2299', 6),
('Dr. Laura Thompson', 'Cardiologist', '555-6633', 1),
('Dr. Anthony White', 'Neurologist', '555-8811', 2);
INSERT INTO Patient (Name, DOB, Gender, Contact, Address) VALUES
('James Anderson', '1985-04-12', 'Male', '555-4512', '102 River Street'),
('Sophia Walker', '1992-09-23', 'Female', '555-6894', '88 Maple Avenue'),
('William Carter', '1978-02-07', 'Male', '555-3021', '41 Pine Road'),
('Ava Thompson', '2005-11-15', 'Female', '555-2245', '21 Oak Street'),
('Daniel Roberts', '1999-07-30', 'Male', '555-7780', '9 Cedar Lane'),
('Mia Collins', '1988-12-01', 'Female', '555-6723', '55 Birch Drive'),
('Benjamin Hughes', '1974-03-29', 'Male', '555-9182', '77 Highland Ave'),
('Emma Richardson', '2010-08-19', 'Female', '555-3390', '12 Willow Way'),
('Lucas Martinez', '1982-01-05', 'Male', '555-7441', '34 Forest Drive'),
('Isabella Clark', '1995-06-27', 'Female', '555-5568', '18 Cherry Street');
INSERT INTO Appointment (PatientID, DoctorID, AppointmentDate, AppointmentTime, Status) VALUES
(1, 1, '2025-01-10', '09:00', 'Completed'),
(2, 3, '2025-01-11', '10:00', 'Scheduled'),
(3, 2, '2025-01-09', '14:00', 'Completed'),
(4, 3, '2025-01-15', '11:30', 'Scheduled'),
(5, 5, '2025-01-17', '16:00', 'Cancelled'),
(6, 6, '2025-01-12', '18:00', 'Completed'),
(7, 4, '2025-01-14', '13:00', 'Scheduled'),
(8, 1, '2025-01-08', '15:30', 'Completed'),
(9, 7, '2025-01-13', '09:30', 'Completed'),
(10, 8, '2025-01-16', '10:45', 'Scheduled');
INSERT INTO MedicalRecord (PatientID, DoctorID, Diagnosis, Treatment, VisitDate) VALUES
(1, 1, 'Hypertension', 'Prescribed ACE inhibitors', '2025-01-10'),
(2, 3, 'Seasonal flu', 'Rest and antiviral medication', '2025-01-11'),
(3, 2, 'Migraine', 'MRI scan and medication', '2025-01-09'),
(4, 3, 'Ear infection', 'Antibiotics for 7 days', '2025-01-15'),
(5, 5, 'Gastroenteritis', 'Oral rehydration and probiotics', '2025-01-17'),
(6, 6, 'Minor trauma', 'X-ray and bandaging', '2025-01-12'),
(7, 4, 'Joint sprain', 'Physiotherapy recommended', '2025-01-14'),
(8, 1, 'Arrhythmia', 'ECG monitoring and medication', '2025-01-08'),
(9, 7, 'Chest discomfort', 'Stress test scheduled', '2025-01-13'),
(10, 8, 'Migraine', 'Pain management plan', '2025-01-16');
INSERT INTO Medication (Name, Dosage, Price, Quantity) VALUES
('Amoxicillin', '500mg', 12.50, 150),
('Ibuprofen', '200mg', 5.20, 300),
('Paracetamol', '500mg', 4.80, 500),
('Atorvastatin', '20mg', 22.00, 200),
('Metformin', '850mg', 15.40, 250),
('Aspirin', '100mg', 3.90, 400),
('Omeprazole', '20mg', 18.75, 180),
('Losartan', '50mg', 16.90, 220);
INSERT INTO Prescription (RecordID, MedID, Quantity) VALUES
(1, 4, 30),
(2, 3, 20),
(3, 8, 15),
(4, 1, 14),
(5, 5, 10),
(6, 6, 25),
(7, 2, 12),
(8, 4, 30),
(9, 7, 28),
(10, 3, 20);

