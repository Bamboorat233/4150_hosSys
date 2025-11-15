-- 添加User表用于用户认证和角色管理
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

-- 创建索引
CREATE INDEX IX_User_Username ON dbo.[User](Username);
CREATE INDEX IX_User_Role ON dbo.[User](Role);

-- 插入初始测试数据
-- 注意：在生产环境中，密码应该加密存储
INSERT INTO dbo.[User] (Username, [Password], Role, FullName, Email, CreatedDate, IsActive)
VALUES 
    (N'admin', N'admin123', N'Admin', N'系统管理员', N'admin@hospital.com', GETDATE(), 1),
    (N'doctor1', N'doc123', N'Doctor', N'张医生', N'doctor1@hospital.com', GETDATE(), 1),
    (N'nurse1', N'nurse123', N'Nurse', N'李护士', N'nurse1@hospital.com', GETDATE(), 1),
    (N'reception1', N'rec123', N'Receptionist', N'王接待员', N'reception1@hospital.com', GETDATE(), 1);

-- 显示创建的用户
SELECT * FROM dbo.[User];

-- 测试用户登录信息：
-- 管理员账户: admin / admin123
-- 医生账户: doctor1 / doc123
-- 护士账户: nurse1 / nurse123
-- 前台账户: reception1 / rec123

