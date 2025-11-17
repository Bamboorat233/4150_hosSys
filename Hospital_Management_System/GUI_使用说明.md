# 医院管理系统 GUI 使用说明

## 项目概述

这是一个基于 C# WPF 开发的医院管理系统，实现了完整的 CRUD 操作和多用户角色管理。

## 技术栈

- **框架**: WPF (.NET Framework 4.7.2)
- **数据库**: SQL Server LocalDB
- **语言**: C#
- **架构**: 三层架构 (GUI层、DAL数据访问层、Model模型层)

## 功能特性

### 1. 用户认证系统
- ✅ 用户登录界面
- ✅ 支持多种用户角色：Admin、Doctor、Nurse、Receptionist
- ✅ 基于角色的权限控制

### 2. 主要管理模块

#### 患者管理 (Patient Management)
- ✅ 新增患者信息
- ✅ 查看患者列表
- ✅ 更新患者信息
- ✅ 删除患者记录
- ✅ 搜索患者功能

#### 医生管理 (Doctor Management)
- ✅ 新增医生信息
- ✅ 查看医生列表
- ✅ 更新医生信息
- ✅ 删除医生记录
- ✅ 搜索医生功能

#### 预约管理 (Appointment Management)
- ✅ 创建预约
- ✅ 查看所有预约
- ✅ 更新预约状态
- ✅ 删除预约
- ✅ 预约状态管理 (Scheduled, Completed, Cancelled, No-Show)

### 3. 用户界面特点
- 🎨 现代化的 Material Design 风格
- 🎯 直观的操作流程
- 📱 响应式布局
- 🔍 实时搜索过滤
- ⚡ 友好的错误提示

## 安装和设置

### 1. 数据库设置

首先需要创建数据库并添加User表：

1. 打开 SQL Server Management Studio 或 Visual Studio 的 SQL Server Object Explorer
2. 确保已运行 `hospital_schema.sql` 创建基础表
3. 运行 `add_user_table.sql` 创建User表并插入测试数据

```sql
-- 执行此脚本创建User表
-- 位置: Hospital_Management_System/add_user_table.sql
```

### 2. 测试账户

系统已预置以下测试账户：

| 角色 | 用户名 | 密码 | 说明 |
|------|--------|------|------|
| 管理员 | admin | admin123 | 拥有所有权限 |
| 医生 | doctor1 | doc123 | 普通医生权限 |
| 护士 | nurse1 | nurse123 | 护士权限 |
| 前台 | reception1 | rec123 | 前台接待权限 |

### 3. 运行项目

1. 在 Visual Studio 中打开解决方案 `Hospital_Management_System.sln`
2. 确保 `Hospital_MamSys_GUI` 项目设为启动项目
3. 按 F5 运行项目
4. 使用测试账户登录

## 使用指南

### 登录系统

1. 启动程序后会显示登录界面
2. 输入用户名和密码
3. 点击"登录"按钮
4. 登录成功后进入主界面

### 主界面导航

主界面左侧是导航菜单，包含：
- 📋 患者管理
- 👨‍⚕️ 医生管理
- 📅 预约管理
- 🏥 科室管理 (Admin专用)
- 👤 用户管理 (Admin专用)
- 🚪 退出登录

### 患者管理操作

#### 新增患者
1. 点击左侧菜单"患者管理"
2. 在右侧表单填写患者信息
   - 姓名 (必填)
   - 出生日期 (必填)
   - 性别 (必选: Male/Female/Other)
   - 联系方式 (可选)
   - 地址 (可选)
3. 点击"新增患者"按钮

#### 查看和搜索患者
- 左侧列表显示所有患者
- 使用顶部搜索框可按姓名、ID或联系方式搜索
- 点击列表中的患者可在右侧查看详细信息

#### 更新患者信息
1. 在列表中选择要更新的患者
2. 修改右侧表单中的信息
3. 点击"更新信息"按钮

#### 删除患者
1. 在列表中选择要删除的患者
2. 点击"删除患者"按钮
3. 确认删除操作

### 医生管理操作

#### 新增医生
1. 点击左侧菜单"医生管理"
2. 填写医生信息
   - 姓名 (必填)
   - 专业/职称 (可选)
   - 联系方式 (可选)
   - 科室ID (必填)
3. 点击"新增医生"按钮

#### 其他操作
- 查看、搜索、更新、删除操作与患者管理类似

### 预约管理操作

#### 创建预约
1. 点击左侧菜单"预约管理"
2. 填写预约信息
   - 患者ID (必填)
   - 医生ID (必填)
   - 预约日期 (必填)
   - 预约时间 (必填，格式: HH:mm，如 09:00)
   - 状态 (必选: Scheduled/Completed/Cancelled/No-Show)
3. 点击"新增预约"按钮

#### 更新预约状态
1. 选择要更新的预约
2. 修改状态或其他信息
3. 点击"更新预约"按钮

## 角色权限说明

### Admin (管理员)
- ✅ 访问所有功能模块
- ✅ 管理用户账户
- ✅ 管理科室信息
- ✅ 完整的CRUD权限

### Doctor/Nurse/Receptionist (普通用户)
- ✅ 患者管理
- ✅ 医生管理
- ✅ 预约管理
- ❌ 无法访问用户管理
- ❌ 无法访问科室管理

## 系统要求

- **操作系统**: Windows 10 或更高版本
- **.NET Framework**: 4.7.2 或更高版本
- **数据库**: SQL Server LocalDB 或 SQL Server Express
- **内存**: 至少 2GB RAM
- **存储**: 至少 100MB 可用空间

## 项目结构

```
Hospital_Management_System/
├── MainWindow.xaml/cs           # 主界面
├── LoginWindow.xaml/cs          # 登录界面
├── PatientManagementWindow.xaml/cs      # 患者管理
├── DoctorManagementWindow.xaml/cs       # 医生管理
├── AppointmentManagementWindow.xaml/cs  # 预约管理
├── App.xaml/cs                  # 应用程序入口
├── App.config                   # 配置文件
├── hospital_schema.sql          # 数据库schema
└── add_user_table.sql          # User表创建脚本

Hospital_MamSys_LIB/
├── Model/                       # 数据模型
│   ├── Patient.cs
│   ├── Doctor.cs
│   ├── Appointment.cs
│   ├── User.cs
│   └── ...
└── DAL/                        # 数据访问层
    ├── DALBase.cs
    ├── DALPatient.cs
    ├── DALDoctor.cs
    ├── DALAppointment.cs
    ├── DALUser.cs
    └── ...
```

## 常见问题

### Q: 登录时提示"登录失败"
**A**: 请检查：
1. 是否已运行 `add_user_table.sql` 创建User表
2. 用户名和密码是否正确
3. 数据库连接是否正常

### Q: 新增记录时提示外键约束错误
**A**: 
1. 添加医生时，确保科室ID存在于Department表中
2. 创建预约时，确保患者ID和医生ID都已存在
3. 可以先在数据库中插入测试数据

### Q: 数据库连接失败
**A**: 检查 `App.config` 中的连接字符串是否正确，确保 `hospital_schema.mdf` 文件在正确的位置。

## 开发人员信息

- **项目**: Hospital Management System
- **版本**: 1.0.0
- **开发环境**: Visual Studio 2019/2022
- **数据库**: SQL Server LocalDB

## 评估标准对照

### ✅ Functional User Interface (desktop)
- WPF桌面应用程序
- 现代化UI设计
- 良好的用户体验

### ✅ CRUD Operations
- Create: 新增患者、医生、预约
- Read: 查看和搜索记录
- Update: 更新信息
- Delete: 删除记录

### ✅ Support for 2-3 User Roles
- Admin (管理员)
- Normal User (Doctor/Nurse/Receptionist)
- 基于角色的权限控制

### ✅ Tools: C#/.NET
- C# WPF
- .NET Framework 4.7.2

### ✅ Integration with Database
- SQL Server LocalDB
- 完整的DAL数据访问层
- 参数化查询防止SQL注入

## 未来改进方向

1. 🔐 密码加密存储 (Hash + Salt)
2. 📊 数据统计和报表功能
3. 🔔 预约提醒功能
4. 📝 病历管理增强
5. 💊 药品库存管理完善
6. 🖨️ 打印发票和收据
7. 📧 邮件通知功能
8. 🌐 多语言支持

## 许可证

此项目用于教育和学习目的。

---

**注意**: 本系统中的密码存储为明文，仅用于演示目的。在生产环境中，必须使用加密方式存储密码。

