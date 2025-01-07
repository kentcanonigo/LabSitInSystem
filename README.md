# Lab Sit-In System

This project is a WPF application developed using .NET 6.0 and SQLite for managing laboratory sit-in sessions. The system includes features for both students and administrators to streamline session tracking and approval processes.

---

## **Features**

### **For Students**
- Submit a session request for admin approval.
- Track session timer upon approval.
- Automatic session ending when time expires.
- Receive notifications for remaining time.

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

## **User Manual**

### **For Students**

#### **Logging In**
1. Open the application and navigate to the Student Login screen.
2. Enter your credentials (Student ID and Password) and click `Login`.

#### **Starting a Lab Session**
1. After logging in, click the `Start Session` button.
2. Wait for admin approval. The status bar will display "Waiting for approval."
3. Once approved, the timer will start, and the status bar will turn green, displaying "Lab Session Active."
4. Notifications will appear when 20 minutes and 10 minutes remain.

#### **Ending a Session**
1. To end your session manually, click the `End Session` button.
2. If the session is not approved, it will be canceled and removed from the system.
3. If the session is approved, the system will record the session duration.

#### **Notifications**
- You will be notified when 20 minutes and 10 minutes remain in your session.
- Ensure you monitor these notifications to manage your time effectively.

---

### **For Admins**

#### **Logging In**
1. Open the application and navigate to the Admin Login screen.
2. Enter your credentials (Username and Password) and click `Login`.

#### **Managing Pending Requests**
1. Navigate to the Pending Requests section.
2. View all unapproved session requests in the `PendingRequestsDataGrid`.
3. To approve a session:
   - Select a request and click the `Approve Selected` button.
   - The session will move to the active sessions list.

#### **Viewing and Managing Sessions**
1. Navigate to the Sit-In Sessions section.
2. View all approved sessions in the `SitInsDataGrid`.
3. To delete or edit a session, use the appropriate options in the admin interface.

#### **Managing Student Records**
1. Navigate to the Student Records section.
2. Use the `Add`, `Edit`, or `Delete` buttons to manage student information.
3. Ensure each student has a unique ID and associated details.

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

## **TODO**

### **More Features**
- Add detailed session history for students and admins.
- Implement notifications for session timeouts and approvals.
- Provide export options (e.g., CSV, PDF) for session records.

### **Beautification**
- Enhance UI/UX design using modern WPF styling.
- Add themes (e.g., light/dark mode).
- Include responsive layouts for better screen adaptability.
