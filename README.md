# 📚 Side Projects — Thực hành tổng hợp ASP.NET Core

> **Mục tiêu:** Thực hành tổng hợp tất cả kiến thức đã học từ các khóa:
> - C# OOP (7 modules)
> - Introduction to Relational Databases (IBM - 4 modules)
> - Dr. Chuck's PostgreSQL (4 modules)
> - Web Application Development with ASP.NET Core (4 modules)

---

## 📋 Tổng quan 3 Side Projects

| # | Project | Độ khó | Mô tả ngắn |
|---|---------|--------|------------|
| 1 | [TaskFlow — Personal Task Manager](#project-1-taskflow) | ⭐ Beginner | Quản lý công việc cá nhân |
| 2 | [BookHive — Book Review Platform](#project-2-bookhive) | ⭐⭐ Intermediate | Nền tảng review sách |
| 3 | [ShopCore — Mini E-Commerce](#project-3-shopcore) | ⭐⭐⭐ Advanced | Cửa hàng trực tuyến mini |

---

## 🧠 Skill Matrix — Mỗi project thực hành những gì?

| Kỹ năng | Project 1 | Project 2 | Project 3 |
|---------|:---------:|:---------:|:---------:|
| **C# Classes & OOP** | ✅ | ✅ | ✅ |
| **Inheritance / Polymorphism** | 🔸 cơ bản | ✅ | ✅ nâng cao |
| **Interfaces / Abstract Classes** | ❌ | ✅ | ✅ |
| **Collections & LINQ** | ✅ cơ bản | ✅ nâng cao | ✅ nâng cao |
| **Exception Handling** | 🔸 | ✅ | ✅ |
| **SQL CRUD** | ✅ (qua EF) | ✅ | ✅ |
| **Relationships (1:N)** | ✅ | ✅ | ✅ |
| **Relationships (M:N)** | ❌ | ✅ | ✅ |
| **JOINs / GROUP BY** | 🔸 | ✅ | ✅ |
| **Normalization / Constraints** | 🔸 | ✅ | ✅ |
| **MVC Pattern** | ✅ | ✅ | ✅ |
| **Razor Views** | ✅ | ✅ | ✅ |
| **Web API (REST)** | ❌ | ✅ | ✅ |
| **EF Core (ORM)** | ✅ | ✅ | ✅ |
| **Migrations** | ✅ | ✅ | ✅ |
| **Model Binding / Validation** | ✅ | ✅ | ✅ |
| **Identity (Register/Login)** | ✅ | ✅ | ✅ |
| **JWT Authentication** | ❌ | ❌ | ✅ |
| **Role-based Authorization** | 🔸 | ✅ | ✅ |
| **Claims/Policy Authorization** | ❌ | 🔸 | ✅ |
| **AJAX** | 🔸 | ✅ | ✅ |
| **Pagination / Filtering** | ❌ | ✅ | ✅ |
| **Repository Pattern** | ❌ | ✅ | ✅ |
| **Data Seeding** | ✅ | ✅ | ✅ |
| **Security (CSRF, XSS)** | 🔸 | ✅ | ✅ |
| **Caching** | ❌ | ❌ | ✅ |
| **Logging / Auditing** | ❌ | 🔸 | ✅ |
| **Data Encryption** | ❌ | ❌ | ✅ |

> ✅ = đầy đủ &nbsp; 🔸 = cơ bản &nbsp; ❌ = không có trong project này

---

## Project 1: TaskFlow

### 📌 Mô tả
Ứng dụng quản lý công việc cá nhân (Todo App). Người dùng có thể đăng ký tài khoản, đăng nhập, tạo/sửa/xóa tasks, phân loại tasks theo categories, và đánh dấu hoàn thành.

### 🎯 Yêu cầu chức năng

1. **Authentication:** Đăng ký, đăng nhập, đăng xuất (Identity Framework)
2. **Task CRUD:** Tạo, xem, sửa, xóa task
3. **Categories:** Phân loại tasks (Work, Personal, Study, etc.)
4. **Status:** Đánh dấu task Pending / In Progress / Completed
5. **Priority:** Gán mức ưu tiên (Low, Medium, High)
6. **Due Date:** Đặt deadline cho task
7. **My Tasks:** Mỗi user chỉ thấy tasks của mình
8. **Dashboard:** Trang chủ hiển thị tổng quan tasks

### 🗄️ Database Schema

```
┌──────────────┐       ┌──────────────┐
│ AspNetUsers   │       │  Categories  │
│ (Identity)    │       ├──────────────┤
│               │       │ Id (PK)      │
│ Id            │       │ Name         │
│ Email         │       │ Color        │
│ UserName      │       └──────┬───────┘
└──────┬───────┘              │
       │                      │
       │ 1:N                  │ 1:N
       │                      │
┌──────┴──────────────────────┴───────┐
│             TodoItems               │
├─────────────────────────────────────┤
│ Id (PK)                             │
│ Title                               │
│ Description                         │
│ DueDate                             │
│ Priority (enum: Low/Medium/High)    │
│ Status (enum: Pending/InProgress/   │
│         Completed)                  │
│ CreatedAt                           │
│ UserId (FK → AspNetUsers)           │
│ CategoryId (FK → Categories)        │
└─────────────────────────────────────┘
```

### 📂 Cấu trúc thư mục mong muốn
```
project-1-taskflow/
├── Controllers/
│   ├── HomeController.cs
│   └── TodoController.cs
├── Models/
│   ├── TodoItem.cs
│   ├── Category.cs
│   └── Enums.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Views/
│   ├── Home/
│   ├── Todo/
│   └── Shared/
├── wwwroot/css/
├── Program.cs
└── appsettings.json
```

### 💡 Gợi ý khi thực hành
- Bắt đầu từ Model → DbContext → Migration → Controller → View
- Không cần tối ưu ngay, viết code đơn giản nhất có thể
- Sau khi hoàn thành, tự review lại xem đâu có thể cải thiện
