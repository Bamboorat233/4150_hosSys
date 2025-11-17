# Hospital Management System - 医院管理系统

> A comprehensive hospital management desktop application built with C# WPF and SQL Server

<div align="center">

![Status](https://img.shields.io/badge/Status-Completed-success)
![Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2-blue)
![Language](https://img.shields.io/badge/Language-C%23-purple)
![Database](https://img.shields.io/badge/Database-SQL%20Server-red)
![License](https://img.shields.io/badge/License-Educational-orange)

</div>

## 📋 Project Overview

This is a fully-functional Hospital Management System developed for **Step 5 - GUI Development** of the 4150 project. The system provides a complete desktop application with user authentication, role-based access control, and full CRUD operations for managing patients, doctors, and appointments.

## ✨ Key Features

### 🔐 User Authentication & Authorization
- Secure login system
- Role-based access control (Admin, Doctor, Nurse, Receptionist)
- Session management

### 👥 Patient Management
- ✅ Create new patient records
- ✅ View patient list with search functionality
- ✅ Update patient information
- ✅ Delete patient records
- Real-time search and filtering

### 👨‍⚕️ Doctor Management
- ✅ Add doctor profiles
- ✅ View and search doctors
- ✅ Update doctor information
- ✅ Remove doctor records
- Department assignment

### 📅 Appointment Management
- ✅ Schedule new appointments
- ✅ View all appointments
- ✅ Update appointment status
- ✅ Cancel appointments
- Status tracking (Scheduled, Completed, Cancelled, No-Show)

### 🎨 Modern User Interface
- Material Design inspired UI
- Intuitive navigation
- Responsive layout
- Real-time data updates
- User-friendly error messages

## 🛠️ Technology Stack

| Component | Technology |
|-----------|------------|
| Framework | WPF (.NET Framework 4.7.2) |
| Language | C# |
| Database | SQL Server LocalDB |
| Architecture | 3-Tier (GUI - DAL - Model) |
| UI Design | Material Design principles |

## 📁 Project Structure

```
4150_hosSys/
├── Hospital_MamSys_LIB/              # Business Logic Layer
│   ├── Model/                        # Data Models
│   │   ├── Patient.cs
│   │   ├── Doctor.cs
│   │   ├── Appointment.cs
│   │   ├── User.cs
│   │   └── ...
│   └── DAL/                          # Data Access Layer
│       ├── DALBase.cs
│       ├── DALPatient.cs
│       ├── DALDoctor.cs
│       ├── DALAppointment.cs
│       ├── DALUser.cs
│       └── ...
│
├── Hospital_Management_System/        # GUI Layer
│   ├── MainWindow.xaml/cs            # Main Dashboard
│   ├── LoginWindow.xaml/cs           # Login Interface
│   ├── PatientManagementWindow.xaml/cs
│   ├── DoctorManagementWindow.xaml/cs
│   ├── AppointmentManagementWindow.xaml/cs
│   ├── hospital_schema.sql           # Database Schema
│   ├── add_user_table.sql           # User Table Setup
│   ├── GUI_使用说明.md               # User Manual (Chinese)
│   ├── 快速启动指南.txt              # Quick Start Guide
│   └── 项目完成总结.md               # Project Summary
│
└── README.md                         # This file
```

## 🚀 Getting Started

### Prerequisites

- Windows 10 or later
- Visual Studio 2019/2022
- .NET Framework 4.7.2 or higher
- SQL Server LocalDB

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd 4150_hosSys
   ```

2. **Setup Database**
   ```sql
   -- Open SQL Server Management Studio or Visual Studio
   -- Connect to (LocalDB)\MSSQLLocalDB
   -- Execute: Hospital_Management_System/hospital_schema.sql
   -- Execute: Hospital_Management_System/add_user_table.sql
   ```

3. **Open Solution**
   ```
   Open Hospital_Management_System.sln in Visual Studio
   ```

4. **Run the Application**
   ```
   Press F5 to build and run
   ```

### 🔑 Test Accounts

| Role | Username | Password | Access Level |
|------|----------|----------|--------------|
| Admin | `admin` | `admin123` | Full access |
| Doctor | `doctor1` | `doc123` | Standard access |
| Nurse | `nurse1` | `nurse123` | Standard access |
| Receptionist | `reception1` | `rec123` | Standard access |

## 📖 Usage

### Login
1. Launch the application
2. Enter username and password
3. Click "登录" (Login)

### Patient Management
1. Click "患者管理" (Patient Management) from the side menu
2. Use the form on the right to add/update patient information
3. Select a patient from the list to view/edit details
4. Use the search box to filter patients

### Doctor Management
1. Click "医生管理" (Doctor Management)
2. Add new doctors with their specialization and department
3. Update or remove doctor records as needed

### Appointment Management
1. Click "预约管理" (Appointment Management)
2. Create new appointments by entering patient ID and doctor ID
3. Update appointment status as needed
4. Search and filter appointments

## 📊 Project Requirements Compliance

| Requirement | Status | Implementation |
|------------|--------|----------------|
| Functional User Interface | ✅ | WPF Desktop Application |
| Create Operations | ✅ | Add Patient/Doctor/Appointment |
| Read Operations | ✅ | View and Search functionality |
| Update Operations | ✅ | Edit records with validation |
| Delete Operations | ✅ | Remove records with confirmation |
| 2-3 User Roles | ✅ | Admin + 3 standard roles |
| Tools: C#/.NET | ✅ | C# WPF + .NET 4.7.2 |
| Database Integration | ✅ | SQL Server LocalDB |

### Evaluation Criteria

- **✅ Usability**: Modern UI, intuitive navigation, clear feedback
- **✅ Completeness**: Full CRUD + Authentication + Role management
- **✅ Database Integration**: Complete DAL layer with parameterized queries

## 🎯 Key Highlights

1. **🏗️ Clean Architecture**: 3-tier architecture with clear separation of concerns
2. **🎨 Modern UI/UX**: Material Design inspired interface with smooth interactions
3. **🔒 Security**: Parameterized queries to prevent SQL injection
4. **👥 Role-based Access**: Different permissions for different user roles
5. **🔍 Search & Filter**: Real-time search functionality across all modules
6. **✅ Validation**: Comprehensive input validation and error handling
7. **📝 Documentation**: Detailed user manual and quick start guide
8. **🧪 Test Data**: Pre-configured test accounts and sample data

## 📚 Documentation

- **[GUI_使用说明.md](Hospital_Management_System/GUI_使用说明.md)** - Comprehensive user manual (Chinese)
- **[快速启动指南.txt](Hospital_Management_System/快速启动指南.txt)** - Quick start guide
- **[项目完成总结.md](Hospital_Management_System/项目完成总结.md)** - Project completion summary

## 🐛 Troubleshooting

### Login fails
- Ensure `add_user_table.sql` has been executed
- Check database connection in `App.config`
- Verify username and password

### Database connection error
- Ensure SQL Server LocalDB is installed
- Check the connection string in `App.config`
- Verify `hospital_schema.mdf` exists in the correct location

### Foreign key constraint error
- Ensure Department records exist before adding doctors
- Ensure Patient and Doctor exist before creating appointments

## 🔮 Future Enhancements

- [ ] Password encryption (Hash + Salt)
- [ ] Appointment calendar view
- [ ] Medical records management
- [ ] Billing and invoicing
- [ ] Report generation
- [ ] Email notifications
- [ ] Data export to Excel
- [ ] Multi-language support

## 📄 License

This project is developed for educational purposes as part of the 4150 course project.

## 👨‍💻 Development Info

- **Project**: Hospital Management System
- **Version**: 1.0.0
- **Development Stage**: Step 5 - GUI Development
- **Status**: ✅ Completed and Ready for Evaluation

---

<div align="center">

**⚠️ Note**: This system uses plain text password storage for demonstration purposes only. In production, always use encrypted password storage.

Made with ❤️ for 4150 Project - Step 5

</div>

