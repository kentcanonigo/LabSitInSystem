# Lab Sit-In System

This project is a WPF application developed using .NET 6.0 and SQLite for managing laboratory sit-in sessions. The system includes features for both students and administrators to streamline session tracking and approval processes.

---

## **Features**

### **For Students**
- Submit a session request for admin approval.
- Track session timer upon approval.
- Automatic session ending when time expires.

### **For Admins**
- View and approve/reject pending session requests.
- View and manage active and completed sessions.
- Add, edit, or delete student records.

---

## **Getting Started**

### **Prerequisites**
- Visual Studio 2022 (or later) with .NET 6.0 installed.
- SQLite installed or DB Browser for SQLite for database inspection (optional).

---

### **Setup Instructions**

#### **Clone the Repository**
```bash
# Clone the repository
git clone <repository-url>

# Navigate to the project directory
cd LabSitInSystem
```

#### **Open the Project**
1. Open `LabSitInSystem.sln` in Visual Studio.
2. Restore NuGet packages if prompted.

#### **Set Up the Local Database**

1. **Generate Database File Using Migrations** (Recommended):
   - Ensure the required NuGet packages are installed:
     ```bash
     Install-Package Microsoft.EntityFrameworkCore
     Install-Package Microsoft.EntityFrameworkCore.Sqlite
     Install-Package Microsoft.EntityFrameworkCore.Tools
     ```
   - Open the Package Manager Console in Visual Studio and run the following commands:
     ```bash
     Add-Migration InitialCreate
     Update-Database
     ```
   - This will create the `LaboratorySitInSystem.db` file in the project directory.

2. **Update Connection String (Mandatory):**
   - Navigate to `AppDbContext` in the project.
   - Replace the connection string in the `OnConfiguring` method with the path to your local database file:
     ```csharp
     optionsBuilder.UseSqlite("Data Source=\"C:\\path\\to\\your\\LaboratorySitInSystem.db\"");
     ```

#### **Run the Application**
1. Press `F5` or `Ctrl+F5` to build and run the application.
2. Log in as an admin or student to access respective features.

---

## **Admin Account Setup**
An initial admin account is seeded into the database:
- **Username:** `admin`
- **Password:** `admin123`

You can update the admin credentials via the database or application.

---

## **Changing Database Path**
To change the database file location:
1. Locate the `AppDbContext` class.
2. Update the connection string in the `OnConfiguring` method:
   ```csharp
   optionsBuilder.UseSqlite("Data Source=\"C:\\path\\to\\your\\database.db\"");
   ```

Ensure the file exists at the specified path.

---

## **How to Contribute**
1. Fork the repository.
2. Create a new branch for your feature or bug fix:
   ```bash
   git checkout -b feature-name
   ```
3. Commit your changes and push them to your fork.
4. Open a pull request with a detailed description of your changes.

---

## **License**
This project is licensed under the MIT License.

---

## **Contact**
For questions or support, please contact the project maintainer.

---

## **TODO**

### **More Features**
- Add detailed session history for students and admins.
- Implement notifications for session timeouts and approvals.
- Provide export options (e.g., CSV, PDF) for session records.

### **Beautification**
- Enhance UI/UX design using modern WPF styling.
- Add themes (e.g., light/dark mode).
- Include responsive layouts for better screen adaptability.
