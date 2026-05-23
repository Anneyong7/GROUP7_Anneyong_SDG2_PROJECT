# SDG 2: Zero Hunger Food Inventory and Distribution Management System
# PROJECT OVERVIEW

The SDG 2: Zero Hunger Food Inventory and Distribution Management System is a desktop-based application developed using VB.NET and SQL Server. The main objective of the system is to help organizations properly manage donated food items, monitor inventory stocks, distribute food supplies, and generate reports efficiently.

Many organizations still use manual methods such as paper records and handwritten logs when managing food donations and distributions. Because of this, records become inaccurate, reports may be delayed, and food items may expire without being noticed. This can lead to food wastage and poor distribution management.

To solve these problems, the researchers developed a computerized system that automates inventory monitoring, donating recording, and food distribution. The system also helps monitor expiration dates to reduce food waste and improve the organization of food resources.

The system includes the following major functions:
User Login and Role Management
Inventory Management
Food Distribution
Donation Logging
Report Generation
Expiration Monitoring

Through this project, organizations can manage food supplies more efficiently and provide better service to beneficiaries and communities.

# INSTALLATION STEPS
1. Set Up the Database
-Open your project in Visual Studio.
-Open the SQL Server Object Explorer window.
-Open the Database_Script.sql file.
-Click the green Execute button. This will clear any old connections, create a fresh SDG2_ZeroHungerDB database, build the correct tables, and create the default user accounts.

2. Verify the Connection
-Ensure you are using Visual Studio's local database feature. The connection string inside the VB.NET code is already configured to:
Server=(localdb)\MSSQLLocalDB;Database=SDG2_ZeroHungerDB;Integrated Security=True;

-No external server installation is required.

3. Run the Application
-Click the Start button at the top of Visual Studio.
-When the login screen appears, use the default administrator credentials:

Username: admin
Password: admin123

You are now ready to add inventory, record donations, and distribute food.

