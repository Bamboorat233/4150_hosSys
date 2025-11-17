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


