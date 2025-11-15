-- 附加数据库
USE master;
GO

CREATE DATABASE hospital_schema
ON (FILENAME = 'C:\Users\lizengpu\source\repos\4150_hosSys\Hospital_Management_System\bin\Debug\hospital_schema.mdf'),
   (FILENAME = 'C:\Users\lizengpu\source\repos\4150_hosSys\Hospital_Management_System\bin\Debug\hospital_schema_log.ldf')
FOR ATTACH;
GO

-- 切换到新数据库
USE hospital_schema;
GO

-- 创建 User 表
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
GO

-- 创建索引
CREATE INDEX IX_User_Username ON dbo.[User](Username);
CREATE INDEX IX_User_Role ON dbo.[User](Role);
GO

-- 插入测试数据
INSERT INTO dbo.[User] (Username, [Password], Role, FullName, Email, CreatedDate, IsActive)
VALUES 
    (N'admin', N'admin123', N'Admin', N'系统管理员', N'admin@hospital.com', GETDATE(), 1),
    (N'doctor1', N'doc123', N'Doctor', N'张医生', N'doctor1@hospital.com', GETDATE(), 1),
    (N'nurse1', N'nurse123', N'Nurse', N'李护士', N'nurse1@hospital.com', GETDATE(), 1),
    (N'reception1', N'rec123', N'Receptionist', N'王接待员', N'reception1@hospital.com', GETDATE(), 1);
GO

-- 验证结果
SELECT * FROM dbo.[User];
GO